using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service
{
   public interface INoteService
    {
        Task<NoteDto> CreateText (NoteDto noteDto);
       
        Task<List<Note>> GetNotes();
    }
}
