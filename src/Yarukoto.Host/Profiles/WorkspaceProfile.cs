using AutoMapper;
using Yarukoto.Data.Model;
using Yarukoto.Host.DTO;

namespace Yarukoto.Host.Profiles;

public class WorkspaceProfile : Profile
{
    public WorkspaceProfile()
    {
        CreateMap<WorkspaceDto, Workspace>()
            .ForMember(dest => dest.WorkspaceId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Notes, opt => opt.Ignore())
            .ReverseMap();
    }
}