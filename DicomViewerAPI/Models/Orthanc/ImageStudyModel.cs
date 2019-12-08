using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DicomViewerAPI.Models.Orthanc
{
    public class ImageStudyModel
    {
        public string PatientName { get; set; }
        public string StudyDate { get; set; }
        public List<ImageSeriesModel> Series { get; set; }
    }
}
