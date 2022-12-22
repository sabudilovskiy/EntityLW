using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LW3
{
    internal class JobController
    {
        AppContext app_context;
        public JobController(AppContext app_context)
        {
            this.app_context = app_context;
        }
        public bool Create(String name_)
        {
            bool answer = true;
            try
            {
                app_context.jobs.Add(new Job() { Name = name_ });
                app_context.SaveChanges();
            }
            catch (Exception exc)
            {
                answer = false;
            }
            return answer;
        }
        public List<Job> GetAll()
        {
            return app_context.jobs.AsQueryable().ToList();
        }
        public bool Update(int id, String name_)
        {
            var found = app_context.jobs.Find(id);
            if (found == null) { return false; }
            found.Name = name_;
            app_context.jobs.AddOrUpdate(found);
            app_context.SaveChanges();
            return true;
        }
        public bool Delete(int id)
        {
            var found = app_context.jobs.Find(id);
            if (found == null) return false;
            app_context.jobs.Remove(found);
            app_context.SaveChanges();
            return true;
        }
    }
}
