namespace ResultsWebAppSample.Domain;

public enum MyErrorType
{
    FailedToDoSomething,
    SomethingNotFound
}

public class MyError
{
    public MyErrorType Type { get; }
    
    public string Message { get; }

    public MyError(MyErrorType type, string message)
    {
        Type = type;
        Message = message;
    }
}