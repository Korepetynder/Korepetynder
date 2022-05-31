using Microsoft.AspNetCore.Http;

namespace Korepetynder.Services
{
    internal abstract class AdBaseService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AdBaseService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected Guid GetCurrentUserId()
        {
            if (_httpContextAccessor == null)
            {
                throw new InvalidOperationException("There is no active HttpContext");
            }

            return new Guid(_httpContextAccessor.HttpContext
                .User
                .FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier")?
                .Value!);
        }
    }
}
