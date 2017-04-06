using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using System.Threading.Tasks;
using E_TRADING.Data.Managers;
using Microsoft.AspNet.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace E_TRADING.Data.Entities
{
    public class User : IdentityUser
    {
        [DisplayName("Ім'я користувача")]
        public override string UserName { get; set; }

        [DisplayName("Номер телефону")]
        public override string PhoneNumber { get; set; }

        [DisplayName("Ім'я")]
        public string FirstName { get; set; }

        [DisplayName("Прізвище")]
        public string LastName { get; set; }

        [DisplayName("Адреса")]
        public string Address { get; set; }
        
        //public Guid? BuyerId { get; set; }
        //[ForeignKey("BuyerId")]
        //public virtual Buyer Buyer { get; set; }

        //public Guid? SellerId { get; set; }
        //[ForeignKey("SellerId")]
        //public virtual Seller Seller { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUserManager manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
    }
}
