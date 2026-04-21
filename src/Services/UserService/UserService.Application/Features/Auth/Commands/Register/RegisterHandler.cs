using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using UserService.Application.Interfaces;
using UserService.Domain.Entities;

namespace UserService.Application.Features.Auth.Commands.Register
{
    public class RegisterHandler : IRequestHandler<RegisterCommand, bool>
    {
        private readonly IUserRepository _repo;

        public RegisterHandler(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            // Hash password before storing 
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Dto.Password);

            // Create user entity
            var user = new User
            {
                FirstName = request.Dto.FirstName,
                LastName = request.Dto.LastName,
                Email = request.Dto.Email,
                PasswordHash = hashedPassword
            };

            // Save to database
            await _repo.CreateAsync(user);

            return true;
        }
    }
}
