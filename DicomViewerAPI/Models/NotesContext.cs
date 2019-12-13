using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DicomViewerAPI.Models
{
    public class NotesContext : DbContext
    {
        public NotesContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Notes> Notes { get; set; }
    }
}
