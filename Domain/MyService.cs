using SimpleFunctionalExtensions;

namespace ResultsWebAppSample.Domain;

public class MyService
{
    public QueryResult<string, MyError> GetSomething(int id) =>
        id == 1
            ? QueryResult<string, MyError>.Ok("The first something")
            : QueryResult<string, MyError>.Fail(new MyError(MyErrorType.SomethingNotFound, "Failed to get the thing :("));

    public CommandResult<MyError> DoSomething(bool shouldDoItCorrectly) =>
        shouldDoItCorrectly
            ? CommandResult<MyError>.Ok()
            : CommandResult<MyError>.Fail(new MyError(MyErrorType.FailedToDoSomething, "Failed to do the thing :("));
}