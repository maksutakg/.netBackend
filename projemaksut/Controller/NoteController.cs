using Application.Service;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace projemaksut.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteService noteService;

        public NoteController(INoteService noteService)
        {
            this.noteService = noteService;
        }

        [HttpPost("createNote")]
        public async Task<ActionResult<Note>> CreateNote([FromBody]NoteDto noteDto)
        {
            await noteService.CreateText(noteDto);

            return Ok ();


        }
        [HttpGet]
        public async Task<ActionResult<List<Note>>> getNotes()
        {

            return Ok (await noteService.GetNotes());
        }


    }
}
