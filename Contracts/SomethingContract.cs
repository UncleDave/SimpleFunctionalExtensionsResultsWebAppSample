namespace ResultsWebAppSample.Contracts;

public class SomethingContract
{
    public string Name { get; }
    
    public SomethingContract(string value)
    {
        Name = value;
    }
}