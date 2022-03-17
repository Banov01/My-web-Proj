﻿using CarPartsShoppingList.Infrastructure.Data.Identity;
using CarPartsShoppingList.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarPartsShoppingList.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        DbSet<Engine> Engines { get; set; }
        DbSet<ShoppingList> ShoppingLists { get; set; }
        DbSet<ShoppingListItem> ShoppingListItmes { get; set; }
        DbSet<Suspension> Suspensions { get; set; }
        DbSet<Transmision> Transmissions { get; set; }
    }
}