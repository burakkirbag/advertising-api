namespace Advertising.Application.Commands
{
    public abstract class CommandBase : ICommand
    {
    }

    public abstract class CommandBase<TResult> : ICommand<TResult>
    {

    }
}
