using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Common.Commands
{
    public abstract class CommandHandler<TCommand, TResult>(IValidator<TCommand> validator, ILogger logger) : IRequestHandler<TCommand, TResult>
       where TCommand : Command<TResult>
    {
        private readonly IValidator<TCommand> _validator = validator;
        private readonly ILogger _logger = logger;

        public async Task<TResult> Handle(TCommand command, CancellationToken ct)
        {
            _logger.LogInformation($"Handling {command} – CorrelationId: {command.Context.CorrelationId}, CausationId: {command.Context.CausationId}",
                typeof(TCommand).Name,
                command.Context.CorrelationId,
                command.Context.CausationId);

            if (_validator is not null)
                await _validator.ValidateAndThrowAsync(command, ct);

            return await Execute(command, ct);
        }

        protected abstract Task<TResult> Execute(TCommand command, CancellationToken ct);
    }
}
