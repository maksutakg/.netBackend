using AutoMapper;
using Domain.Entities;
using FluentValidation;
using Infrastructure.Mapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Persistence.Context;
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

        public NoteService(AppDbContext context, IMapper mapper = null)
        {

            _context = context;
            _mapper = mapper;
        }

        public async Task<NoteDto> CreateText(NoteDto noteDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == noteDto.UserId);
            if (user != null)
            {
               var note = _mapper.Map<Note>(noteDto);
                await _context.Notes.AddAsync(note);
                await _context.SaveChangesAsync();
                  return _mapper.Map<NoteDto>(note);
             
          
            }
            return null;
        }

        public async Task<List<Note>> GetNotes()
        {
            var notes = await _context.Notes.ToListAsync();
            return notes;
        }
    }
}
