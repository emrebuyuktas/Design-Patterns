namespace ChainOfResponsibilityPattern.ChainOfResponsibility;

public class ExcelProcessHandler<T> : ProcessHandler
{
    public override object Handle(object o)
    {
        Console.WriteLine("Converting to Excel");
        return base.Handle(o);
    }
}