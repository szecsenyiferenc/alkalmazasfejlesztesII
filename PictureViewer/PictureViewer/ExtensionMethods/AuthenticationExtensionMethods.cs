using Core.Models;
using Core.Models.Input;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PictureViewer.ExtensionMethods
{
    public static class AuthenticationExtensionMethods
    {
        public static void SignIn(this HttpContext httpContext, Customer customer)
        {
            httpContext.Session.SetInt32("userId", customer.Id);
            var customerJson = JsonConvert.SerializeObject(customer);
            httpContext.Session.SetString("customer", customerJson);
        }

        public static void SignOut(this HttpContext httpContext)
        {
            httpContext.Session.Remove("userId");
            httpContext.Session.Remove("customer");
        }

        public static Customer GetCurrentUser(this HttpContext httpContext)
        {
            try
            {
                var customerJson = httpContext.Session.GetString("customer");
                var customer = JsonConvert.DeserializeObject<Customer>(customerJson);
                return customer;
            }
            catch
            {
                return null;
            }

        }

        public static int? GetCurrentUserId(this HttpContext httpContext)
        {
             return httpContext.Session.GetInt32("userId");
        }

        public static bool IsSignedIn(this HttpContext httpContext)
        {
            if (httpContext.Session.GetInt32("userId") == null)
            {
                return false;
            }
            return true;
        }
    }
}
