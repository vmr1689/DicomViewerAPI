using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net;

namespace DicomViewerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DicomController : ControllerBase
    {
        [HttpGet("GetAllStudiesFromOrthanc")]
        public IActionResult GetAllStudiesFromOrthanc()
        {
            var request = (HttpWebRequest)WebRequest.Create("http://127.0.0.1:8042/studies?expand");

            request.Method = "GET";
            //request.UserAgent = RequestConstants.UserAgentValue;
            request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;

            var content = string.Empty;

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (var stream = response.GetResponseStream())
                {
                    using (var sr = new StreamReader(stream))
                    {
                        content = sr.ReadToEnd();
                    }
                }
            }

            return Ok(content);
        }


        [HttpGet("GetAllPatientsFromOrthanc")]
        public IActionResult GetAllPatientsFromOrthanc()
        {
            var request = (HttpWebRequest)WebRequest.Create("http://127.0.0.1:8042/patients?expand");

            request.Method = "GET";
            //request.UserAgent = RequestConstants.UserAgentValue;
            request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;

            var content = string.Empty;

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (var stream = response.GetResponseStream())
                {
                    using (var sr = new StreamReader(stream))
                    {
                        content = sr.ReadToEnd();
                    }
                }
            }

            return Ok(content);
        }

        [HttpGet("GetStudiesFromOrthanc/{patientId}")]
        public IActionResult GetStudiesFromOrthanc(string patientId)
        {
            var request = (HttpWebRequest)WebRequest.Create(string.Format("http://127.0.0.1:8042/patients/{0}/studies", patientId));

            request.Method = "GET";
            //request.UserAgent = RequestConstants.UserAgentValue;
            request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;

            var content = string.Empty;

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (var stream = response.GetResponseStream())
                {
                    using (var sr = new StreamReader(stream))
                    {
                        content = sr.ReadToEnd();
                    }
                }
            }

            return Ok(content);
        }
    }
}
