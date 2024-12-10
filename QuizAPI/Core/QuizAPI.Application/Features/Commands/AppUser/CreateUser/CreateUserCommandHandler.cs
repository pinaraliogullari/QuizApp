using MediatR;
using Microsoft.AspNetCore.Identity;

namespace QuizAPI.Application.Features.Commands.AppUser.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        private readonly UserManager<Domain.Entities.AppUser> _userManager;

        public CreateUserCommandHandler(UserManager<Domain.Entities.AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            var existingUserByEmail = await _userManager.FindByEmailAsync(request.Email);
            var existingUserByUsername = await _userManager.FindByNameAsync(request.UserName);

            CreateUserCommandResponse response = new(false, "");

            if (existingUserByEmail != null)
            {
                response = response with { IsSuccessful = false, Message = "This email address is already in use." };
                return response;
            }

            if (existingUserByUsername != null)
            {
                response = response with { IsSuccessful = false, Message = "This username is already taken." };
                return response;
            }
            var newUser = new Domain.Entities.AppUser()
            {
                Id = Guid.NewGuid().ToString(),
                Email = request.Email,
                UserName = request.UserName
            };

            IdentityResult result = await _userManager.CreateAsync(newUser, request.Password);

            if (result.Succeeded)
            {
                response = response with { IsSuccessful = true, Message = "User has been created successfully." };
            }
            else
            {
                response = response with { IsSuccessful = false, Message = "User creation failed. Errors:\n" };
                foreach (var error in result.Errors)
                {
                    response = response with { Message = response.Message + $"{error.Code}: {error.Description}\n" };
                }
            }

            return response;
        }
    }
}