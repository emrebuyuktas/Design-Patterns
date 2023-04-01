namespace ChainOfResponsibilityPattern.ChainOfResponsibility;

public class EmailProcessHandler<T>: ProcessHandler
{
    public override object Handle(object o)
    {
        Console.WriteLine("Sending email");
        return base.Handle(null);
    }
}