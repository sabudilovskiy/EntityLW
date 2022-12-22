using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LW3
{
    internal class BoxOfficeController
    {
        AppContext app_context;
        public BoxOfficeController(AppContext app_context)
        {
            this.app_context = app_context;
        }
        public bool Create(int id_worker, int number)
        {
            bool answer = true;
            try
            {
                app_context.boxOffices.Add(new BoxOffice() { WorkerId = id_worker, Number = number }) ;
                app_context.SaveChanges();
            }
            catch (Exception exc)
            {
                answer = false;
            }
            return answer;
        }
        public List<BoxOffice> GetAll()
        {
            return app_context.boxOffices.AsQueryable().ToList();
        }
        public bool Update(int id, int id_worker, int number)
        {
            var found = app_context.boxOffices.Find(id);
            if (found == null) { return false; }
            try
            {
                found.WorkerId= id_worker;
                found.Number= number;
                app_context.boxOffices.AddOrUpdate(found);
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
            var found = app_context.boxOffices.Find(id);
            if (found == null) return false;
            app_context.boxOffices.Remove(found);
            app_context.SaveChanges();
            return true;
        }
    }
}
