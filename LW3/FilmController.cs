using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LW3
{
    internal class FilmController
    {
        AppContext app_context;
        public FilmController(AppContext app_context)
        {
            this.app_context = app_context;
        }
        public bool Create(int year, String name_RU, String name_EN, String slogan, int id_director)
        {
            bool answer = true;
            try
            {
                app_context.films.Add(new Film() {
                    Year = year,
                    name_RU= name_RU,
                    name_EN= name_EN,
                    slogan= slogan,
                    DirectorId= id_director
                }) ;
                app_context.SaveChanges();
            }
            catch (Exception exc)
            {
                answer = false;
            }
            return answer;
        }
        public List<Film> GetAll()
        {
            return app_context.films.AsQueryable().ToList();
        }
        public bool Update(int id, int year, String name_RU, String name_EN, String slogan, int id_director)
        {
            var found = app_context.films.Find(id);
            if (found == null) { return false; }
            try
            {
                found.Year = year;
                found.name_EN= name_EN;
                found.name_RU= name_RU;
                found.slogan= slogan;
                found.DirectorId= id_director;
                app_context.films.AddOrUpdate(found);
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
            var found = app_context.films.Find(id);
            if (found == null) return false;
            app_context.films.Remove(found);
            app_context.SaveChanges();
            return true;
        }
    }
}
