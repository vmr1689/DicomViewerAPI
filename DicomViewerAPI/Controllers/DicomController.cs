using Dicom;
using Dicom.Network;
using DicomViewerAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace DicomViewerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DicomController : ControllerBase
    {
        private IPACSService _pacsService;
        private static readonly string PathToDicomImages = Path.Combine(@"D:\DicomViewerApi\dcm\");
        private static readonly string PathToDicomJpgImages = Path.Combine(@"D:\DicomViewerApi\jpg\");
        public DicomController(IPACSService pacsService)
        {
            _pacsService = pacsService;
        }

        [HttpGet("GetAllStudies")]
        public async Task<IActionResult> GetAllStudies()
        {
            var result = await this._pacsService.GetAllStudies();
            return Ok(result);
            //var request = (HttpWebRequest)WebRequest.Create("http://127.0.0.1:8042/studies?expand");

            //request.Method = "GET";
            ////request.UserAgent = RequestConstants.UserAgentValue;
            //request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;

            //var content = string.Empty;

            //using (var response = (HttpWebResponse)request.GetResponse())
            //{
            //    using (var stream = response.GetResponseStream())
            //    {
            //        using (var sr = new StreamReader(stream))
            //        {
            //            content = sr.ReadToEnd();
            //        }
            //    }
            //}

            // return Ok(content);
        }


        [HttpGet("GetAllPatients")]
        public async Task<IActionResult> GetAllPatients()
        {
            var result = await this._pacsService.GetAllPatients();
            return Ok(result);
            //var request = (HttpWebRequest)WebRequest.Create("http://127.0.0.1:8042/patients?expand");

            //request.Method = "GET";
            ////request.UserAgent = RequestConstants.UserAgentValue;
            //request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;

            //var content = string.Empty;

            //using (var response = (HttpWebResponse)request.GetResponse())
            //{
            //    using (var stream = response.GetResponseStream())
            //    {
            //        using (var sr = new StreamReader(stream))
            //        {
            //            content = sr.ReadToEnd();
            //        }
            //    }
            //}

            //return Ok(content);
        }

        [HttpGet("GetPatientStudies/{patientId}")]
        public async Task<IActionResult> GetPatientStudies(string patientId)
        {
            var result = await this._pacsService.GetPatientStudies(patientId);
            return Ok(result);
            //var request = (HttpWebRequest)WebRequest.Create(string.Format("http://127.0.0.1:8042/patients/{0}/studies", patientId));

            //request.Method = "GET";
            ////request.UserAgent = RequestConstants.UserAgentValue;
            //request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;

            //var content = string.Empty;

            //using (var response = (HttpWebResponse)request.GetResponse())
            //{
            //    using (var stream = response.GetResponseStream())
            //    {
            //        using (var sr = new StreamReader(stream))
            //        {
            //            content = sr.ReadToEnd();
            //        }
            //    }
            //}

            //return Ok(content);
        }

        [HttpGet("GetAllSeries")]
        public async Task<IActionResult> GetAllSeries()
        {
            var result = await this._pacsService.GetAllSeries();
            return Ok(result);
            //var request = (HttpWebRequest)WebRequest.Create("http://127.0.0.1:8042/series?expand");

            //request.Method = "GET";
            ////request.UserAgent = RequestConstants.UserAgentValue;
            //request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;

            //var content = string.Empty;

            //using (var response = (HttpWebResponse)request.GetResponse())
            //{
            //    using (var stream = response.GetResponseStream())
            //    {
            //        using (var sr = new StreamReader(stream))
            //        {
            //            content = sr.ReadToEnd();
            //        }
            //    }
            //}

            //return Ok(content);
        }

        [HttpGet("GetSeriesById/{seriesId}")]
        public async Task<IActionResult> GetSeriesById(string seriesId)
        {
            var result = await this._pacsService.GetSeriesById(seriesId);
            return Ok(result);
            //var request = (HttpWebRequest)WebRequest.Create(string.Format("http://127.0.0.1:8042/series/{0}", seriesId));

            //request.Method = "GET";
            ////request.UserAgent = RequestConstants.UserAgentValue;
            //request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;

            //var content = string.Empty;

            //using (var response = (HttpWebResponse)request.GetResponse())
            //{
            //    using (var stream = response.GetResponseStream())
            //    {
            //        using (var sr = new StreamReader(stream))
            //        {
            //            content = sr.ReadToEnd();
            //        }
            //    }
            //}

            //return Ok(content);
        }

        [HttpGet("GetImageSeriesByPatientId/{patientId}")]
        public async Task<ActionResult> GetImageSeriesByPatientId(string patientId)
        {
            var result = await this._pacsService.GetImageSeriesByPatientId(patientId);
            return Ok(result);
        }

        [HttpGet("GetImageSeriesByStudyId/{studyId}")]
        public async Task<ActionResult> GetImageSeriesByStudyId(string studyId)
        {
            var result = await this._pacsService.GetImageSeriesByStudyId(studyId);
            return Ok(result);
        }

        [HttpGet("GetImageStudyByStudyId/{studyId}")]
        public async Task<ActionResult> GetImageStudyByStudyId(string studyId)
        {
            var result = await this._pacsService.GetImageStudyByStudyId(studyId);
            return Ok(result);
        }

        [HttpGet("GetInstanceById/{instanceId}")]
        public async Task<ActionResult> GetInstanceById(string instanceId)
        {
            var result = await this._pacsService.GetInstanceById(instanceId);
            return Ok(result);
        }

        [HttpGet("GetInstanceById1/{instanceId}")]
        public ActionResult GetInstanceById1(string instanceId)
        {
            string hostUrl = string.Format("{0}://{1}", Request.Scheme, Request.Host);
            var result = this._pacsService.GetInstancePreviewById(instanceId, hostUrl);
            var aa = new { path = result};
            return Ok(aa);
        }

        [HttpGet("GetDicomTagsById/{instanceId}")]
        public async Task<ActionResult> GetDicomTagsById(string instanceId)
        {
            var result = await this._pacsService.GetDicomTagsById(instanceId);
            return Ok(result);
        }

        [HttpGet("GetSegmentsByInstanceId/{instanceId}")]
        public async Task<ActionResult> GetSegmentsByInstanceId(string instanceId)
        {
            string hostUrl = string.Format("{0}://{1}", Request.Scheme, Request.Host);
            var result = await this._pacsService.GetSegmentsByInstanceId(instanceId, hostUrl);
            return Ok(result);
        }
    }
}
