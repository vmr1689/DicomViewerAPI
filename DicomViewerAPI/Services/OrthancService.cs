using DicomViewerAPI.Helpers;
using DicomViewerAPI.Models.Orthanc;
using Microsoft.Extensions.Options;
using RestSharp;
using RestSharp.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DicomViewerAPI.Services
{
    public class OrthancService : IPACSService
    {
        private static readonly string PathToDicomImages = Path.Combine(@"D:\POC\Latest\DicomViewer\Api\DicomViewerAPI\DicomViewerAPI\Images\dcm\");
        private static readonly string PathToDicomJpgImages = Path.Combine(@"D:\POC\Latest\DicomViewer\Api\DicomViewerAPI\DicomViewerAPI\Images\jpg\");
        private static readonly string PathToDicomSegments = Path.Combine(@"D:\POC\Latest\DicomViewer\Api\DicomViewerAPI\DicomViewerAPI\Images\segments\");

        private readonly AppSettings _appSettings;

        private static readonly IEnumerable<HttpStatusCode> ValidStatusCodes = new List<HttpStatusCode>
        {
            HttpStatusCode.OK,
            HttpStatusCode.Created,
            HttpStatusCode.NoContent
        };

        private readonly RestClient _client;

        public OrthancService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _client = new RestClient(_appSettings.OrthancServiceUrl);
        }


        public async Task<List<PatientModel>> GetAllPatients()
        {
            var apiUrl = "/patients?expand";
            var request = new RestRequest(apiUrl, Method.GET);

            var response = await this.SendRequestAsync<List<PatientModel>>(request, string.Empty, apiUrl);

            if (response.IsSuccessful)
            {
                return response.Data;
            }

            List<PatientModel> returnResponse = new List<PatientModel>
            {
                new PatientModel { }
            };

            return await Task.FromResult<List<PatientModel>>(returnResponse);
        }


        public async Task<List<PatientModel>> GetPatientById(string patientId)
        {
            var apiUrl = string.Format("/patients/{0}", patientId);
            var request = new RestRequest(apiUrl, Method.GET);

            var response = await this.SendRequestAsync<List<PatientModel>>(request, string.Empty, apiUrl);

            if (response.IsSuccessful)
            {
                return response.Data;
            }

            List<PatientModel> returnResponse = new List<PatientModel>
            {
                new PatientModel { }
            };

            return await Task.FromResult<List<PatientModel>>(returnResponse);
        }

        public async Task<List<StudiesModel>> GetAllStudies()
        {
            var apiUrl = "/studies?expand";
            var request = new RestRequest(apiUrl, Method.GET);

            var response = await this.SendRequestAsync<List<StudiesModel>>(request, string.Empty, apiUrl);

            if (response.IsSuccessful)
            {
                return response.Data;
            }

            List<StudiesModel> returnResponse = new List<StudiesModel>
            {
                new StudiesModel { }
            };

            return await Task.FromResult<List<StudiesModel>>(returnResponse);
        }

        public async Task<StudiesModel> GetStudyById(string studyId)
        {
            var apiUrl = string.Format("/studies/{0}", studyId);
            var request = new RestRequest(apiUrl, Method.GET);

            var response = await this.SendRequestAsync<StudiesModel>(request, string.Empty, apiUrl);

            if (response.IsSuccessful)
            {
                return response.Data;
            }

            StudiesModel returnResponse = new StudiesModel();

            return await Task.FromResult<StudiesModel>(returnResponse);
        }

        public async Task<ImageSeriesParentStudyModel> GetImageStudyById(string studyId)
        {
            var apiUrl = string.Format("/studies/{0}", studyId);
            var request = new RestRequest(apiUrl, Method.GET);

            var response = await this.SendRequestAsync<ImageSeriesParentStudyModel>(request, string.Empty, apiUrl);

            if (response.IsSuccessful)
            {
                return response.Data;
            }

            ImageSeriesParentStudyModel returnResponse = new ImageSeriesParentStudyModel();


            return await Task.FromResult<ImageSeriesParentStudyModel>(returnResponse);
        }

        private async Task<IRestResponse<T>> SendRequestAsync<T>(IRestRequest request, string bearerToken, string apiUrl, bool isIAMRequest = false)
        {
            var result = await _client.ExecuteTaskAsync<T>(request);

            return result;
        }

        public async Task<List<StudiesModel>> GetPatientStudies(string patientId)
        {
            var apiUrl = string.Format("/patients/{0}/studies", patientId);
            var request = new RestRequest(apiUrl, Method.GET);

            var response = await this.SendRequestAsync<List<StudiesModel>>(request, string.Empty, apiUrl);

            if (response.IsSuccessful)
            {
                return response.Data;
            }

            List<StudiesModel> returnResponse = new List<StudiesModel>
            {
                new StudiesModel { }
            };

            return await Task.FromResult<List<StudiesModel>>(returnResponse);
        }

        public async Task<List<SeriesModel>> GetAllSeries()
        {
            var apiUrl = "/series?expand";
            var request = new RestRequest(apiUrl, Method.GET);

            var response = await this.SendRequestAsync<List<SeriesModel>>(request, string.Empty, apiUrl);

            if (response.IsSuccessful)
            {
                return response.Data;
            }

            List<SeriesModel> returnResponse = new List<SeriesModel>
            {
                new SeriesModel { }
            };

            return await Task.FromResult<List<SeriesModel>>(returnResponse);
        }

        public async Task<List<SeriesModel>> GetSeriesById(string seriesId)
        {
            var apiUrl = string.Format("/series/{0}", seriesId);
            var request = new RestRequest(apiUrl, Method.GET);

            var response = await this.SendRequestAsync<List<SeriesModel>>(request, string.Empty, apiUrl);

            if (response.IsSuccessful)
            {
                return response.Data;
            }

            List<SeriesModel> returnResponse = new List<SeriesModel>
            {
                new SeriesModel { }
            };

            return await Task.FromResult<List<SeriesModel>>(returnResponse);
        }

        public async Task<List<ImageSeriesModel>> GetImageSeriesByPatientId(string patientId)
        {
            var apiUrl = string.Format("/patients/{0}/series", patientId);
            var request = new RestRequest(apiUrl, Method.GET);

            var response = await this.SendRequestAsync<List<ImageSeriesModel>>(request, string.Empty, apiUrl);

            if (response.IsSuccessful)
            {
                if (response.Data != null)
                {
                    foreach (var item in response.Data)
                    {
                        item.InstancesCount = item.Instances.Count;
                        item.InstancesModel = new List<ImageSeriesInstanceModel>();

                        foreach (var inst in item.Instances)
                        {

                            var instanceModel = new ImageSeriesInstanceModel();

                            var preview = _appSettings.OrthancServiceUrl + "/instances/" + inst + "/preview";
                            var file = _appSettings.OrthancServiceUrl + "/instances/" + inst + "/file";

                            instanceModel.InstanceId = inst;
                            instanceModel.InstancePreview = preview;
                            instanceModel.InstanceFile = file;

                            item.InstancesModel.Add(instanceModel);
                        }
                        item.FirstInstanceId = item.Instances[0];
                        item.FirstInstanceModel = await this.GetInstanceById(item.FirstInstanceId);
                        item.FirstInstancePreview = _appSettings.OrthancServiceUrl + "/instances/" + item.Instances[0] + "/preview";
                        item.FirstInstanceFile = _appSettings.OrthancServiceUrl + "/instances/" + item.Instances[0] + "/file";
                        item.ParentStudyModel = await this.GetImageStudyById(item.ParentStudy);
                    }
                }

                return response.Data;
            }

            List<ImageSeriesModel> returnResponse = new List<ImageSeriesModel>
            {
                new ImageSeriesModel { }
            };

            return await Task.FromResult<List<ImageSeriesModel>>(returnResponse);
        }

        public async Task<List<ImageSeriesModel>> GetImageSeriesByStudyId(string studyId)
        {
            var apiUrl = string.Format("/studies/{0}/series?expand", studyId);
            var request = new RestRequest(apiUrl, Method.GET);

            var response = await this.SendRequestAsync<List<ImageSeriesModel>>(request, string.Empty, apiUrl);

            if (response.IsSuccessful)
            {
                if (response.Data != null)
                {
                    foreach (var item in response.Data)
                    {
                        item.InstancesCount = item.Instances.Count;
                        item.InstancesModel = new List<ImageSeriesInstanceModel>();

                        foreach (var inst in item.Instances)
                        {
                            var instanceModel = new ImageSeriesInstanceModel();

                            var preview = _appSettings.OrthancServiceUrl + "/instances/" + inst + "/preview";
                            var file = _appSettings.OrthancServiceUrl + "/instances/" + inst + "/file";

                            instanceModel.InstanceId = inst;
                            instanceModel.InstancePreview = preview;
                            instanceModel.InstanceFile = file;

                            item.InstancesModel.Add(instanceModel);
                        }
                        item.FirstInstanceId = item.Instances[0];
                        item.FirstInstanceModel = await this.GetInstanceById(item.FirstInstanceId);
                        item.FirstInstancePreview = _appSettings.OrthancServiceUrl + "/instances/" + item.Instances[0] + "/preview";
                        item.FirstInstanceFile = _appSettings.OrthancServiceUrl + "/instances/" + item.Instances[0] + "/file";
                        item.ParentStudyModel = await this.GetImageStudyById(item.ParentStudy);
                    }

                }

                var result = response.Data.Where(x => x.ParentStudy == studyId).ToList();
                return result;
            }

            List<ImageSeriesModel> returnResponse = new List<ImageSeriesModel>
            {
                new ImageSeriesModel { }
            };

            return await Task.FromResult<List<ImageSeriesModel>>(returnResponse);
        }

        public async Task<ImageStudyModel> GetImageStudyByStudyId(string studyId)
        {
            var apiUrl = string.Format("/studies/{0}/series?expand", studyId);
            var request = new RestRequest(apiUrl, Method.GET);

            var response = await this.SendRequestAsync<List<ImageSeriesModel>>(request, string.Empty, apiUrl);

            if (response.IsSuccessful)
            {
                if (response.Data != null)
                {
                    foreach (var item in response.Data)
                    {
                        item.InstancesCount = item.Instances.Count;
                        item.InstancesModel = new List<ImageSeriesInstanceModel>();

                        foreach (var inst in item.Instances)
                        {
                            var instanceModel = new ImageSeriesInstanceModel();

                            var preview = _appSettings.OrthancServiceUrl + "/instances/" + inst + "/preview";
                            var file = _appSettings.OrthancServiceUrl + "/instances/" + inst + "/file";

                            instanceModel.InstanceId = inst;
                            instanceModel.InstancePreview = preview;
                            instanceModel.InstanceFile = file;

                            item.InstancesModel.Add(instanceModel);
                        }
                        item.FirstInstanceId = item.Instances[0];
                        item.FirstInstanceModel = await this.GetInstanceById(item.FirstInstanceId);
                        item.FirstInstancePreview = _appSettings.OrthancServiceUrl + "/instances/" + item.Instances[0] + "/preview";
                        item.FirstInstanceFile = _appSettings.OrthancServiceUrl + "/instances/" + item.Instances[0] + "/file";
                        item.ParentStudyModel = await this.GetImageStudyById(item.ParentStudy);
                    }

                }
            }

            ImageStudyModel returnResponse = new ImageStudyModel();

            returnResponse.PatientName = response.Data.FirstOrDefault().ParentStudyModel.PatientMainDicomTags.PatientName;
            var studyDate = response.Data.FirstOrDefault().ParentStudyModel.MainDicomTags.StudyDate;

            if (studyDate != null)
            {
                returnResponse.StudyDate = DateTime.ParseExact(studyDate, "yyyyMMdd", null).ToString("yyyy-MM-dd");
            }

            returnResponse.Series = new List<ImageSeriesModel>();
            returnResponse.Series.AddRange(response.Data);

            return await Task.FromResult<ImageStudyModel>(returnResponse);
        }

        public async Task<InstanceModel> GetInstanceById(string instanceId)
        {
            var apiUrl = string.Format("/instances/{0}", instanceId);
            var request = new RestRequest(apiUrl, Method.GET);

            var response = await this.SendRequestAsync<InstanceModel>(request, string.Empty, apiUrl);

            if (response.IsSuccessful)
            {
                var preview = _appSettings.OrthancServiceUrl + "/instances/" + response.Data.ID + "/preview";
                var file = _appSettings.OrthancServiceUrl + "/instances/" + response.Data.ID + "/file";

                response.Data.File = file;
                response.Data.Preview = preview;

                return response.Data;
            }

            InstanceModel returnResponse = new InstanceModel();

            return await Task.FromResult<InstanceModel>(returnResponse);
        }

        public string GetInstancePreviewById(string instanceId, string hostUrl = "")
        {
            var apiUrl = string.Format("/instances/{0}/file", instanceId);

            var request = new RestRequest(apiUrl, Method.GET);
            request.AddHeader("Access-Control-Allow-Origin", "*");
            request.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3");
            request.AddHeader("Accept-Encoding", "gzip, deflate, br");

            var dcmPath = Path.Combine(PathToDicomImages, instanceId) + ".dcm";

            var response = _client.Execute(request);
            response.RawBytes.SaveAs(dcmPath);

            var hostdcm = hostUrl + "/dcm/" + instanceId + ".dcm";

            return hostdcm;

        }

        public async Task<object> GetDicomTagsById(string instanceId)
        {
            var apiUrl = string.Format("/instances/{0}/tags", instanceId);
            var request = new RestRequest(apiUrl, Method.GET);

            var response = await this.SendRequestAsync<object>(request, string.Empty, apiUrl);

            object jsonTags = JsonConvert.DeserializeObject(response.Content);
            return jsonTags;
        }

        public async Task<InstanceSegmentsModel> GetSegmentsByInstanceId(string instanceId, string hostUrl = "")
        {
            var apiUrl = string.Format("/instances/{0}/tags", instanceId);
            var request = new RestRequest(apiUrl, Method.GET);

            var response = await this.SendRequestAsync<List<Dictionary<string, InstanceTagsModel>>>(request, string.Empty, apiUrl);

            if (response.IsSuccessful)
            {
                var result = new InstanceSegmentsModel();
                var values = response.Data.SelectMany(x => x.Values).ToList();

                result.PatientName = values.SingleOrDefault(x => x.Name == "PatientName").Value;
                result.InstanceCreationDate = values.SingleOrDefault(x => x.Name == "InstanceCreationDate").Value;

                var instanceSegmentsPath = Path.Combine(PathToDicomSegments, instanceId) + "/segments/";

                if (Directory.Exists(instanceSegmentsPath))
                {
                    var instanceSegmentFiles = Directory.GetFiles(instanceSegmentsPath, "*.jpg");

                    foreach (var file in instanceSegmentFiles)
                    {
                        var segmentsModel = new SegmentsModel();

                        var fileName = Path.GetFileNameWithoutExtension(file);
                        var jpgFilePath = Path.Combine(instanceSegmentsPath, fileName) + ".jpg";

                        if (File.Exists(jpgFilePath))
                        {
                            var finalPath = hostUrl + "/segments/" + instanceId + "/segments/" + fileName + ".jpg";
                            segmentsModel.Name = fileName;
                            segmentsModel.Type = ".jpg";
                            segmentsModel.Url = finalPath;

                            result.Segments.Add(segmentsModel);
                        }
                    }
                    return result;
                }
                else
                {
                    var segmentsPath = Path.Combine(PathToDicomSegments);

                    if (Directory.Exists(segmentsPath))
                    {
                        var segmentFiles = Directory.GetFiles(segmentsPath, "*.jpg");

                        foreach (var file in segmentFiles)
                        {
                            var segmentsModel = new SegmentsModel();

                            var fileName = Path.GetFileNameWithoutExtension(file);
                            var jpgFilePath = Path.Combine(segmentsPath, fileName) + ".jpg";

                            if (File.Exists(jpgFilePath))
                            {
                                var finalPath = hostUrl + "/segments/" + fileName + ".jpg";
                                segmentsModel.Name = fileName;
                                segmentsModel.Type = ".jpg";
                                segmentsModel.Url = finalPath;

                                result.Segments.Add(segmentsModel);
                            }
                        }
                    }
                    return result;
                }
            }

            InstanceSegmentsModel returnResponse = new InstanceSegmentsModel();
            return await Task.FromResult<InstanceSegmentsModel>(returnResponse);
        }
    }
}
