using Weather_API.Core.DTO;

namespace Weather_API.Core.Interface
{
    public interface IAuthService
    {
        public Task<ResponseDTO<RegistrationResponseDTO>> Register(RegistrationDTO userDetails);
        public Task<ResponseDTO<CredentialResponseDTO>> Login(LoginDTO model);

    }
}