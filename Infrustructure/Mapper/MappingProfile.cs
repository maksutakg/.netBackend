using AutoMapper;
using Domain.Entities;
using Domain.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static Azure.Core.HttpHeader;

namespace Infrastructure.Mapper
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, CreateUserRequest>().ReverseMap();
            CreateMap<Note,NoteDto>().ReverseMap();
    
            CreateMap<NoteDto, UpdateNoteRequest>().ReverseMap();
            CreateMap<NoteDto, CreateUserRequest>().ReverseMap();
            CreateMap<UpdateNoteRequest, Note>().ReverseMap();
            CreateMap<User, UpdateUserRequest>().ReverseMap();
            CreateMap<Mahalle,MahalleDto>().ReverseMap();
            CreateMap<CreateNoteRequest,Note>().ReverseMap();




        }
    }
}
