using Microsoft.AspNetCore.Http;

namespace Korepetynder.Services
{
    internal abstract class AdBaseService
    {
        private IHttpContextAccessor _httpContextAccessor;

        public AdBaseService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected Guid GetCurrentUserId()
        {
            return new Guid(_httpContextAccessor.HttpContext
                .User
                .FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier")?
                .Value!);
        }
    }
}
