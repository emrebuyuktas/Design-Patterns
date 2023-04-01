namespace ChainOfResponsibilityPattern.ChainOfResponsibility;

public class ZipProcessHandler<T>: ProcessHandler
{
    public override object Handle(object o)
    {
        Console.WriteLine("Converting to Zip");
        return base.Handle(o);
    }
}