using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SmartRecipesMVC.Web.Helpers
{
    public class AuthenticateUser : Controller
    {
        public string GetUserId()
        {
            if (User.Identity is ClaimsIdentity claimsIdentity)
            {
                var userIdClaim = claimsIdentity.Claims
                    .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
                if (userIdClaim != null)
                {
                    var userId = userIdClaim.Value;
                    return userId;
                }
            }
            return null;
        }
    }
}
