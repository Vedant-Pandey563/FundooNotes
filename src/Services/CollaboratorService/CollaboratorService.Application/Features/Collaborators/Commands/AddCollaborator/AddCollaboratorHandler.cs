using Dapr.Client;
using MediatR;
using CollaboratorService.Application.Interfaces;
using CollaboratorService.Domain.Entites;

namespace CollaboratorService.Application.Features.Collaborators.Commands.AddCollaborator
{
    public class AddCollaboratorHandler : IRequestHandler<AddCollaboratorCommand, int>
    {
        private readonly ICollaboratorRepository _repo;
        private readonly DaprClient _daprClient;

        public AddCollaboratorHandler(
            ICollaboratorRepository repo,
            DaprClient daprClient)
        {
            _repo = repo;
            _daprClient = daprClient;
        }

        public async Task<int> Handle(AddCollaboratorCommand request, CancellationToken cancellationToken)
        {
            // Map the command correctly:
            // - OwnerUserId comes from the authenticated user in the controller
            // - CollaboratorUserId comes from the request DTO
            var collaborator = new Collaborator
            {
                NoteId = request.Dto.NoteId,
                OwnerUserId = request.OwnerUserId,
                CollaboratorUserId = request.Dto.CollaboratorUserId,
                CreatedAt = DateTime.UtcNow
            };

            var insertedId = await _repo.AddAsync(collaborator);

            // Publish a collaborator-added event for the email subscriber.
            // This is the event your Dapr pub/sub subscriber will receive.
            await _daprClient.PublishEventAsync(
                pubsubName: "pubsub",
                topicName: "collaborator.added",
                data: new
                {
                    CollaboratorId = insertedId,
                    NoteId = collaborator.NoteId,
                    OwnerUserId = collaborator.OwnerUserId,
                    CollaboratorUserId = collaborator.CollaboratorUserId
                },
                cancellationToken: cancellationToken);

            return insertedId;
        }
    }
}