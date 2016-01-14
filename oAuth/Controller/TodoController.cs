using oAuth.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace oAuth.Controller
{
    //Authorize indique que la classe est régie par oAuth
    [Authorize]
    public class TodoController : ApiController
    {

        //si on souchaite qu'une méthode sois accessible il faut mettre [AllowAnonymous]

        private MyDbContext db;       
        public TodoController()
        {
            db = new MyDbContext();
        }

        [HttpGet]
        public IQueryable<Todo> GetAllTodo()
        {
            return db.Todoes;
        }

        [HttpGet]
        public Todo GetTodoById(int? id)
        {
            Todo obj = db.Set<Todo>().Find(id);

            if(obj == null)
            {
                return null;
            }

            return obj;
        }

        [HttpPost]
        public Todo AddTodo(Todo todo)
        {
            Todo obj = db.Set<Todo>().Add(todo);
            db.SaveChanges();

            return todo;
        }

        [HttpDelete]
        public Todo Delete(int? id)
        {
            Todo obj = db.Set<Todo>().Find(id);

            if (obj == null)
            {
                return null;
            }

            db.Set<Todo>().Remove(obj);
            db.SaveChanges();

            return obj;
        }

        [HttpDelete]
        public Todo Update(Todo obj)
        {
            var t = db.Entry<Todo>(obj);
            t.State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return obj;
        }


    }
}
