namespace ChainOfResponsibilityPattern.ChainOfResponsibility;

public interface IProcessHandler
{
    IProcessHandler SetNext(IProcessHandler handler);
    object Handle(object o);
}