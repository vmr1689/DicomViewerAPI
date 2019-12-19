using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DicomViewerAPI.Models
{
    public class Segments
    {
        public string InstanceId { get; set; }
        public string Segment { get; set; }

        public bool IsThreshold { get; set; }
        public bool IsKMeans { get; set; }
        public bool IsRegionGrowth { get; set; }
        
    }
}
