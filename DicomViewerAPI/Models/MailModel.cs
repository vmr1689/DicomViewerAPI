using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DicomViewerAPI.Models
{
    public class MailModel
    {
        public string ToAddress { get; set; }
        public string FromAddress { get; set; }
        public string BodyMessage { get; set; }
        public string Subject { get; set; }
    }
}
