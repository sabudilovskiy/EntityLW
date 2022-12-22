using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LW3
{
    internal class DirectorController
    {
        AppContext app_context;
        public DirectorController(AppContext app_context)
        {
            this.app_context = app_context;
        }
        public bool Create(String fio_)
        {
            bool answer = true;
            try
            {
                app_context.directors.Add(new Director() { FIO = fio_ });
                app_context.SaveChanges();
            }
            catch (Exception exc)
            {
                answer = false;
            }
            return answer;
        }
        public List<Director> GetAll()
        {
            return app_context.directors.AsQueryable().ToList();
        }
        public bool Update(int id, String fio_)
        {
            var found = app_context.directors.Find(id);
            if (found == null) { return false; }
            found.FIO = fio_;
            app_context.directors.AddOrUpdate(found);
            app_context.SaveChanges();
            return true;
        }
        public bool Delete(int id)
        {
            var found = app_context.directors.Find(id);
            if (found == null) return false;
            app_context.directors.Remove(found);
            app_context.SaveChanges();
            return true;
        }
    }
}
