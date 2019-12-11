using DicomViewerAPI.Models.Orthanc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DicomViewerAPI.Services
{
    public interface IPACSService
    {
        Task<List<PatientModel>> GetAllPatients();
        Task<List<PatientModel>> GetPatientById(string patientId);
        Task<List<StudiesModel>> GetAllStudies();
        Task<StudiesModel> GetStudyById(string studyId);
        Task<List<StudiesModel>> GetPatientStudies(string patientId);
        Task<List<SeriesModel>> GetAllSeries();
        Task<List<SeriesModel>> GetSeriesById(string seriesId);
        Task<List<ImageSeriesModel>> GetImageSeriesByPatientId(string patientId);
        Task<ImageSeriesParentStudyModel> GetImageStudyById(string studyId);
        Task<List<ImageSeriesModel>> GetImageSeriesByStudyId(string studyId);
        Task<ImageStudyModel> GetImageStudyByStudyId(string studyId);
        Task<InstanceModel> GetInstanceById(string instanceId);
        string GetInstancePreviewById(string instanceId, string hostUrl = "");

        Task<object> GetDicomTagsById(string instanceId);
    }
}
