using Microsoft.AspNetCore.Identity;


namespace APPR_PART_1_POE.Services
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(RoleManager<IdentityRole> roleManager)
        {
            string[] roles = { "Employee", "Donor" }; // Define the roles you want to create

            foreach (var role in roles) //for each goes through each role in the roles array, one at a time.
            {
                if (!await roleManager.RoleExistsAsync(role)) //RoleManager is used so that it can manage Identity roles.
                {
                    await roleManager.CreateAsync(new IdentityRole(role)); //If role doesn't exist, create it.
                }
            }
        }
    }
}
