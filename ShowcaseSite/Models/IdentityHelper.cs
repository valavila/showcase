using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShowcaseSite.Models;

namespace ShowcaseSite.Models
{
    public class IdentityHelper
    {
        /* public void GetBio()
        {
            string Bio = U
        }*/ 
        public static void SetIdentityOptions(IdentityOptions options)
        {
            // Setting sign in options                                                 
            options.SignIn.RequireConfirmedEmail = false;
            options.SignIn.RequireConfirmedPhoneNumber = false;

            // Set password strength
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 8;
            options.Password.RequireNonAlphanumeric = false;


        }

    }
}
