using System.Collections.Generic;
using WebAPI.Models.Models;

namespace WebAPI.Models.Result
{
    public class FullTeam : Team
    {
        public FullTeam()
        {
            Users = new List<User>();
        }

        public override int MembersCount => Users.Count;
        
        public IList<User> Users { get; set; }
    }
}