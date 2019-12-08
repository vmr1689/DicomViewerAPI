namespace DicomViewerAPI.Models.Orthanc
{
    public class ImageSeriesModelMainDicomTags
    {
        public string BodyPartExamined { get; set; }
        public string ImageOrientationPatient { get; set; }
        public string Manufacturer { get; set; }
        public string Modality { get; set; }
        public string PerformedProcedureStepDescription { get; set; }
        public string ProtocolName { get; set; }
        public string SequenceName { get; set; }
        public string SeriesDate { get; set; }
        public string SeriesDescription { get; set; }
        public string SeriesInstanceUID { get; set; }
        public string SeriesNumber { get; set; }
        public string SeriesTime { get; set; }
    }
}
