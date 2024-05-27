using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace ZenDev.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class GroupController : Controller
    {

    }
}
