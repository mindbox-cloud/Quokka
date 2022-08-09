namespace Mindbox.Quokka;

public interface ICallContextContainer
{
    public TCallContext GetCallContext<TCallContext>() where TCallContext : class;
}