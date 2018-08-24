using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeHelp.API
{
    [Authorize]
    public class BaseController:Controller
    {
        
    }
}