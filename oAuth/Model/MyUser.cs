using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oAuth.Model
{
    [JsonObject(MemberSerialization.OptIn)]
    public class MyUser : IdentityUser
    {
        //Identity User est une classe abstaite contenant un template pour un user
        [JsonProperty]
        public override string Id { get; set; }

        [JsonProperty]
        public override string UserName { get; set; }

        public virtual List<Todo> Todoes { get; set; }
    }
}
