using Essay.Core;
using Essay.Entities;
using EssayBusiness.Models;
using EssayBusiness.Services.Abstract;
using EssayData.Services.Abstract;
using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace EssayBusiness.Services.Concrete
{
    public class EssayService : IEssayService
    {
        private IEssayMongoService _essayMongoService;
        public EssayService(IEssayMongoService essayMongoService)
        {
            _essayMongoService = essayMongoService;
        }

        public async Task<IDataResult<EssayCollection>> AddEssay(EssayModel essay)
        {
            try
            {
                EssayCollection essayCollection = new EssayCollection();
                essayCollection.Title = essay.Title;
                essayCollection.Author = essay.Author;
                essayCollection.Language = essay.Language;
                essayCollection.Subject = essay.Subject;
                essayCollection.Record = DateTime.Now;
                essayCollection.LastUpdate = DateTime.Now;
                if (essay.Comment == null)
                {
                    essayCollection.Comment = "*No Comment*";
                }
                else
                    essayCollection.Comment = essay.Comment;

                var fileName = Path.GetFileName(essay.TextFile.FileName);
                var fileExtension = Path.GetExtension(fileName);
                var path = String.Concat(Convert.ToString(Guid.NewGuid()), fileExtension);
                essayCollection.Path = "/" + path;

                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
                using (var localFile = File.OpenWrite(fileName))

                using (var uploadedFile = essay.TextFile.OpenReadStream())
                {
                    uploadedFile.CopyTo(localFile);
                    await uploadedFile.FlushAsync();
                }
                //var objfiles = new Files()
                //{
                //    DocumentId = 0,
                //    Name = newFileName,
                //    FileType = fileExtension,
                //    CreatedOn = DateTime.Now
                //};

                //using (var target = new MemoryStream())
                //{
                //    files.CopyTo(target);
                //    objfiles.DataFiles = target.ToArray();
                //}
                await _essayMongoService.Insert(essayCollection);

                return new SuccessDataResult<EssayCollection>("Completed!");
            }
            catch (Exception ex)
            {
                return new FailDataResult<EssayCollection>("Oops! Something is wrong..." + ex.Message.ToString());
            }
        }

        public async Task<IDataResult<EssayCollection>> DeleteEssay(string essayId)
        {
            try
            {
                var _filter = Builders<EssayCollection>.Filter.Where(x => x.Id == essayId);
                var _get = await _essayMongoService.GetBy(_filter);
                if (_get != null)
                {
                    await _essayMongoService.Delete(essayId);
                }
                return new SuccessDataResult<EssayCollection>("Completed!");

            }
            catch (Exception ex)
            {
                return new FailDataResult<EssayCollection>("Oops! Something is wrong..." + ex.Message.ToString());
            }

        }

        public async Task<IDataResult<IEnumerable<EssayCollection>>> GetEssay()
        {
            try
            {

                var _get = await _essayMongoService.GetAll();

                return new SuccessDataResult<IEnumerable<EssayCollection>>(_get);

            }
            catch (Exception ex)
            {
                return new FailDataResult<IEnumerable<EssayCollection>>("Oops! Record couldn't find..." + ex.Message.ToString());
            }
        }

        public async Task<IDataResult<EssayCollection>> UpdateEssay(string essayId, EssayModel model)
        {
            try
            {
                var _filter = Builders<EssayCollection>.Filter.Where(x => x.Id == essayId);
                var _get = await _essayMongoService.GetBy(_filter);
                _get.Title = model.Title;
                _get.Subject = model.Subject;
                _get.Language = model.Language;
                _get.Comment = model.Comment;
                _get.Author = model.Author;
                _get.LastUpdate = DateTime.UtcNow + TimeSpan.FromHours(3);
                await _essayMongoService.Replace(_get);
                return new SuccessDataResult<EssayCollection>("Completed!");

            }
            catch (Exception ex)
            {
                return new FailDataResult<EssayCollection>("Oops! Record couldn't find..." + ex.Message.ToString());
            }

        }

    }
}
