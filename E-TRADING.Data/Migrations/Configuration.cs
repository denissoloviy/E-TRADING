namespace E_TRADING.Data.Migrations
{
    using Common;
    using Entities;
    using Managers;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "E_TRADING.Data.ApplicationDbContext";
        }

        protected override void Seed(ApplicationDbContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<User>(context));
            var roleManager = new ApplicationRoleManager(new RoleStore<IdentityRole>(context));

            #region Users

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = UserRole.SuperAdmin
                },
                new IdentityRole
                {
                    Name = UserRole.Administrator
                },
                new IdentityRole
                {
                    Name = UserRole.Buyer
                },
                new IdentityRole
                {
                    Name = UserRole.Seller
                },
                new IdentityRole
                {
                    Name = UserRole.Banned
                }
            };

            foreach (var identityRole in roles)
            {
                var existingRole = roleManager.FindByName(identityRole.Name);
                if (existingRole == null)
                {
                    roleManager.Create(identityRole);
                }
            }

            var superAdmin = new User //only one, always
            {
                UserName = "superadmin",
                Email = "superadmin@admin.com",
                EmailConfirmed = true,
                Id = Guid.NewGuid().ToString()
            };
            var superAdminPassword = "q1w2e3r4";

            var adminUsers = new List<User>
            {
                new User
                {
                    UserName = "admin",
                    Email = "admin@admin.com",
                    EmailConfirmed = true,
                    Id = Guid.NewGuid().ToString()
                }
            };

            var buyerUsers = new List<User>
            {
                new User
                {
                    UserName = "buyer",
                    Email = "buyer@buyer.com",
                    EmailConfirmed = true,
                    Id = Guid.NewGuid().ToString()
                }
            };

            var sellerUsers = new List<User>
            {
                new User
                {
                    UserName = "seller",
                    Email = "seller@seller.com",
                    EmailConfirmed = true,
                    Id = Guid.NewGuid().ToString()
                }
            };

            var eUser = userManager.FindByName(superAdmin.UserName);
            if (eUser == null)
            {
                userManager.Create(superAdmin, superAdminPassword);
                userManager.SetLockoutEnabled(superAdmin.Id, false);

                var rolesForUser = userManager.GetRoles(superAdmin.Id);
                if (!rolesForUser.Contains(UserRole.SuperAdmin))
                {
                    userManager.AddToRole(superAdmin.Id, UserRole.SuperAdmin);
                }
            }

            foreach (var applicationUser in adminUsers)
            {
                var existingUser = userManager.FindByName(applicationUser.UserName);
                if (existingUser == null)
                {
                    userManager.Create(applicationUser, Common.Constants.Password);
                    userManager.SetLockoutEnabled(applicationUser.Id, false);

                    var rolesForUser = userManager.GetRoles(applicationUser.Id);
                    if (!rolesForUser.Contains(UserRole.Administrator))
                    {
                        userManager.AddToRole(applicationUser.Id, UserRole.Administrator);
                    }
                }
            }

            foreach (var applicationUser in buyerUsers)
            {
                var existingUser = userManager.FindByName(applicationUser.UserName);
                if (existingUser == null)
                {
                    userManager.Create(applicationUser, Common.Constants.Password);
                    userManager.SetLockoutEnabled(applicationUser.Id, false);

                    var rolesForUser = userManager.GetRoles(applicationUser.Id);
                    if (!rolesForUser.Contains(UserRole.Buyer))
                    {
                        userManager.AddToRole(applicationUser.Id, UserRole.Buyer);
                    }

                    context.Buyers.Add(new Buyer
                    {
                        Id = applicationUser.Id
                    });
                }
            }

            foreach (var applicationUser in sellerUsers)
            {
                var existingUser = userManager.FindByName(applicationUser.UserName);
                if (existingUser == null)
                {
                    userManager.Create(applicationUser, Common.Constants.Password);
                    userManager.SetLockoutEnabled(applicationUser.Id, false);

                    var rolesForUser = userManager.GetRoles(applicationUser.Id);
                    if (!rolesForUser.Contains(UserRole.Seller))
                    {
                        userManager.AddToRole(applicationUser.Id, UserRole.Seller);
                    }

                    context.Sellers.Add(new Seller
                    {
                        Id = applicationUser.Id,
                        Alias = applicationUser.UserName + "Alias",
                        ContactPhone = "0987654321",
                        OfficeAddress = "Kiev, Sechenova, 6"
                    });
                }
            }

            #endregion

            base.Seed(context);
        }
    }
}
