using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DicomViewerAPI.Models
{
    public class Notes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long NotesId { get; set; }
        public string InstanceId { get; set; }
        public string NotesSummary { get; set; }
        
    }
}
