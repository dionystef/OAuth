using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oAuth.Model
{
    public class MyDbContext : IdentityDbContext<MyUser>
    {
        public DbSet<Todo> Todoes { get; set; }
    }
}
