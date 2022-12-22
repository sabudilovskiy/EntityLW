using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LW3
{
    internal class WorkerController
    {
        AppContext app_context;
        public WorkerController(AppContext app_context)
        {
            this.app_context = app_context;
        }
        public bool Create(String fio_, int id_job)
        {
            bool answer = true;
            try
            {
                app_context.workers.Add(new Worker() { FIO = fio_, JobId = id_job });
                app_context.SaveChanges();
            }
            catch (Exception exc)
            {
                answer = false;
            }
            return answer;
        }
        public List<Worker> GetAll()
        {
            return app_context.workers.AsQueryable().ToList();
        }
        public bool Update(int id, String fio_, int id_job)
        {
            var found = app_context.workers.Find(id);
            if (found == null) { return false; }
            try
            {
                found.FIO = fio_;
                found.JobId = id_job;
                app_context.workers.AddOrUpdate(found);
                app_context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool Delete(int id)
        {
            var found = app_context.workers.Find(id);
            if (found == null) return false;
            app_context.workers.Remove(found);
            app_context.SaveChanges();
            return true;
        }
    }
}
