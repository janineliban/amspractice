using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AssetManagementSystem;
using AssetManagementSystem.Models;
using AssetManagementSystem.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using TinyIoC;

namespace Earviems.Web
{
    public static class Bootstrapper
    {
        public static void Register(TinyIoCContainer current)
        {
            //Database
            current.Register(new ApplicationDbContext()).AsMultiInstance();

            //Services
            current.Register<TransactionService>().AsMultiInstance();

            //Application User Manager
            var applicantUserManager = new ApplicationUserManager(new UserStore<ApplicationUser>(current.Resolve<ApplicationDbContext>()));
            current.Register(applicantUserManager);

            //Controller and API Resolver
            System.Web.Mvc.DependencyResolver.SetResolver(new TinyIocMvcDependencyResolver(current));
          ;
        }
    }
}