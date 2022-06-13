using FormBuilder.Domains.Translation.Models;
using FormBuilder.Domains.Translation.Queries.GetTranslatedText;
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
public class TranslationsController : ApiControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger _logger;
    
    public TranslationsController(IMediator mediator, ILogger<TranslationsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Translate text
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(TranslatedModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponseModel<ErrorModel>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponseModel<ErrorModel>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Translate([FromBody] GetTranslatedTextQuery query)
    {
        var result = await _mediator.Send(query);

        return Ok(result);
    }
}