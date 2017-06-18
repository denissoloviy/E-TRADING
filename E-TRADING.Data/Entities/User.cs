using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using System.Threading.Tasks;
using E_TRADING.Data.Managers;
using Microsoft.AspNet.Identity;
using System.ComponentModel;
using System;

namespace E_TRADING.Data.Entities
{
    public class User : IdentityUser
    {
        [DisplayName("Name")]
        public override string UserName { get; set; }

        [DisplayName("Phone number")]
        public override string PhoneNumber { get; set; }
        
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Last name")]
        public string LastName { get; set; }

        [DisplayName("Addres")]
        public string Address { get; set; }

        [DisplayName("Patronymic")]
        public string MiddleName { get; set; }

        [DisplayName("Birth date")]
        public DateTime? BirthDate { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUserManager manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
    }
}
