using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class Seed
    {
        public static async Task SeedUsers(DataContext context)
        {
            if (await context.Users.AnyAsync()) return;

            var userData = await System.IO.File.ReadAllTextAsync("Data/UserSeedData.json");
            var users = JsonSerializer.Deserialize<List<Employee>>(userData);
            foreach (var user in users)
            {
                using var hmac = new HMACSHA512();

                user.UserName = user.UserName.ToLower();
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd"));
                user.PasswordSalt = hmac.Key;
                // does not need to be async here as not doing anything to database yes we just adding the tracking
                context.Users.Add(user);
            }


            await context.SaveChangesAsync();
        }

        public static async Task SeedAddressess(DataContext context)
        {
            if (await context.Addresses.AnyAsync()) return;

            var addressData = await System.IO.File.ReadAllTextAsync("Data/AddressesSeedData.json");
            var addresses = JsonSerializer.Deserialize<List<Address>>(addressData);
            foreach (var address in addresses)
            {
                context.Addresses.Add(address);
            }
            await context.SaveChangesAsync();
        }

        public static async Task SeedCustomers(DataContext context)
        {
            if (await context.Customers.AnyAsync()) return;

            var customerData = await System.IO.File.ReadAllTextAsync("Data/CustomerSeedData.json");
            var customers = JsonSerializer.Deserialize<List<Customer>>(customerData);
            foreach (var customer in customers)
            {
                context.Customers.Add(customer);
            }
            await context.SaveChangesAsync();
        }

        public static async Task SeedMeasureTypes(DataContext context)
        {
            if (await context.MeasureTypes.AnyAsync()) return;

            var measureTypeData = await System.IO.File.ReadAllTextAsync("Data/MeasureTypeSeedData.json");
            var measureTypes = JsonSerializer.Deserialize<List<MeasureType>>(measureTypeData);
            foreach (var type in measureTypes)
            {
                context.MeasureTypes.Add(type);
            }
            await context.SaveChangesAsync();
        }

        public static async Task SeedMeasures(DataContext context)
        {
            if (await context.Measures.AnyAsync()) return;

            var measuresData = await System.IO.File.ReadAllTextAsync("Data/MeasuresSeedData.json");
            var measures = JsonSerializer.Deserialize<List<Measure>>(measuresData);
            foreach (var measure in measures)
            {
                context.Measures.Add(measure);
            }
            await context.SaveChangesAsync();
        }

    }
}