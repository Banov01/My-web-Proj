﻿using CarPartsShoppingList.Core.Contracts;
using CarPartsShoppingList.Core.ViewModels;
using CarPartsShoppingList.Infrastructure.Data.Common;
using CarPartsShoppingList.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CarPartsShoppingList.Core.Services
{
    public class ShoppingListService : BaseService, IShoppingListService
    {
        public ShoppingListService(IRepository _repo) : base(_repo)
        {
        }

        public async Task Add(ShoppingListItemViewModel model)
        {
            await this.repo.AddAsync(model);
            await this.repo.SaveChangesAsync();
        }

        public ShoppingListViewModel GetShoppingList(int id)
        {
            return repo.All<ShoppingList>()
                .Where(x => x.Id == id)
                .Select(x => new ShoppingListViewModel()
                {
                    Id = x.Id,
                    ShoppingListName = x.Name
                })
                .FirstOrDefault();
        }

        public ShoppingListItemViewModel GetShoppingListItemById(int id)
        {
            return repo.All<ShoppingListItem>()
                .Where(x => x.Id == id)
                .Select(x => new ShoppingListItemViewModel()
                {
                    Id = x.Id,
                    ShoppingListItemName = x.Name,
                    IsPurchased = x.IsChecked,
                    ShoppingListItemPrice = x.Price
                })
                .FirstOrDefault();
        }

        public IQueryable<ShoppingListItemViewModel> GetShoppingListItems(int shoppingListId)
        {
            return repo.AllReadonly<ShoppingListItem>()
                .Include(x => x.Transmission)
                .Include(x => x.Engine)
                .Include(x => x.Suspension)
                .Where(x => x.ShoppingListId == shoppingListId)
                .Select(x => new ShoppingListItemViewModel()
                {
                    Id = x.Id,
                    ShoppingListItemName = x.Name,
                    ShoppingListItemPrice = x.Price,
                    IsPurchased = x.IsChecked,
                });
        }

        public IQueryable<ShoppingListViewModel> GetShoppingLists()
        {
            var engines = repo.

            return repo.AllReadonly<ShoppingList>()
                .Select(x => new ShoppingListViewModel()
                {
                     Id = x.Id,
                     ShoppingListName = x.Name,
                     ShoppingListItems = new ShoppingListItem()
                     {
                        Engines= engines
                     }

                })
                .AsQueryable();
        }

        public async Task<bool> SaveData(ShoppingListViewModel model)
        {
            try
            {
                foreach (var item in model.ShoppingListItems)
                {
                    await repo.AddAsync(item);
                }
                repo.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                throw new ArgumentException("Cant save data");
            }
        }
    }
}
