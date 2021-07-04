using System;
using System.Collections.Generic;

namespace JiraProject.Business.Security.JWT
{
    public class AccessToken
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public long UserId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public List<string> UserRoles { get; set; }
    }
}