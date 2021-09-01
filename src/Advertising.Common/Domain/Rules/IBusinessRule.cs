namespace Advertising.Domain.Rules
{
    public interface IBusinessRule
    {
        string Message { get; }
        bool IsBroken();
    }
}
