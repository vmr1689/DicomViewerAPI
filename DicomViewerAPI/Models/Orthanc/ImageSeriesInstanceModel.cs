using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DicomViewerAPI.Models.Orthanc
{
    public class ImageSeriesInstanceModel
    {
        public string InstanceId { get; set; }
        public string InstancePreview { get; set; }
        public string InstanceFile { get; set; }
    }
}
