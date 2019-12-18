using Dicom.Imaging;
using DicomViewerAPI.Helpers;
using DicomViewerAPI.Models;
using Microsoft.Extensions.Options;
using RestSharp;
using RestSharp.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DicomViewerAPI.Services
{
    public class SegmentsService : ISegmentsService
    {
        private readonly AppSettings _appSettings;
        private readonly RestClient _client;

        public SegmentsService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _client = new RestClient(_appSettings.OrthancServiceUrl);

        }

        public async Task<string> SegmentImagesAsync(Segments segments)
        {
            string zipFilePath = _appSettings.SegmentImagesZipPath + segments.InstanceId + ".zip";
            if (File.Exists(zipFilePath)) File.Delete(zipFilePath);
            if (Directory.Exists(_appSettings.SegmentImagesZipPath + segments.InstanceId)) Directory.Delete(_appSettings.SegmentImagesZipPath + segments.InstanceId,true);
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), _appSettings.ImageSegmentServiceURL))
                {
                    string inputImgFile = _appSettings.InputImagePath + segments.InstanceId + ".jpg";
                    //string zipFilePath = _appSettings.SegmentImagesZipPath + segments.InstanceId + ".zip";
                    var fileStream = new FileStream(zipFilePath , FileMode.Create, FileAccess.Write, FileShare.None);

                    var multipartContent = new MultipartFormDataContent();
                    multipartContent.Add(new StringContent(segments.Segment), "segments");
                    multipartContent.Add(new ByteArrayContent(File.ReadAllBytes(inputImgFile)), "image", Path.GetFileName(inputImgFile));
                    request.Content = multipartContent;


                    var response = httpClient.SendAsync(request).Result;

                    //var stream = await response.Content.ReadAsStreamAsync();
                    //await stream.CopyToAsync(fileStream);

                    if (response.IsSuccessStatusCode)
                    {
                        using (HttpContent content = response.Content)
                        {
                            using (Stream contentStream = content.ReadAsStreamAsync().Result)
                            {
                                try
                                {
                                 await contentStream.CopyToAsync(fileStream);
                                }
                                catch (Exception e)
                                {
                                    throw;
                                    //return "Segmentation Error";
                                }
                                finally
                                {
                                    if (contentStream != null)
                                        contentStream.Dispose();
                                    if (fileStream != null)
                                        fileStream.Dispose();
                                    if (File.Exists(zipFilePath))
                                        ZipFile.ExtractToDirectory(zipFilePath, _appSettings.SegmentImagesZipPath + segments.InstanceId);
                                }
                            }
                        }

                    }


                }
            }
            return "Segmentation Success";
        }

        public string GetInstancePreviewById(string instanceId)
        {
            ImageManager.SetImplementation(WinFormsImageManager.Instance);
            var dcmFile = _appSettings.DCMFilePath + instanceId + ".dcm";
            var image = new DicomImage(dcmFile);
            var fileName = Path.GetFileNameWithoutExtension(dcmFile);
            var dcmJpg = Path.Combine(_appSettings.InputImagePath, fileName) + ".jpg";
            image.RenderImage().AsClonedBitmap().Save(dcmJpg);
            return dcmJpg;

        }
    }
}
