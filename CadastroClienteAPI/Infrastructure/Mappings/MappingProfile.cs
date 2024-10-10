namespace CadastroClienteAPI.Infrastructure.Mappings
{
    using AutoMapper;
    using CadastroClienteAPI.Models.DTO;
    using CadastroClienteAPI.Models;

    namespace YourNamespace.Infrastructure.Mappings
    {
        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                // Mapeia Cliente para ClienteDTO
                CreateMap<Cliente, ClienteDTO>()
                    .ForMember(dest => dest.Enderecos, opt => opt.MapFrom(src => src.Enderecos))
                    .ForMember(dest => dest.Telefones, opt => opt.MapFrom(src => src.Telefones))
                    .ForMember(dest => dest.Emails, opt => opt.MapFrom(src => src.Emails));

                CreateMap<ClienteDTO, Cliente>()
                    .ForMember(dest => dest.Telefones, opt => opt.MapFrom(src => src.Telefones))
                    .ForMember(dest => dest.Enderecos, opt => opt.MapFrom(src => src.Enderecos))
                    .ForMember(dest => dest.Emails, opt => opt.MapFrom(src => src.Emails))
                    .ForMember(dest => dest.Id, opt => opt.Ignore());
                    
                CreateMap<Endereco, EnderecoDTO>();
                CreateMap<EnderecoDTO, Endereco>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore());
                                
                CreateMap<Telefone, TelefoneDTO>();
                CreateMap<TelefoneDTO, Telefone>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore());

                CreateMap<Email, EmailDTO>();
                CreateMap<EmailDTO, Email>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore());
             }
        }
    }

}
