using System;
using System.Collections.Generic;

namespace scaffoldbcontext.Models
{
    public partial class Users
    {
        public long UserId { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Passwordhash { get; set; }
        public string LastChange { get; set; }
    }
}
