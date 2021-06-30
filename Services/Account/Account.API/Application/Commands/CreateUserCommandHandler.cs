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
    // Regular CommandHandler
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserViewModel>
    {
        private readonly IMediator _mediator;
        private readonly IUserRepository _userRepository;

        private readonly ILogger<CreateUserCommandHandler> _logger;

        // Using DI to inject infrastructure persistence Repositories
        public CreateUserCommandHandler(IMediator mediator,
            IUserRepository userRepository,
            ILogger<CreateUserCommandHandler> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task<UserViewModel> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User(request.Name, request.Email, request.Password);

            _logger.LogInformation("----- Creating User - User: {@User}", user);

            user = _userRepository.Add(user);

            return Task.FromResult(UserViewModel.FromUser(user));
        }
    }
}
