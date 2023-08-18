using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryServiceApi.Controllers.ApiControllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,MainAdmin,Developer")]
    public class AdminController : Controller
    {

    }
}

