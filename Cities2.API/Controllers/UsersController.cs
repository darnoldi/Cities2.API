using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cities2.API.Controllers
{
    [Route("api/users")]

    public class UsersController : Controller
    {
        [HttpGet()]

        public JsonResult GetUsers()
        {
            return new JsonResult(new List<object>()
                    {
                       new { id=1, FirstName="Dave", UserName="davea"},
                       new { id=2, Surname="Alwyn", UserName="alwynp"}
                    });
        }


    }
}




