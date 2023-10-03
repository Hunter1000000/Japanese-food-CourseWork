using System.Security.Claims;

namespace Website.Controllers
{
    internal class BaseResponse<T>
    {
        public ClaimsIdentity Data { get; set; }
        public string Description { get; set; }
        public int StatusCode { get; set; }
    }
}