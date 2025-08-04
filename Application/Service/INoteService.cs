using Domain.Entities;
using Domain.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service
{
   public interface INoteService
    {
        Task<NoteDto> CreateText (CreateNoteRequest createNote);
        Task<List<NoteDto>> GetNoteByUserId(int id);
        Task<List<Note>> GetNotes();
        Task<NoteDto> UpdateNote(UpdateNoteRequest updateNote);
        Task<bool> DeleteNote(int id);
        Task<List<NoteDto>> FiltreNotes(string note);
    }
}
