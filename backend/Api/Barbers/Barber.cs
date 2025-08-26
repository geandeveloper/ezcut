using Api.Barbers.Create;
using Api.Barbers.Events;
using Common.Events;
using Common.Models;

namespace Api.Barbers
{
    public class Barber : Aggregate
    {
        public Guid UserId { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }

        public static Barber Create(CreateBarberCommand command)
        {
            return Raise<Barber>(
                new BarberCreatedEvent(
                    AggregateId: Guid.NewGuid(),
                    UserId: Guid.NewGuid(),
                    Name: command.Name,
                    Email: command.Email,
                    Context: command.Context
                ));
        }

        protected override void Apply(IDomainEvent @event)
        {
        }
    }
}
