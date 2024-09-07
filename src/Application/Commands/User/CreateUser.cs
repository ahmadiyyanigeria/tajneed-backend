using FluentValidation;
using MediatR;

namespace Application.Commands.User;

public class CreateUser
{
    public record Command : IRequest<string>
    {

    }

    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {

        }
    }
}