using System.Collections.Generic;

namespace DicomViewerAPI.Models.Orthanc
{
    public class InstanceSegmentsModel
    {
        public InstanceSegmentsModel()
        {
            this.Segments = new List<SegmentsModel>();
        }
        public string PatientName { get; set; }
        public string InstanceCreationDate { get; set; }
        public List<SegmentsModel> Segments { get; set; }
    }

    public class SegmentsModel
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Type { get; set; }
    }
}
