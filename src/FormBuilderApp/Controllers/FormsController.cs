using FormBuilder.Domains.Forms.Commands.AddForm;
using FormBuilder.Domains.Forms.Commands.DeleteForm;
using FormBuilder.Domains.Forms.Commands.UpdateForm;
using FormBuilder.Domains.Forms.Queries.GetFormById;
using FormBuilder.Domains.Forms.Queries.GetForms;
using kr.bbon.AspNetCore;
using kr.bbon.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FormBuilderApp.Controllers;

[ApiController]
[Route(DefaultValues.RouteTemplate)]
[Area(DefaultValues.AreaName)]
[ApiVersion(DefaultValues.ApiVersion)]
[Produces("application/json")]
public class FormsController : ApiControllerBase
{
    public FormsController(IMediator mediator, ILogger<FormsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Get forms
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetForms([FromQuery] GetFormsQuery query)
    {
        var forms = await _mediator.Send(query);
        return Ok(forms);
    }

    /// <summary>
    /// Get form by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetFormById([FromRoute] Guid id)
    {
        var query = new GetFormByIdQuery
        {
            Id = id,
        };
        var form = await _mediator.Send(query);

        return Ok(form);
    }

    /// <summary>
    /// Add form
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> AddForm([FromBody] AddFormCommand command)
    {
        var form = await _mediator.Send(command);

        return Accepted(form);
    }

    /// <summary>
    /// Update form
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<IActionResult> UpdateForm([FromBody] UpdateFormCommand command)
    {
        var form = await _mediator.Send(command);

        return Accepted(form);
    }

    /// <summary>
    /// Delete form
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteForm([FromRoute] Guid id)
    {
        var command = new DeleteFormCommand {Id = id};
        await _mediator.Send(command);

        return Accepted();
    }

    private readonly IMediator _mediator;
    private readonly ILogger _logger;
}