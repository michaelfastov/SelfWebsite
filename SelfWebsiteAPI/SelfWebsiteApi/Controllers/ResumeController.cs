using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SelfWebsiteApi.Enums.Auth;
using SelfWebsiteApi.Enums.Resume;
using SelfWebsiteApi.Models.ResumeModels;
using SelfWebsiteApi.Services.Interfaces.ResumeServices;
using System.Configuration;

namespace SelfWebsiteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResumeController : ControllerBase
    {
        private readonly IResumeService _resumeService;
        public ResumeController(IResumeService resumeService)
        {
            _resumeService = resumeService;
        }

        [HttpGet("GetMainResume")]
        public async Task<ActionResult<ResumeModel>> GetMainResume()
        {
            return await _resumeService.GetMainResume();
        }

            [HttpGet("GetMainResumeTest")]
        public async Task<ActionResult<ResumeModel>> GetMainResumeTest()
        {
            var resume = new ResumeModel();
            resume.Name = "Michael Fastov";
            resume.Description = "Full Stack .NET developer with 3+ years of experience.";
            resume.Sections = new List<SectionModel>();

            var section1 = new SectionModel();
            //section1.Id = 1;
            section1.SectionType = SectionType.WorkExperience;
            section1.JobTitle = "Full Stack .NET Developer";
            section1.Company = "Varteq inc.";
            section1.From = DateTime.Now.AddDays(-20);
            section1.To = DateTime.Now;
            section1.Content = @"• Integrated 4 third party reporting API's in the application
            • Participated in project upgrade from .NET 2.2 to .NET 6.0
            • Worked with Google Ads, Google Campaign Manager, Google Display Video, MediaMath, Paragone APIs";
            section1.Separator = " - ";

            var section2 = new SectionModel();
            //section2.Id = 2;
            section2.SectionType = SectionType.WorkExperience;
            section2.JobTitle = "Full Stack .NET Developer";
            section2.Company = "Langate Software";
            section2.From = DateTime.Now.AddDays(-40);
            section2.To = DateTime.Now.AddDays(-20);
            section2.Content = @"• Worked directly with the customer
            • Created an authorization security structure
            • Improved and refactored the legacy application code";
            section2.Separator = " - ";

            var section3 = new SectionModel();
            //section3.Id = 3;
            section3.SectionType = SectionType.Education;
            //section3.Title = "test";
            section3.Company = "Kharkiv National University of Radio Electronics";
            section3.Location = "Kharkiv, Ukraine";
            section3.Description = "Bachelor of Software Engineering";
            section3.From = DateTime.Now.AddDays(-40);
            section3.To = DateTime.Now.AddDays(-10);


            resume.Sections.Add(section1);
            resume.Sections.Add(section2);
            resume.Sections.Add(section3);
            resume.Links = new List<LinkModel>();
            //resume.Links.Add(new LinkModel() { Id = 1 });
            //var resume = await _resumeService.GetMainResume();

            //if (resume is null)
            //{
            //    return NotFound();
            //}

            return Ok(resume);
        }

        [HttpPost("admin/CreateResume"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResumeModel>> CreateResume([FromBody] ResumeModel resume)
        {
            var resumeResult = await _resumeService.CreateResume(resume);

            if (resumeResult == null)
            {
                return BadRequest();
            }

            return Ok(resumeResult);
        }

        [HttpPut("admin/UpdateResume"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResumeModel>> UpdateResume([FromBody] ResumeModel resume)
        {
            var resumeResult = await _resumeService.UpdateResume(resume);

            if (resumeResult == null)
            {
                return BadRequest();
            }

            return Ok(resumeResult);
        }

        [HttpPost("admin/CreateOrUpdateResume"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResumeModel>> CreateOrUpdateResume([FromBody] ResumeModel resume)
         {
            var resumeResult = await _resumeService.CreateOrUpdateResume(resume);

            if (resumeResult == null)
            {
                return BadRequest();
            }

            return Ok(resumeResult);
        }

        [HttpDelete("admin/DeleteResume"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResumeModel>> DeleteResume([FromBody] int resumeId)
        {
            await _resumeService.DeleteResume(resumeId); ;

            return NoContent();
        }
    }
}
