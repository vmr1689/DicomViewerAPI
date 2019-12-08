using System.Collections.Generic;

namespace DicomViewerAPI.Models.Orthanc
{
    public class StudiesModel
    {
        public StudiesModel()
        {
            this.MainDicomTags = new StudiesModelMainDicomTags();
            this.PatientMainDicomTags = new StudiesModelPatientMainDicomTags();
            this.Series = new List<string>();
        }
        public string ID { get; set; }
        public bool IsStable { get; set; }
        public string LastUpdate { get; set; }
        public StudiesModelMainDicomTags MainDicomTags { get; set; }
        public string ParentPatient { get; set; }
        public StudiesModelPatientMainDicomTags PatientMainDicomTags { get; set; }
        public List<string> Series { get; set; }
        public string Type { get; set; }
    }
}
