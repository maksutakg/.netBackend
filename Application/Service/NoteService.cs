using AutoMapper;
using Domain.Entities;
using Domain.Request;
using FluentValidation;
using Infrastructure.Exception;
using Infrastructure.Mapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Persistence.Context;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service
{
    public class NoteService : INoteService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;


        public NoteService(AppDbContext context, IMapper mapper )
        {

            _context = context;
            _mapper = mapper;
        }

        public async Task<NoteDto> CreateText(NoteDto noteDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == noteDto.UserId);
            if (user != null)
            {
                Log.Information($"Note Created byId: {noteDto.UserId}");
               var note = _mapper.Map<Note>(noteDto);
                await _context.Notes.AddAsync(note);
                await _context.SaveChangesAsync();
                return _mapper.Map<NoteDto>(note);
             
          
            }
            throw new NotFoundException("bu userId bulunamadı");
        }

        public async Task<bool> DeleteNote(int id)
        {
            var note = await _context.Notes.FindAsync(id);
            if (note == null)
            {
                throw new NotFoundException("note bulunamadı");

            }
            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<NoteDto>> GetNoteByUserId(int id)
        {
            Log.Information($"get not by user ıd ; {id}");
            var user = await _context.Users.FindAsync(id);
            var query = _context.Notes.AsQueryable();
            if ( user != null)
            {
                query = query.Where(u=>u.UserId == id);
                var notes = await query.ToListAsync();
                return _mapper.Map<List<NoteDto>>(notes);
            }
            throw new NotFoundException("user bulunamadı");
        }
   
 
        public async Task<List<Note>> GetNotes()
        {
            var notes = await _context.Notes.ToListAsync();
            return notes;
        }

        public async Task<NoteDto> UpdateNote(UpdateNoteRequest updateNote)
        {
             Log.Information($"Updated Note: {updateNote.Id}");
             var existingNote = await _context.Notes.FindAsync(updateNote.Id);
            if (existingNote!=null)
            {
                _mapper.Map(updateNote, existingNote);
                await _context.SaveChangesAsync();
                return _mapper.Map<NoteDto>(existingNote);
            }
             throw new NotFoundException("noteId bulun");
        }
    }
}
