using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DicomViewerAPI.Models;
using DicomViewerAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DicomViewerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SegmentsController : ControllerBase
    {
        private ISegmentsService _segmentsService;
        private static readonly string segmentsImagePath = Path.Combine(@"D:\POC\Latest\DicomViewer\Api\DicomViewerAPI\DicomViewerAPI\Images\segments\");
        public SegmentsController(ISegmentsService segmentsService)
        {
            _segmentsService = segmentsService;
        }
        [HttpPost]
        public async Task<ActionResult> SegmentImages([FromBody] Segments segments)
        {
            var jpgFile = this._segmentsService.GetInstancePreviewById(segments.InstanceId);
            var segmentedResult = await this._segmentsService.SegmentImagesAsync(segments);
            var result = new { SegmentStatus = segmentedResult };
            return Ok(result);
        }
    }
}