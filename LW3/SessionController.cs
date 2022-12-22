using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LW3
{
    internal class SessionController
    {
        AppContext app_context;
        public SessionController(AppContext app_context)
        {
            this.app_context = app_context;
        }
        public bool Create(DateTime begin, DateTime end, int id_hall, int id_films)
        {
            bool answer = true;
            try
            {
                app_context.sessions.Add(new Session() { 
                    begin= begin,
                    end= end,
                    HallId= id_hall,
                    FilmId= id_films
                });
                app_context.SaveChanges();
            }
            catch (Exception exc)
            {
                answer = false;
            }
            return answer;
        }
        public List<Session> GetAll()
        {
            return app_context.sessions.AsQueryable().ToList();
        }
        public bool Update(int id, DateTime begin, DateTime end, int id_hall, int id_films)
        {
            var found = app_context.sessions.Find(id);
            if (found == null) { return false; }
            try
            {
                found.begin= begin;
                found.end= end;
                found.HallId= id_hall;
                found.FilmId= id_films;
                app_context.sessions.AddOrUpdate(found);
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
            var found = app_context.sessions.Find(id);
            if (found == null) return false;
            app_context.sessions.Remove(found);
            app_context.SaveChanges();
            return true;
        }
    }
}
