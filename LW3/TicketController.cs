using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LW3
{
    internal class TicketController
    {
        AppContext app_context;
        public TicketController(AppContext app_context)
        {
            this.app_context = app_context;
        }
        public bool Create(float cost, int seat_number, int id_session, int id_box_office)
        {
            bool answer = true;
            try
            {
                app_context.tickets.Add(new Ticket() { 
                    cost= cost,
                    seat_number=seat_number,
                    SessionId=id_session,
                    BoxOfficeId=id_box_office
                });
                app_context.SaveChanges();
            }
            catch (Exception exc)
            {
                answer = false;
            }
            return answer;
        }
        public List<Ticket> GetAll()
        {
            return app_context.tickets.AsQueryable().ToList();
        }
        public bool Update(int id, float cost, int seat_number, int id_session, int id_box_office)
        {
            var found = app_context.tickets.Find(id);
            if (found == null) { return false; }
            try
            {
                found.cost= cost;
                found.seat_number=seat_number;
                found.SessionId= id_session;
                found.BoxOfficeId= id_box_office;
                app_context.tickets.AddOrUpdate(found);
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
            var found = app_context.tickets.Find(id);
            if (found == null) return false;
            app_context.tickets.Remove(found);
            app_context.SaveChanges();
            return true;
        }
    }
}
