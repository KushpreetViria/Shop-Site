using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace API.Data
{
    public class Seed
    {
        public static async Task SeedUsers(DataContext context, ILogger logger){
            if(await context.Users.AnyAsync()) return;

            var userData = await System.IO.File.ReadAllTextAsync("Data/UserSeed.json");
            var users = JsonSerializer.Deserialize<List<AppUser>>(userData);
            foreach(var user in users)
            {
                using var hmac = new HMACSHA512();
                user.passHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("pa$$"));
                user.passSalt = hmac.Key;

                context.Users.Add(user);                
            }

            // var itemData = await System.IO.File.ReadAllTextAsync("Data/ItemSeed.json");
            // var items = JsonSerializer.Deserialize<List<Item>>(itemData);
            // foreach(var item in items)
            // {
            //     context.Items.Add(item);
            // }

            await context.SaveChangesAsync();


            // //NO CLUE HOW TO ADD DATA TO AN ... FIGURE IT OUT LATER
            // Cart cart1 = new Cart(){
            //     AppUser = await context.Users.FindAsync(1),
            //     Count = 0,
            //     DateCreated = System.DateTime.Now,
            // };
            // cart1.Items= new List<Item>();
            // cart1.Items.Add(await context.Items.FindAsync(1));
            // cart1.Items.Add(await context.Items.FindAsync(2));

            // var entity = await context.Users.FindAsync(1);
            
            // System.Console.Write("--------------------------------------------------------------------------------------");
            
            // entity.UserName = "RANDOMNAME";
            // entity.Cart = cart1;
            // await context.SaveChangesAsync();
        }
    }
}