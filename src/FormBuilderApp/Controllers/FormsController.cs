using FormBuilder.Domains.Forms.Commands.AddForm;
using FormBuilder.Domains.Forms.Commands.DeleteForm;
using FormBuilder.Domains.Forms.Commands.Migrations;
using FormBuilder.Domains.Forms.Commands.UpdateForm;
using FormBuilder.Domains.Forms.Models;
using FormBuilder.Domains.Forms.Queries.GetFormById;
using FormBuilder.Domains.Forms.Queries.GetForms;
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
    [ProducesResponseType(typeof(PagedModel<FormModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponseModel<ErrorModel>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponseModel<ErrorModel>), StatusCodes.Status500InternalServerError)]
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
    [ProducesResponseType(typeof(FormModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponseModel<ErrorModel>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponseModel<ErrorModel>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponseModel<ErrorModel>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetFormById([FromRoute] Guid id)
    {
        var form = await _mediator.Send(new GetFormByIdQuery(id));

        return Ok(form);
    }

    /// <summary>
    /// Add form
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(FormModel), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ApiResponseModel<ErrorModel>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponseModel<ErrorModel>), StatusCodes.Status500InternalServerError)]
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
    [ProducesResponseType(typeof(FormModel), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ApiResponseModel<ErrorModel>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponseModel<ErrorModel>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponseModel<ErrorModel>), StatusCodes.Status500InternalServerError)]
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
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ApiResponseModel<ErrorModel>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponseModel<ErrorModel>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponseModel<ErrorModel>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteForm([FromRoute] Guid id)
    {
        var command = new DeleteFormCommand { Id = id };
        await _mediator.Send(command);

        return Accepted();
    }

    /// <summary>
    /// (Temporary) Migrate data
    /// </summary>
    /// <returns></returns>
    //[Obsolete]
    //[HttpPost("migrate")]
    //[ProducesResponseType(StatusCodes.Status202Accepted)]
    //[ProducesResponseType(typeof(ApiResponseModel<ErrorModel>), StatusCodes.Status500InternalServerError)]
    //public async Task<IActionResult> Migrate()
    //{
    //    var command = new MigrationCommand();
    //    await _mediator.Send(command);

    //    return Accepted();
    //}

    private readonly IMediator _mediator;
    private readonly ILogger _logger;
}