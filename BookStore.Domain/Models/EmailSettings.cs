using System;
namespace BookStore.API.Models
{
    public class EmailSettings
    {
        public String Email { get; set; }
        public String Password { get; set; }
        public String DisplayName { get; set; }
        public String Host { get; set; }
        public int Port { get; set; }
    }
}