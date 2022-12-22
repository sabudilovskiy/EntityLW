using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LW3
{
    internal class HallController
    {
        AppContext app_context;
        public HallController(AppContext app_context)
        {
            this.app_context = app_context;
        }
        public bool Create(int number, int capacity)
        {
            bool answer = true;
            try
            {
               
                app_context.halls.Add(new Hall() { capacity = capacity, Number = number});
                app_context.SaveChanges();
            }
            catch (Exception exc)
            {
                answer = false;
            }
            return answer;
        }
        public List<Hall> GetAll()
        {
            return app_context.halls.AsQueryable().ToList();
        }
        public bool Update(int id, int number, int capacity)
        {
            var found = app_context.halls.Find(id);
            if (found == null) { return false; }
            found.Number = number;
            found.capacity= capacity;
            app_context.halls.AddOrUpdate(found);
            app_context.SaveChanges();
            return true;
        }
        public bool Delete(int id)
        {
            var found = app_context.halls.Find(id);
            if (found == null) return false;
            app_context.halls.Remove(found);
            app_context.SaveChanges();
            return true;
        }
    }
}
