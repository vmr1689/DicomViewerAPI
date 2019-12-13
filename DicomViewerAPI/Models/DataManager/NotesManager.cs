using DicomViewerAPI.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DicomViewerAPI.Models.DataManager
{
    public class NotesManager : IDataRepository<Notes>
    {
        readonly NotesContext _notesContext;

        public NotesManager(NotesContext context)
        {
            _notesContext = context;
        }

        public IEnumerable<Notes> GetAll()
        {
            return _notesContext.Notes.ToList();
        }

        public IEnumerable<Notes> GetByInstanceId(string id)
        {
            return _notesContext.Notes.Where(e => e.InstanceId == id).ToList();
        }

        public Notes GetByNotesId(long id)
        {
            return _notesContext.Notes.FirstOrDefault(e => e.NotesId == id);
        }

        public void Add(Notes entity)
        {
            _notesContext.Notes.Add(entity);
            _notesContext.SaveChanges();
        }

        public void Update(Notes notes, Notes entity)
        {
            notes.InstanceId = entity.InstanceId;
            notes.NotesSummary = entity.NotesSummary;

            _notesContext.SaveChanges();
        }

        public void Delete(Notes user)
        {
            _notesContext.Notes.Remove(user);
            _notesContext.SaveChanges();
        }
    }
}
