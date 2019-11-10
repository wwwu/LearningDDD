using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LearningDDD.Domain.Core.Bus;
using LearningDDD.Domain.Core.Commands;
using MediatR;

namespace LearningDDD.Infrastructure.Data.Bus
{
    public sealed class InMemoryBus : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public InMemoryBus(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Unit> SendCommand<T>(T command) where T : BaseCommand
        {
            return await _mediator.Send(command);
        }
    }
}
