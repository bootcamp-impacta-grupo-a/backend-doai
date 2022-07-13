using AutoMapper;
using DoaiApi.Data.DTOs;
using DoaiApi.Models;

namespace DoaiApi.Profiles
{
    public class InstituicaoProfile : Profile
    {
        public InstituicaoProfile()
        {
            CreateMap<CreateInstituicaoDto, Instituicao>();
        }
    }
}
