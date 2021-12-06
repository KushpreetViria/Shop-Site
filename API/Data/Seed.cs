using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace API.Data
{
    /*
        Populates the database with data from UserSeed.json and ItemSeed.json files
    */
    
    public class Seed
    {
        public static async Task SeedUsers(DataContext context, ILogger logger){
            // if it's a populated database dont do anythings
            if(await context.Users.AnyAsync() || await context.Items.AnyAsync()) return;

            // ----------- add users -----------
            var userData = await System.IO.File.ReadAllTextAsync("Data/UserSeed.json");
            var users = JsonSerializer.Deserialize<List<AppUser>>(userData);
            foreach(var user in users)
            {
                using var hmac = new HMACSHA512();
                user.passHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("pa$$"));
                user.passSalt = hmac.Key;

                context.Users.Add(user);
            }
            await context.SaveChangesAsync();

            // ----------- add items to users -----------
            var itemData = await System.IO.File.ReadAllTextAsync("Data/ItemSeed.json");
            var items = JsonSerializer.Deserialize<List<Item>>(itemData);
            
            // selling 3 items
            var itemUser1 = await context.Users.FindAsync(1);
            addUserItems(itemUser1,items.Take(3).ToList());

            //selling 2 items
            var itemUser2 = await context.Users.FindAsync(2);
            addUserItems(itemUser2,items.Skip(3).Take(2).ToList());

            await context.SaveChangesAsync();

            // ----------- create a cart for 3 users -----------
            // createCart(context,await context.Users.FindAsync(1));
            // createCart(context,await context.Users.FindAsync(2));
            // createCart(context,await context.Users.FindAsync(3));

            await context.SaveChangesAsync();
        }

        private static void addUserItems(AppUser user,List<Item> items){
            foreach(var item in items)
            {
                if(user.Items == null) user.Items = new List<Item>();
                user.Items.Add(item);
            }
        }

        private static void createCart(DataContext context, AppUser user){
            if(user.Cart == null){
                user.Cart = new Cart(){
                    Count = 0,
                    DateCreated = System.DateTime.Now,
                    Items = new List<Item>()
                };
            }

            // add a random ammount of items
            var rand = new Random();
            var skip = (int)(rand.NextDouble() * context.Items.Count());
            var randItems = context.Items.OrderBy(o => o.Id).Skip(skip).Take(3);            
            foreach(var item in randItems){
                user.Cart.Count++;
                user.Cart.Items.Add(item);
            }
        }

    }
}