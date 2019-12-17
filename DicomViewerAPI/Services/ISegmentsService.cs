using DicomViewerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DicomViewerAPI.Services
{
    public interface ISegmentsService
    {
        Task<string> SegmentImagesAsync(Segments segments);
        string GetInstancePreviewById(string instanceId);
    }
}
