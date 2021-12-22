﻿using AppointmentScheduling.Models;
using AppointmentScheduling.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace AppointmentScheduling.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(ApplicationDbContext db, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _roleManager = roleManager;
            _userManager = userManager; 
        }

        public void Initalize()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception e)
            {

            }

            if (_db.Roles.Any(x => x.Name == Utility.Helper.Admin)) return;

            _roleManager.CreateAsync(new IdentityRole(Helper.Admin)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(Helper.Doctor)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(Helper.Patient)).GetAwaiter().GetResult();


            _userManager.CreateAsync(new ApplicationUser
            {
                UserName = "admin@email.com",
                Email= "admin@email.com",
                EmailConfirmed = true,
                Name = "Admin Root"
            }, "Admin123*").GetAwaiter().GetResult();

            ApplicationUser user = _db.Users.FirstOrDefault(u => u.Email == "admin@email.com");
            _userManager.AddToRoleAsync(user, Helper.Admin).GetAwaiter().GetResult();
        }
    }
}