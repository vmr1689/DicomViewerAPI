using System.Collections.Generic;

namespace DicomViewerAPI.Models.Orthanc
{
    public class ImageSeriesModel
    {
        public ImageSeriesModel()
        {
            this.MainDicomTags = new ImageSeriesModelMainDicomTags();
            this.ParentStudyModel = new ImageSeriesParentStudyModel();
            this.InstancesModel = new List<ImageSeriesInstanceModel>();
            this.FirstInstanceModel = new InstanceModel();
        }

        public object ExpectedNumberOfInstances { get; set; }
        public string ID { get; set; }
        public List<string> Instances { get; set; }
        public string FirstInstanceId { get; set; }
        public string FirstInstancePreview { get; set; }
        public InstanceModel FirstInstanceModel { get; set; }
        public string FirstInstanceFile { get; set; }
        public List<ImageSeriesInstanceModel> InstancesModel { get; set; }
        public int InstancesCount { get; set; }
        public bool IsStable { get; set; }
        public string LastUpdate { get; set; }
        public ImageSeriesModelMainDicomTags MainDicomTags { get; set; }
        public string ParentStudy { get; set; }
        public ImageSeriesParentStudyModel ParentStudyModel { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
    }
}
