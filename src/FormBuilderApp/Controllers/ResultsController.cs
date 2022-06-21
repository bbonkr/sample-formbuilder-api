using FormBuilder.Domains.Results.Commands.AddResult;
using FormBuilder.Domains.Results.Models;
using FormBuilder.Domains.Results.Queries.GetResultById;
using FormBuilder.Domains.Results.Queries.GetResults;
using kr.bbon.AspNetCore;
using kr.bbon.AspNetCore.Models;
using kr.bbon.AspNetCore.Mvc;
using kr.bbon.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FormBuilderApp.Controllers;

[ApiController]
[Route(DefaultValues.RouteTemplate)]
[Area(DefaultValues.AreaName)]
[ApiVersion(DefaultValues.ApiVersion)]
[Produces("application/json")]
public class ResultsController : ApiControllerBase
{
    public ResultsController(IMediator mediator, ILogger<ResultsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Get results
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(PagedModel<ResultModel>), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ApiResponseModel<ErrorModel>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponseModel<ErrorModel>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetResults([FromQuery] GetResultsQuery query)
    {
        var results = await _mediator.Send(query);

        return Ok(results);
    }

    /// <summary>
    /// Get result by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ResultModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponseModel<ErrorModel>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponseModel<ErrorModel>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponseModel<ErrorModel>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetResultById([FromRoute] Guid id)
    {
        var query = new GetResultByIdQuery(id);

        var result = await _mediator.Send(query);

        return Ok(result);
    }

    /// <summary>
    /// Add result
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(ResultModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponseModel<ErrorModel>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponseModel<ErrorModel>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddResult([FromBody] AddResultCommand command)
    {
        var result = await _mediator.Send(command);

        return Accepted(result);
    }

    private readonly IMediator _mediator;
    private readonly ILogger _logger;
}