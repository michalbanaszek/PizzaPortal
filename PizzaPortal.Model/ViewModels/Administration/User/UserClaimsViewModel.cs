using System.Collections.Generic;

namespace PizzaPortal.Model.ViewModels.Administration.User
{
    public class UserClaimsViewModel : BaseViewModel
    {
        public UserClaimsViewModel()
        {
            Claims = new List<UserClaim>();
        }

        public string UserId { get; set; }
        public List<UserClaim> Claims { get; set; }
    }

    public class UserClaim
    {
        public string ClaimType { get; set; }
        public bool IsSelected { get; set; }
    }
}
