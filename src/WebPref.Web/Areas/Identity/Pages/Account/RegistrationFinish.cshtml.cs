using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebPref.Web.Areas.Identity.Pages.Account
{
    public class RegistrationFinishModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        public RegistrationFinishModel(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public void OnGet(string email)
        {
            ViewData["email"] = email;
        }
        
    }
}
