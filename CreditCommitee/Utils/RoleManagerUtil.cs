using Commitee.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreditCommitee.Utils
{
    public class RoleManagerUtil
    {
        public RoleManager<ApplicationRole> RoleManager { get; private set; }
    }
}