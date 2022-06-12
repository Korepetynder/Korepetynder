using Korepetynder.Api.Attributes;
using Korepetynder.Contracts.Requests.Media;
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
    [Route("api/[controller]")]
    [ApiController]
    public class MediaController : ControllerBase
    {
        private readonly IMediaService _mediaService;

        public MediaController(IMediaService mediaService)
        {
            _mediaService = mediaService;
        }

        [HttpPost]
        [DisableFormValueModelBinding]
        public async Task<IActionResult> UploadFile()
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
    }
}
