using Essay.Core;
using Essay.Entities;
using EssayBusiness.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EssayBusiness.Services.Abstract
{
    public interface IEssayService
    {
        Task<IDataResult<EssayCollection>> AddEssay(EssayModel essay);
        Task<IDataResult<EssayCollection>> DeleteEssay(string essayId);
        Task<IDataResult<EssayCollection>> UpdateEssay(string essayId, EssayModel model);
        Task<IDataResult<IEnumerable<EssayCollection>>> GetEssay();

    }
}
