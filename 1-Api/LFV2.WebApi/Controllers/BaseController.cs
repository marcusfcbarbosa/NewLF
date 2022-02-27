using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace LFV2.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BaseController : ControllerBase
    {
    }
}
