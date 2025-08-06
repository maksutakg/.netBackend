using Application.Service;
using AutoMapper;
using Domain.Entities;
using Domain.Request;
using FluentValidation;
using Infrastructure.Exception;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using Persistence.Context;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
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

        [Authorize]
        [HttpPost("CreateNote")]
        public async Task<ActionResult<Note>> CreateNote([FromBody]CreateNoteRequest noteRequest)
        {
            var TokenUserId =User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (TokenUserId==null)
            {
                throw new NotFoundException("token sorunu");
            }
            var user = await _context.Users.FindAsync(int.Parse(TokenUserId));
            var note = _map.Map<Note>(noteRequest);
            var validationResult = await validator.ValidateAsync(note);
            var errormessage = string.Join("; ", validationResult.Errors);
            if (!validationResult.IsValid)
            {
            throw new ValidationException(errormessage);
            }
            var created = await noteService.CreateText(user.Id,noteRequest);
            return Ok(created);

        }
        [Authorize]
        [HttpGet("GetUserNotesById")]
        public async Task<ActionResult<List<NoteDto>>> getNoteById(int id)
        {
            
            var result = await noteService.GetNoteByUserId(id);
            
            if (result == null)
            {
                throw new NotFoundException("id not found ");
            } 
            return Ok(result);
        }

        [Authorize]
        [HttpPut("UpdateNote")]
        
        public async Task<ActionResult<NoteDto>> updateNote(UpdateNoteRequest updateNote) 
        {
            
            var userNote = await noteService.FindNoteById(updateNote.NoteId);
            if (userNote==null)
            {
                throw new NotFoundException("not bulunamadı");
            }
            var tokenUser = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (userNote.UserId.ToString() == tokenUser)
            {
                return await noteService.UpdateNote(updateNote);
            }
            throw new NotFoundException("bu not sizin değil ");

       }

        [Authorize]
        [HttpDelete("DeleteNote")]
        public async Task<ActionResult> DeleteNote(int id)
        {
            var note = await _context.Notes.FindAsync(id);
            if (note==null)
            {
                throw new NotFoundException("note bulunamadı");
            }
            await noteService.DeleteNote(id);
            return Ok();
        }
     

        [Authorize]
        [HttpGet("FiltreNot")]
        public async Task <ActionResult<List<NoteDto>>> FiltreNote([FromQuery] string text)
        {

             return await noteService.FiltreNotes(text);
            

        }

    }





}
