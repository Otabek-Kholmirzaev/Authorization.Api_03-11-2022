using Authorization.Api.Filters;
using Authorization.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Authorization.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Role("user")]
public class ProblemController : ControllerBase
{
    private readonly SolutionService _solutionService;

    public ProblemController(SolutionService solutionService)
    {
        _solutionService = solutionService;
    }

    [HttpGet]
    public IActionResult AddProblem(int n)
    {
        var answer = _solutionService.GetAnswer(n);

        return Ok(answer);
    }
}
