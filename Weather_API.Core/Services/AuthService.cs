using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using Weather_API.Core.DTO;
using Weather_API.Core.Interface;
using Weather_API.Domain.Enum;
using Weather_API.Domain.Models;

namespace Weather_API.Core.Services
{
    public class AuthService: IAuthService
    {
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;


        public AuthService(IServiceProvider provider)
        {
            _userManager = provider.GetRequiredService<UserManager<AppUser>>();
            _tokenService = provider.GetRequiredService<ITokenService>();
            _mapper = provider.GetRequiredService<IMapper>();
        }

        public async Task<ResponseDTO<RegistrationResponseDTO>> Register(RegistrationDTO userDetails)
        {
            var checkEmail = await _userManager.FindByEmailAsync(userDetails.Email);
            if (checkEmail != null)
            {
                return ResponseDTO<RegistrationResponseDTO>.Fail("Email already Exists", (int)HttpStatusCode.BadRequest);
            }
            var userModel = _mapper.Map<AppUser>(userDetails);
            await _userManager.CreateAsync(userModel, userDetails.Password);
            await _userManager.AddToRoleAsync(userModel, Roles.User.ToString());
            return ResponseDTO<RegistrationResponseDTO>.Success("Registration Successful",
                new RegistrationResponseDTO { Id = userModel.Id, Email = userModel.Email },
                (int)HttpStatusCode.Created);
        }

        public async Task<ResponseDTO<CredentialResponseDTO>> Login(LoginDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return ResponseDTO<CredentialResponseDTO>.Fail("User does not exist", (int)HttpStatusCode.NotFound);
            }

            if (!await _userManager.CheckPasswordAsync(user, model.Password))
            {
                return ResponseDTO<CredentialResponseDTO>.Fail("Invalid user credential", (int)HttpStatusCode.BadRequest);
            }

            if (!user.IsActive)
            {
                return ResponseDTO<CredentialResponseDTO>.Fail("User's account is deactivated", (int)HttpStatusCode.BadRequest);
            }

            user.RefreshToken = _tokenService.GenerateRefreshToken();
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7); //sets refresh token for 7 days
            var credentialResponse = new CredentialResponseDTO()
            {
                Id = user.Id,
                Token = await _tokenService.GenerateToken(user),
                RefreshToken = user.RefreshToken
            };

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return ResponseDTO<CredentialResponseDTO>.Success("Login successful", credentialResponse);
            }
            return ResponseDTO<CredentialResponseDTO>.Fail("Failed to login user", (int)HttpStatusCode.InternalServerError);
        }
    }
}
