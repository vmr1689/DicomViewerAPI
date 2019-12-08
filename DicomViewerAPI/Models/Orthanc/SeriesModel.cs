using System.Collections.Generic;

namespace DicomViewerAPI.Models.Orthanc
{
    public class SeriesModel
    {
        public SeriesModel() 
        {
            this.MainDicomTags = new SeriesModelMainDicomTags();
        }

        public object ExpectedNumberOfInstances { get; set; }
        public string ID { get; set; }
        public List<string> Instances { get; set; }
        public bool IsStable { get; set; }
        public string LastUpdate { get; set; }
        public SeriesModelMainDicomTags MainDicomTags { get; set; }
        public string ParentStudy { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
    }
}
