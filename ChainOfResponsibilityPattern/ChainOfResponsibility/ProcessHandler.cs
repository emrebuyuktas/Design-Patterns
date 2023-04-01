namespace ChainOfResponsibilityPattern.ChainOfResponsibility;

public abstract class ProcessHandler : IProcessHandler
{
    
    private IProcessHandler _nextHandler;
    
    public IProcessHandler SetNext(IProcessHandler handler)
    {
        _nextHandler = handler;
        return _nextHandler;
    }

    public virtual object Handle(object o)
    {
        return _nextHandler != null ? _nextHandler.Handle(o) : null;
    }
}