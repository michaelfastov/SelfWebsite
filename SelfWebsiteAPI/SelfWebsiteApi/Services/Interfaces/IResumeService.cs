﻿using SelfWebsiteApi.Models.ResumeModels;

namespace SelfWebsiteApi.Services.Interfaces
{
    public interface IResumeService
    {
        Task<List<ResumeModel>> SearchResume(string input);
        Task<ResumeModel?> GetMainResume();
        Task<ResumeModel?> CreateResume(ResumeModel resume);
        Task<ResumeModel?> UpdateResume(ResumeModel resume);
        Task<ResumeModel?> CreateOrUpdateResume(ResumeModel resume);
        Task DeleteResume(int resumeId);
    }
}
