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
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserUpdateViewModel>
    {
        private readonly IMediator _mediator;
        private readonly IUserRepository _userRepository;

        private readonly ILogger<UpdateUserCommandHandler> _logger;

        // Using DI to inject infrastructure persistence Repositories
        public UpdateUserCommandHandler(IMediator mediator,
            IUserRepository userRepository,
            ILogger<UpdateUserCommandHandler> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public Task<UserUpdateViewModel> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User(request.Name, request.Email, request.Password);
            
            _logger.LogInformation("----- Updating User - User: {@User}", user);

            user = _userRepository.Update(request.IdentityUser, user);

            return Task.FromResult(UserUpdateViewModel.FromUser(user));
        }
    }
}
