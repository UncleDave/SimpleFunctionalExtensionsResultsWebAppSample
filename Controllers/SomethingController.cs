using Microsoft.AspNetCore.Mvc;
using ResultsWebAppSample.Contracts;
using ResultsWebAppSample.Domain;
using ResultsWebAppSample.Infrastructure;
using SimpleFunctionalExtensions;

namespace ResultsWebAppSample.Controllers;

[ApiController]
[Route("api/somethings")]
[UnwrapResult]
public class SomethingController
{
    private readonly MyService _myService;

    public SomethingController(MyService myService)
    {
        _myService = myService;
    }

    [HttpGet("{id:int}")]
    public QueryResult<string, MyError> Get(int id)
    {
        return _myService.GetSomething(id);
    }

    [HttpPost]
    public CommandResult<MyError> Post(bool correctly)
    {
        return _myService.DoSomething(correctly);
    }
    
    [HttpGet("{id:int}/resilient")]
    public QueryResult<string, MyError> GetResiliently(int id)
    {
        // Catch is called only when the result is a failure.
        // Catch expects a QueryResult to be returned, but T can be implicitly converted to QueryResult<T>.
        return _myService.GetSomething(id).Catch(error => $"Something called \"{error.Message}\"");
    }

    [HttpPost("resilient")]
    public CommandResult<MyError> PostResiliently(bool correctly)
    {
        return _myService.DoSomething(correctly).Catch(_ => CommandResult<MyError>.Ok());
    }
    
    [HttpGet("{id:int}/mapped")]
    public QueryResult<SomethingContract, MyError> GetMapped(int id)
    {
        // Map is called only when the result is a success.
        return _myService.GetSomething(id).Map(value => new SomethingContract(value));
    }
}