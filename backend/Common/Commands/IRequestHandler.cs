namespace Common.Commands
{
    public interface IRequestHandler<TCommand, TResult> where TCommand : Command<TResult>
    {
    }
}