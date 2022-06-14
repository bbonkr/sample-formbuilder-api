using FormBuilder.Domains.Languages.Models;
using FormBuilder.Domains.Languages.Queries.GetLanguages;
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
public class LanguagesController : ApiControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger _logger;
    
    public LanguagesController(IMediator mediator, ILogger<LanguagesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }   
    
    /// <summary>
    /// Get languages
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(PagedModel<LanguageModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponseModel<ErrorModel>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponseModel<ErrorModel>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetLanguages([FromQuery] GetLanguagesQuery query)
    {
        var result = await _mediator.Send(query);

        return Ok(result);
    }
}