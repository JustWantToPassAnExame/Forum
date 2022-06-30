using AutoMapper;
using Business.Models;
using Data.Entities;
using System.Linq;

namespace Business
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<Comment, CommentModel>()
                .ForMember(cm => cm.Id, x => x.MapFrom(c => c.Id))
                .ForMember(cm => cm.ThreadId, x => x.MapFrom(c => c.ThreadId))
                .ForMember(cm => cm.UserId, x => x.MapFrom(c => c.UserId))
                .ForMember(cm => cm.CreationDate, x => x.MapFrom(c => c.CreationDate))
                .ForMember(cm => cm.CommentText, x => x.MapFrom(c => c.CommentText))
                .ReverseMap();

             }
    }
}
