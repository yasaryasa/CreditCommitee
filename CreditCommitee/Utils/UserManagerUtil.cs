using Commitee.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreditCommitee.Utils
{
    public class UserManagerUtil
    {
        public UserManager<ApplicationUser> UserManager { get; private set; }
    }
}