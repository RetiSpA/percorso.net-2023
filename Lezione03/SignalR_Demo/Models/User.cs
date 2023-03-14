using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SignalR_Demo.Models
{
    public class User
    {
        //public string Name { get; set; }

        //public string Surname { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class UserIDProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            return connection.User?.FindFirst(ClaimTypes.Name)?.Value;
        }
    }
}
