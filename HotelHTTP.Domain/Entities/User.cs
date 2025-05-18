using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Entities
{
    public class User 
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string PasswordHash {  get; private set; }
        public string Email { get; private set; }

        private User(Guid id, string name, string passwordHash,string email) 
        {
            Id = id;
            Name = name;
            PasswordHash = passwordHash;
            Email = email;
        }

        public static User Create(Guid id, string name, string passwordhash, string email)
        {
            return new User(id, name, passwordhash, email);
        }
    }
}
