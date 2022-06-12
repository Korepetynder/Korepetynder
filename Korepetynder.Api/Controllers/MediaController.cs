using Korepetynder.Api.Attributes;
using Korepetynder.Contracts.Requests.Media;
using Korepetynder.Contracts.Responses.Media;
using Korepetynder.Data.DbModels;
using Korepetynder.Services.Media;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Identity.Web.Resource;
using System.Globalization;

namespace Korepetynder.Api.Controllers
{
    [Authorize]
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAdB2C:Scopes")]
    [Route("[controller]")]
    [ApiController]
    public class MediaController : ControllerBase
    {
        private readonly IMediaService _mediaService;

        public MediaController(IMediaService mediaService)
        {
            _mediaService = mediaService;
        }

        /// <summary>
        /// Uploads a new multimedia file.
        /// </summary>
        /// <returns>Newly created multimedia file entry.</returns>
        [HttpPost]
        [DisableFormValueModelBinding]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<MultimediaFileResponse>> UploadMultimediaFile()
        {
            var fileResult = await _mediaService.ProcessFile(Request, ModelState);

            if (fileResult == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Bind form data to the model
            var multimediaFileRequest = new MultimediaFileRequest();
            var formValueProvider = new FormValueProvider(
                BindingSource.Form,
                new FormCollection(fileResult.FormAccumulator.GetResults()),
                CultureInfo.CurrentCulture);
            var bindingSuccessful = await TryUpdateModelAsync(multimediaFileRequest, prefix: "",
                valueProvider: formValueProvider);

            if (!bindingSuccessful)
            {
                ModelState.AddModelError("File",
                    "The request couldn't be processed (Error 5).");
                // Log error

                return BadRequest(ModelState);
            }

            var multimediaFile = new MultimediaFile(fileResult.BlobUrl)
            {
                Type = MultimediaFileType.Picture,
                SubjectId = multimediaFileRequest.SubjectId
            };

            var multimediaFileResponse = await _mediaService.AddMultimediaFile(multimediaFile);
            return Created(nameof(MediaController), multimediaFileResponse);
        }

        /// <summary>
        /// Gets the list of multimedia files uploaded by the user.
        /// </summary>
        /// <returns>List of multimedia files.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<MultimediaFileResponse>>> GetMultimediaFiles()
        {
            try
            {
                var multimediaFiles = await _mediaService.GetMultimediaFiles();
                return multimediaFiles.ToList();
            }
            catch (InvalidOperationException)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Deletes multimedia file with given ID.
        /// </summary>
        /// <param name="id">ID of the multimedia file to delete.</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteMultimediaFile([FromRoute] int id)
        {
            try
            {
                await _mediaService.DeleteMultimediaFile(id);
                return NoContent();
            }
            catch (InvalidOperationException)
            {
                return BadRequest();
            }
            catch (ArgumentException)
            {
                return Forbid();
            }
        }
    }
}
