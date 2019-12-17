namespace DicomViewerAPI.Helpers
{
    public class AppSettings
    {
        public string Secret { get; set; }

        public string OrthancServiceUrl { get; set; }

        public string ImageSegmentServiceURL { get; set; }

        public string SegmentImagesZipPath { get; set; }

        public string InputImagePath { get; set; }
        public string DCMFilePath { get; set; }
    }
}
