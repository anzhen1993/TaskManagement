using System.Web.Http;

namespace TaskManagement.Controllers
{
    public class AuthenController : ApiController
    {
        [Authorize]
        public IHttpActionResult Authorize()
        {
            return Ok("Authorized");
        }
    }
}
