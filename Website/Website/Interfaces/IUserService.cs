using Website.Controllers;
using Website.Domain.Response;
using Website.Models;

namespace Website.Interfaces
{
    public interface IUserService
    {
        Domain.Response.BaseResponse<Dictionary<int, string>> GetRoles();

    }
}
