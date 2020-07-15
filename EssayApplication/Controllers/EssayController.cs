using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EssayBusiness.Models;
using EssayBusiness.Services.Abstract;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EssayWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EssayController : ControllerBase
    {
        private IEssayService _essayService;
        //public static IWebHostEnvironment _environment;

        public EssayController(IEssayService essayService)
        {
            _essayService = essayService;
        }
        [HttpPost("AddEssay")]
        public async Task<IActionResult> AddEssay([FromForm]EssayModel essay)
        {
            var result = await _essayService.AddEssay(essay);

            if (result.Success)
            {
                return new ObjectResult(result.Message);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        [HttpPost("DeleteEssay")]
        public async Task<IActionResult> DeleteEssay(string essayId)
        {
            var result = await _essayService.DeleteEssay(essayId);

            if (result.Success)
            {
                return new ObjectResult(result.Message);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        [HttpPost("UpdateEssay")]
        public async Task<IActionResult> UpdateEssay(string essayId, EssayModel model)
        {
            var result = await _essayService.UpdateEssay(essayId, model);

            if (result.Success)
            {
                return new ObjectResult(result.Message);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        [HttpGet("GetEssay")]
        public async Task<IActionResult> GetEssay()
        {
            var result = await _essayService.GetEssay();

            if (result.Success)
            {
                return new ObjectResult(result.Data);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

    }
}
