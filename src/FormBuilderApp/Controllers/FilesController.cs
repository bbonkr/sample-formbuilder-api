using System.Net;
using FormBuilder.Domains.Files.Commands.DeleteFile;
using FormBuilder.Domains.Files.Commands.Upload;
using FormBuilder.Domains.Files.Models;
using FormBuilder.Domains.Files.Queries.GetFileByUri;
using FormBuilderApp.Models;
using kr.bbon.AspNetCore;
using kr.bbon.AspNetCore.Models;
using kr.bbon.AspNetCore.Mvc;
using kr.bbon.Core;
using kr.bbon.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FormBuilderApp.Controllers;

[ApiController]
[Route(DefaultValues.RouteTemplate)]
[Area(DefaultValues.AreaName)]
[ApiVersion(DefaultValues.ApiVersion)]
[Produces("application/json")]
public class FilesController : ApiControllerBase
{
    public FilesController(IMediator mediator, ILogger<FilesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// download file
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpPost("download")]
    [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponseModel<ErrorModel>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponseModel<ErrorModel>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponseModel<ErrorModel>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Download([FromBody] GetFileByUriQuery query)
    {
        var file = await _mediator.Send(query);

        if (file.Content == null && file.Stream == null)
        {
            throw new ApiException(HttpStatusCode.NotFound);
        }

        var contentType = file.ContentType; // "application/octet-stream";

        return File(file.Stream, contentType, file.Name);
    }

    /// <summary>
    /// Uplaod file
    /// </summary>
    /// <param name="files"></param>
    /// <returns></returns>
    /// <exception cref="ApiException"></exception>
    [HttpPost]
    [ProducesResponseType(typeof(IEnumerable<UploadFileMediaModel>), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ApiResponseModel<ErrorModel>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponseModel<ErrorModel>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Upload([FromForm] IList<IFormFile> files)
    {
        // TODO Determine where store file from form file.
        var containerName = "forms";

        var result = new List<UploadFileMediaModel>();
        if (files == null)
        {
            throw new ApiException(HttpStatusCode.BadRequest);
        }

        await Parallel.ForEachAsync(files, async (file, canclleationToken) =>
        {
            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream, canclleationToken);
            await memoryStream.FlushAsync(canclleationToken);

            var command = new UploadCommand
            {
                Name = file.FileName,
                ContainerName = containerName,
                Size = file.Length,
                ContentType = file.ContentType,
                FileContent = memoryStream.ToArray(),
            };

            var model = await _mediator.Send(command, canclleationToken);

            result.Add(model);
        });

        return Accepted(result);
    }

    /// <summary>
    /// Delete file
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("delete")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ApiResponseModel<ErrorModel>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponseModel<ErrorModel>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete([FromBody] DeleteFileCommand command)
    {
        await _mediator.Send(command);

        return Accepted(true);
    }

    private readonly IMediator _mediator;
    private readonly ILogger _logger;
}