using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace PizzaPortal.Model.Models
{
    public class Order : BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }
   
        public string AddressLine1 { get; set; }     
        public string AddressLine2 { get; set; }
        public string ZipCode { get; set; }  
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public decimal OrderTotal { get; set; }
        public DateTime OrderPlaced { get; set; }
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
    }
}
