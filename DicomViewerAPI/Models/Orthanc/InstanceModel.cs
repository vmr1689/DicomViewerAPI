using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DicomViewerAPI.Models.Orthanc
{
    public class InstanceModelMainDicomTags
    {
        public string AcquisitionNumber { get; set; }
        public string ImageOrientationPatient { get; set; }
        public string ImagePositionPatient { get; set; }
        public string InstanceCreationDate { get; set; }
        public string InstanceCreationTime { get; set; }
        public string InstanceNumber { get; set; }
        public string SOPInstanceUID { get; set; }
    }

    public class InstanceModel
    {
        public int FileSize { get; set; }
        public string FileUuid { get; set; }
        public string ID { get; set; }
        public string Preview { get; set; }
        public string File { get; set; }
        public int IndexInSeries { get; set; }
        public InstanceModelMainDicomTags MainDicomTags { get; set; }
        public string ParentSeries { get; set; }
        public string Type { get; set; }
    }
}
