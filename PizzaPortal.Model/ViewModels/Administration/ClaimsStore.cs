using System.Collections.Generic;
using System.Security.Claims;

namespace PizzaPortal.Model.ViewModels.Administration
{
    public static class ClaimsStore
    {
        public static List<Claim> AllClaims = new List<Claim>()
        {
            new Claim("Create Role", "true"),
            new Claim("Edit Role","true"),
            new Claim("Delete Role","true")
        };
    }
}
