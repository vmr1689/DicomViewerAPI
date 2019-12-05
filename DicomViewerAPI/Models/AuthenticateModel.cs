using System.ComponentModel.DataAnnotations;

namespace DicomViewerAPI.Models
{
    public class AuthenticateModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
