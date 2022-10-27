using Microsoft.AspNetCore.Identity;
using Technic.Web.Data.Base;
using Technic.Web.Data.Entites;

namespace Technic.Web.Data
{
    public class DataSeed
    {
        private readonly IApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;
        private const string AdminRoleName = "Admin";

        public DataSeed(IApplicationDbContext context, UserManager<User> userManager
            ,RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public static void Initialize(IApplicationDbContext context, UserManager<User> userManager
            , RoleManager<IdentityRole> roleManager)
        {
            AddCategory(context);
            
            AddAdmin(roleManager, userManager);
        }
        private static void AddAdmin(RoleManager<IdentityRole> roleManager,UserManager<User> userManager)
        {
            if (!roleManager.RoleExistsAsync(AdminRoleName).Result)
            {
                IdentityRole role = new IdentityRole(AdminRoleName);
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;

               
            }
            if (userManager.FindByNameAsync("Admin").Result == null)
            {
                User manager = new User();
                manager.UserName = "Admin";
                manager.Email = "RandomGmail@Gmail.com";

                IdentityResult result = userManager.CreateAsync(manager, "123456789zZ*").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(manager, AdminRoleName).Wait();
                }
            }
        }
      
        private static void AddCategory(IApplicationDbContext context)
        {
            if (context.ProductCategories.Count() == 0)
            {
                context.ProductCategories.AddRangeAsync(
                   new ProductCategory
                   {
                       Name = "Периферия",
                       Id = Guid.NewGuid(),
                       Slug = "Smartphone",
                   },
                   new ProductCategory
                   {
                       Name = "Розетки",
                       Id = Guid.NewGuid(),
                       Slug = "Tablet",
                   },
                   new ProductCategory
                   {
                       Name = "Камеры",
                       Id = Guid.NewGuid(),
                       Slug = "Laptop",
                   },
                   new ProductCategory
                   {
                       Name = "Пульты",
                       Id = Guid.NewGuid(),
                       Slug = "Household-appliance",
                   },
                   new ProductCategory
                   {
                       Name = "Колонки",
                       Id = Guid.NewGuid(),
                       Slug = "Office",
                   },
                   new ProductCategory
                   {
                       Name = "Smart-часы",
                       Id = Guid.NewGuid(),
                       Slug = "Smart-watch",
                   },
                   new ProductCategory
                   {
                       Name = "Мониторы",
                       Id = Guid.NewGuid(),
                       Slug = "Tv",
                   },
                   new ProductCategory
                   {
                       Name = "Лампочки",
                       Id = Guid.NewGuid(),
                       Slug = "Pc",
                   }
               ); 
            }
            context.SaveChanges();
        }
    }
}
