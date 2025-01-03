using AutoMapper;
using Yarukoto.Data.Model;
using Yarukoto.Host.DTO;

namespace Yarukoto.Host.Profiles;

public class NoteProfile : Profile
{
    public NoteProfile()
    {
        CreateMap<NoteDto, Note>()
            .ForMember(dest => dest.NoteId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Workspace, opt => opt.Ignore())
            .ReverseMap();
    }
}