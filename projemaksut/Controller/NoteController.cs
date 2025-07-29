using Application.Service;
using AutoMapper;
using Domain.Entities;
using Domain.Request;
using FluentValidation;
using Infrastructure.Exception;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using Persistence.Context;
using System.Collections.Generic;
using ValidationException = Infrastructure.Exception.ValidationException;

namespace projemaksut.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteService noteService;
        private readonly AppDbContext _context;
        private readonly IMapper _map;
        private readonly IValidator<Note> validator;


        public NoteController(INoteService noteService, AppDbContext context, IMapper map, IValidator<Note> validator = null)
        {
            this.noteService = noteService;
            _context = context;
            _map = map;
            this.validator = validator;
        }

        [HttpPost("CreateNote")]
        public async Task<ActionResult<Note>> CreateNote([FromBody]NoteDto noteDto)
        {
            var note = _map.Map<Note>(noteDto);
            var validationResult = await validator.ValidateAsync(note);
            var errormessage = string.Join("; ", validationResult.Errors);
            if (!validationResult.IsValid)
            {
            throw new ValidationException(errormessage);
            }
            var created = await noteService.CreateText(noteDto);
            return Ok(created);

        }
        [HttpGet("GetNotesByUserId")]
        public async Task<ActionResult<List<NoteDto>>> getNoteById(int id)
        {
            
            var result = await noteService.GetNoteByUserId(id);
            
            if (result == null)
            {
                throw new NotFoundException("id not found ");
            } 
            return Ok(result);
        }

        [HttpPut("UpdateNote")]
        
        public async Task<ActionResult<NoteDto>> updateNote(UpdateNoteRequest updateNote) 
        {
               var user = _map.Map<Note>(updateNote);
               var validationResult =await validator.ValidateAsync(user);
               var errormessage = string.Join("; ", validationResult.Errors);
               if (!validationResult.IsValid) { return await noteService.UpdateNote(updateNote); }
                throw new ValidationException(errormessage);
               
             
       }


        
        [HttpDelete("DeleteNote")]
        
        public async Task<ActionResult<UserDto>> deleteUser(int id)
        {

            noteService.DeleteNote(id);
            return null;
        }
        

      


    }
}
