using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LW3
{
    internal class ConsoleController
    {
        protected Commands cur_command = Commands.Create;
        protected Types cur_type;
        protected JobController jobs;
        protected WorkerController workers;
        protected BoxOfficeController boxOffices;
        protected DirectorController directors;
        protected HallController halls;
        protected FilmController films;
        protected SessionController sessions;
        protected TicketController tickets;

        protected enum Types
        {
            Job = 0,
            Worker = 1,
            BoxOffice = 2,
            Director = 3,
            Hall = 4,
            Films = 5,
            Session = 6,
            Ticket = 7,
            Error = 8
        }
        protected enum Commands
        {
            Stop = 0,
            Create = 1,
            GetAll = 2,
            Update = 3,
            Delete = 4,
            Error = 5
        }
        protected void ConvertToCommand(String input)
        {
            if (!Enum.TryParse(input, true, out Commands my_command)) my_command = Commands.Error;
            cur_command = my_command;
        }
        protected void ConvertToType(String input)
        {
            if (!Enum.TryParse(input, true, out Types my_type)) my_type = Types.Error;
            cur_type = my_type;
        }
        protected static DateTime SafeInputDateTime(String prev)
        {
            Console.WriteLine(prev);
            DateTime result;
            try
            {
                result = DateTime.Parse(Console.ReadLine());
                return result;
            }
            catch (Exception e) { }
            while (true)
            {
                try
                {
                    Console.WriteLine("Допущена ошибка, повторите ввод:");
                    result = DateTime.Parse(Console.ReadLine());
                    return result;
                }
                catch (Exception e) { }
            }
        }
        protected static int SafeInputInt(String prev)
        {
            Console.WriteLine(prev);
            int result;
            try
            {
                result = int.Parse(Console.ReadLine());
                return result;
            }
            catch (Exception e) { }
            while (true)
            {
                try
                {
                    Console.WriteLine("Допущена ошибка, повторите ввод:");
                    result = int.Parse(Console.ReadLine());
                    return result;
                }
                catch (Exception e) { }
            }
        }
        protected static float SafeInputFloat(String prev)
        {
            Console.WriteLine(prev);
            float result;
            try
            {
                result = float.Parse(Console.ReadLine());
                return result;
            }
            catch (Exception e) { }
            while (true)
            {
                try
                {
                    Console.WriteLine("Допущена ошибка, повторите ввод:");
                    result = float.Parse(Console.ReadLine());
                    return result;
                }
                catch (Exception e) { }
            }
        }
        protected static String SafeInputString(String prev)
        {
            Console.WriteLine(prev);
            return Console.ReadLine();
        }
        protected static void MessageCreated(bool status)
        {
            if (status) Console.WriteLine("Успешно создано");
            else Console.WriteLine("Прозошла ошибка при создании");
        }
        protected static void MessageUpdated(bool status)
        {
            if (status) Console.WriteLine("Успешно изменено");
            else Console.WriteLine("Прозошла ошибка при изменении");
        }
        protected static void MessageDeleted(bool status)
        {
            if (status) Console.WriteLine("Успешно удалено");
            else Console.WriteLine("Элемент не найден");
        }
        protected void JobInput()
        {
            if (cur_command == Commands.Create)
            {
                String name = SafeInputString("Введите name: ");
                MessageCreated(jobs.Create(name));
            }
            else if (cur_command == Commands.Update)
            {
                int id = SafeInputInt("Введите id: ");
                String name = SafeInputString("Введите name: ");
                MessageUpdated(jobs.Update(id, name));
            }
            else if (cur_command == Commands.Delete)
            {
                int id = SafeInputInt("Введите id: ");
                MessageDeleted(jobs.Delete(id));
            }
            else
            {
                var listAll = jobs.GetAll();
                foreach (var item in listAll)
                {
                    String elem = "id: " + item.Id + " name: " + item.Name;
                    Console.WriteLine(elem);
                }
            }
        }
        protected void WorkerInput()
        {
            if (cur_command == Commands.Create)
            {
                String fio = SafeInputString("Введите ФИО: ");
                int id_job = SafeInputInt("Введите id_job: ");
                MessageCreated(workers.Create(fio, id_job));
            }
            else if (cur_command == Commands.Update)
            {
                int id = SafeInputInt("Введите id: ");
                String fio = SafeInputString("Введите ФИО: ");
                int id_job = SafeInputInt("Введите id_job: ");
                MessageCreated(workers.Update(id, fio, id_job));
            }
            else if (cur_command == Commands.Delete)
            {
                int id = SafeInputInt("Введите id: ");
                MessageDeleted(workers.Delete(id));
            }
            else if (cur_command == Commands.GetAll)
            {
                var all = workers.GetAll();
                all.ForEach(x => Console.WriteLine("id: " + x.Id + " FIO: " + x.FIO + " id_job: " + x.JobId
                ));
            }
        }
        protected void BoxOfficeInput()
        {
            if (cur_command == Commands.Create)
            {
                int number = SafeInputInt("Введите number: ");
                int id_worker = SafeInputInt("Введите id_worker: ");
                MessageCreated(boxOffices.Create(number, id_worker));
            }
            else if (cur_command == Commands.Update)
            {
                int id = SafeInputInt("Введите id: ");
                int number = SafeInputInt("Введите number: ");
                int id_worker = SafeInputInt("Введите id_worker: ");
                MessageUpdated(boxOffices.Update(id, number, id_worker));
            }
            else if (cur_command == Commands.Delete)
            {
                int id = SafeInputInt("Введите id: ");
                MessageDeleted(boxOffices.Delete(id));
            }
            else if (cur_command == Commands.GetAll)
            {
                var all = boxOffices.GetAll();
                all.ForEach(x => Console.WriteLine(
                    "id: " + x.Id + " number: " + x.Number + " id_worker: " + x.WorkerId
                ));
            }
        }
        protected void DirectorInput()
        {
            if (cur_command == Commands.Create)
            {
                var FIO = SafeInputString("Введите FIO: ");
                MessageCreated(directors.Create(FIO));
            }
            else if (cur_command == Commands.Update)
            {
                var id = SafeInputInt("Введите id: ");
                var FIO = SafeInputString("Введите FIO: ");
                MessageUpdated(directors.Update(id, FIO));
            }
            else if (cur_command == Commands.Delete)
            {
                var id = SafeInputInt("Введите id: ");
                MessageDeleted(directors.Delete(id));
            }
            else if (cur_command == Commands.GetAll)
            {
                var all = directors.GetAll();
                all.ForEach(x => Console.WriteLine(
                    "id: " + x.Id + " FIO: " + x.FIO
                ));
            }
        }
        protected void HallInput()
        {
            if (cur_command == Commands.Create)
            {
                var number = SafeInputInt("Введите number: ");
                var capacity = SafeInputInt("Введите capacity: ");
                MessageCreated(halls.Create(number, capacity));
            }
            else if (cur_command == Commands.Update)
            {
                var id = SafeInputInt("Введите id: ");
                var number = SafeInputInt("Введите number: ");
                var capacity = SafeInputInt("Введите capacity: ");
                MessageUpdated(halls.Update(id, number, capacity)); 
            }
            else if (cur_command== Commands.Delete) {
                var id = SafeInputInt("Введите id: ");
                MessageDeleted(halls.Delete(id));
            }
            else if (cur_command == Commands.GetAll)
            {
                var all = halls.GetAll();
                all.ForEach(x => Console.WriteLine(
                    "id: " + x.Id + " number: " + x.Number + " capacity: " + x.capacity
                    ));
            }
        }
        protected void FilmInput()
        {
            if (cur_command == Commands.Create)
            {
                var year = SafeInputInt("Введите year: ");
                var name_RU = SafeInputString("Введите name_RU: ");
                var name_EN = SafeInputString("Введите name_EN: ");
                var slogan = SafeInputString("Введите slogan: ");
                var id_director = SafeInputInt("Введите id_director: ");
                MessageCreated(films.Create(year, name_RU, name_EN, slogan, id_director));
            }
            else if (cur_command == Commands.Update) {
                var id = SafeInputInt("Введите id: ");
                var year = SafeInputInt("Введите year: ");
                var name_RU = SafeInputString("Введите name_RU: ");
                var name_EN = SafeInputString("Введите name_EN: ");
                var slogan = SafeInputString("Введите slogan: ");
                var id_director = SafeInputInt("Введите id_director: ");
                MessageUpdated(films.Update(id, year, name_RU, name_EN, slogan, id_director));
            }
            else if (cur_command == Commands.Delete)
            {
                var id = SafeInputInt("Введите id: ");
                MessageDeleted(films.Delete(id));
            }
            else if (cur_command == Commands.GetAll)
            {
                var all = films.GetAll();
                all.ForEach(x => Console.WriteLine(
                    "id: " + x.Id + " name_RU: " + x.name_RU + " name_EN: " + x.name_EN + " slogan: " + x.slogan + " id_director: " + x.DirectorId
                )
                );
            }
        }
        protected void SessionInput()
        {
            if (cur_command == Commands.Create)
            {
                var begin = SafeInputDateTime("Введите begin: ");
                var end = SafeInputDateTime("Введите end: ");
                var id_hall = SafeInputInt("Введите id_hall: ");
                var id_films = SafeInputInt("Введите id_films: ");
                MessageCreated(sessions.Create(begin, end, id_hall, id_films));
            }
            else if (cur_command == Commands.Update)
            {
                var id = SafeInputInt("Введите id: ");
                var begin = SafeInputDateTime("Введите begin: ");
                var end = SafeInputDateTime("Введите end: ");
                var id_hall = SafeInputInt("Введите id_hall: ");
                var id_films = SafeInputInt("Введите id_films: ");
                MessageUpdated(sessions.Update(id, begin, end, id_hall, id_films));
            }
            else if (cur_command == Commands.Delete)
            {
                var id = SafeInputInt("Введите id: ");
                MessageDeleted(sessions.Delete(id));
            }
            else if (cur_command == Commands.GetAll)
            {
                var alls = sessions.GetAll();
                alls.ForEach(x =>
                    Console.WriteLine("id: " + x.Id + " begin: " + x.begin + " end: " + x.end + " id_hall: " + x.HallId + " id_films: " + x.FilmId
                    ));
            }
        }
        protected void TicketInput()
        {
            if (cur_command == Commands.Create)
            {
                var cost = SafeInputFloat("Введите cost: ");
                var seat_number = SafeInputInt("Введите seat number: ");
                var id_session = SafeInputInt("Введите id_session: ");
                var id_box_office = SafeInputInt("Введите id_box_office: ");
                MessageCreated(tickets.Create(cost, seat_number, id_session, id_box_office));
            }
            else if (cur_command == Commands.Update)
            {
                var id = SafeInputInt("Введите id: ");
                var cost = SafeInputFloat("Введите cost: ");
                var seat_number = SafeInputInt("Введите seat number: ");
                var id_session = SafeInputInt("Введите id_session: ");
                var id_box_office = SafeInputInt("Введите id_box_office: ");
                MessageUpdated(tickets.Update(id, cost, seat_number, id_session, id_box_office));
            }
            else if (cur_command == Commands.Delete)
            {
                var id = SafeInputInt("Введите id: ");
                MessageDeleted(tickets.Delete(id));
            }
            else if (cur_command == Commands.GetAll)
            {
                var all = tickets.GetAll();
                all.ForEach(x =>
                Console.WriteLine(
                    "id: " + x.Id + " cost: " + x.cost + " seat number: " + x.seat_number + " id_session: " + x.SessionId + " id_box_office: " + x.BoxOfficeId
                ));
            }
        }
        protected void Dispatch()
        {
            if (cur_type == Types.Job) JobInput();
            else if (cur_type == Types.Worker) WorkerInput();
            else if (cur_type == Types.BoxOffice) BoxOfficeInput();
            else if (cur_type == Types.Ticket) TicketInput();
            else if (cur_type == Types.Director) DirectorInput();
            else if (cur_type == Types.Hall) HallInput();
            else if (cur_type == Types.Films) FilmInput();
            else if (cur_type == Types.Session) SessionInput();
            else if (cur_type == Types.Ticket) TicketInput();
        }
        public void Start()
        {
            String input;
            while (cur_command != Commands.Stop)
            {
                Console.WriteLine("Введите команду: Stop, Create, Update, GetAll, Delete: ");
                input = Console.ReadLine();
                ConvertToCommand(input);
                while (cur_command == Commands.Error)
                {
                    Console.WriteLine("Ошибка ввода, повторите ввод: ");
                    input = Console.ReadLine();
                    ConvertToCommand(input);
                }
                if (cur_command == Commands.Stop)
                {
                    continue;
                }
                Console.WriteLine("Введите таблицу: Job, Worker, Box_office, Hall, Director, Film, Session, Ticket: ");
                input = Console.ReadLine();
                ConvertToType(input);
                while (cur_type == Types.Error)
                {
                    Console.WriteLine("Ошибка ввода, повторите ввод: ");
                    input = Console.ReadLine();
                    ConvertToCommand(input);
                }
                Dispatch();
            }
        }
        public ConsoleController(AppContext appContext)
        {
            jobs = new JobController(appContext);
            workers= new WorkerController(appContext);
            boxOffices = new BoxOfficeController(appContext);
            directors= new DirectorController(appContext);
            halls = new HallController(appContext);
            films = new FilmController(appContext);
            sessions = new SessionController(appContext);
            tickets = new TicketController(appContext);
        }
    }
}
