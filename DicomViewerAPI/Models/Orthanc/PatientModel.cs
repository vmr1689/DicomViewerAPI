using System.Collections.Generic;

namespace DicomViewerAPI.Models.Orthanc
{
    public class PatientModel
    {
        public PatientModel()
        {
            this.MainDicomTags = new PatientModelMainDicomTags();
        }
        public string ID { get; set; }
        public bool IsStable { get; set; }
        public string LastUpdate { get; set; }
        public PatientModelMainDicomTags MainDicomTags { get; set; }
        public List<string> Studies { get; set; }
        public string Type { get; set; }
    }
}
