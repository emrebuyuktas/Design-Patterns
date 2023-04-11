namespace CompositePattern.Component;

public interface IComponent
{
    public int Id { get; set; }
    public string Name { get; set; }
    int Count();
    string Display();
}