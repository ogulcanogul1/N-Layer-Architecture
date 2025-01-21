using App.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomBaseController : ControllerBase
    {
        [NonAction]
        public IActionResult CreateActionResult<T>(ServiceResult<T> result)
        {
            if (result.StatusCode == HttpStatusCode.NoContent)
            {
                return new ObjectResult(null) { StatusCode = result.StatusCode.GetHashCode() };
                //return NoContent();
                // result.StatusCode int'a cast de edilebilir.
            }
            if (result.StatusCode == HttpStatusCode.Created)
            {
                return Created(result.UrlAsCreated, result);
            }

            return new ObjectResult(result) { StatusCode = (int)result.StatusCode };

        }
        [NonAction]
        public IActionResult CreateActionResult(ServiceResult result)
        {
            if (result.StatusCode == HttpStatusCode.NoContent)
            {
                return new ObjectResult(null) { StatusCode = result.StatusCode.GetHashCode() };
                // result.StatusCode int'a cast de edilebilir.
            }

            return new ObjectResult(result) { StatusCode = (int)result.StatusCode };

        }

    }
}


//{
//    "data": {
//        "id": 2,
//    "name": "kalem1",
//    "price": 20,
//    "stock": 500
//    },
//  "errorMessage": null
//}