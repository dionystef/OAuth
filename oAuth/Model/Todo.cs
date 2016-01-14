using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oAuth.Model
{
    public class Todo
    {
        //Les todos 

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }

        //One to Many with User
        public virtual MyUser User { get; set; }

        public virtual int UserId { get; set; }

    }
}
