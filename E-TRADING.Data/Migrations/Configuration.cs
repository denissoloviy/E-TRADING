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
    using System.Linq;

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

            var category = new Category
            {
                Name = "Electronics",
                Id = "307b21c4-1d3a-4884-8c88-25082d785a8f"
            };
            var categories = new List<Category>
            {
                new Category
                {
                    Name = "Clothes",
                    Id = "E8C33859-C8FA-4BB3-A5C1-0E769FF3A475"
                },
                new Category
                {
                    MasterCategoryId = "307b21c4-1d3a-4884-8c88-25082d785a8f",
                    Name = "Computers",
                    Id = "C764CE78-314E-4B78-B984-04C9751F678D"
                },
                new Category
                {
                    Name = "Sport",
                    Id = "a515a13e-6d78-416e-826b-10bf01ae9b1b"
                },
                new Category
                {
                    Name = "House and garden",
                    Id = "d345D13a-6d78-416e-826b-10bf01ae9b1b"
                },
            };

            var categ = context.Categories.FirstOrDefault(item => item.Name == category.Name);
            if (categ == null)
            {
                context.Categories.Add(category);
            }

            foreach (var categor in categories)
            {
                var existingCategory = context.Categories.FirstOrDefault(item => item.Name == categor.Name);
                if (existingCategory == null)
                {
                    context.Categories.Add(categor);
                }
            }

            var products = new List<Product>
            {
                new Product
                {
                    CategoryId = "C764CE78-314E-4B78-B984-04C9751F678D",
                    Name = "Lenovo G510",
                    Description = "Very Good notepad",
                    Amount = 1,
                    Price = 5000M,
                    SellerId = userManager.FindByName("seller").Id
                },
                new Product
                {
                    CategoryId = "E8C33859-C8FA-4BB3-A5C1-0E769FF3A475",
                    Name = "Wool hat",
                    Description = "Very warm",
                    Amount = 2,
                    Price = 100M,
                    SellerId = userManager.FindByName("seller").Id
                },
                new Product
                {
                    CategoryId = "a515a13e-6d78-416e-826b-10bf01ae9b1b",
                    Name = "Weights",
                    Description = "Very heavy",
                    Amount = 2,
                    Price = 500M,
                    SellerId = userManager.FindByName("seller").Id
                },
                new Product
                {
                    CategoryId = "d345D13a-6d78-416e-826b-10bf01ae9b1b",
                    Name = "Shovel \"Tyapka\"",
                    Description = "Good",
                    Amount = 1,
                    Price = 300M,
                    SellerId = userManager.FindByName("seller").Id
                }
            };

            foreach (var product in products)
            {
                var existingProduct = context.Products.FirstOrDefault(item => item.Name == product.Name);
                if (existingProduct == null)
                {
                    context.Products.Add(product);
                }
            }

            base.Seed(context);
        }
    }
}
