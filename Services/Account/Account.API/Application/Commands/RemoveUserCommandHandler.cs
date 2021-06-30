using Account.API.ViewModel;
using Account.Domain.AggregatesModel.UserAggregates;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Account.API.Application.Commands
{
    public class RemoveUserCommandHandler : IRequestHandler<RemoveUserCommand, bool>
    {

        private readonly IMediator _mediator;
        private readonly IUserRepository _userRepository;

        private readonly ILogger<UpdateUserCommandHandler> _logger;

        public RemoveUserCommandHandler(IMediator mediator, IUserRepository userRepository, ILogger<UpdateUserCommandHandler> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task<bool> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("----- Removing User - User: {@User}", request);

            var userToUpdate = _userRepository.Remove(request.IdentityUser);
            return Task.FromResult(userToUpdate);
        }
    }
}
