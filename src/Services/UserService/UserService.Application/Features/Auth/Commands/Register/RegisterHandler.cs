using MediatR;
using UserService.Application.Events;
using UserService.Application.Interfaces;
using UserService.Domain.Entities;

namespace UserService.Application.Features.Auth.Commands.Register
{
    public class RegisterHandler : IRequestHandler<RegisterCommand, bool>
    {
        private readonly IUserRepository _repo;
        private readonly IMessagePublisher _publisher;

        public RegisterHandler(IUserRepository repo, IMessagePublisher publisher)
        {
            _repo = repo;
            _publisher = publisher;
        }

        public async Task<bool> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            // Normalize the email so duplicate checks are consistent.
            var email = request.Dto.Email.Trim().ToLowerInvariant();

            // Prevent duplicate registration.
            var existingUser = await _repo.GetByEmailAsync(email);
            if (existingUser is not null)
            {
                return false;
            }

            // Hash password before persisting it.
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Dto.Password);

            var user = new User
            {
                FirstName = request.Dto.FirstName.Trim(),
                LastName = request.Dto.LastName.Trim(),
                Email = email,
                PasswordHash = hashedPassword
            };

            await _repo.CreateAsync(user);

            // Publish a domain event after the database write succeeds.
            await _publisher.PublishAsync(new UserRegisteredEvent
            {
                Email = user.Email,
                FirstName = user.FirstName
            }, "user.registered", cancellationToken);

            return true;
        }
    }
}