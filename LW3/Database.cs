using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace LW3
{
    class Job
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public ICollection<Worker> workers { get; set; }
    }

    class Worker
    {
        public int Id { get; set; }
        public String FIO { get; set; }
        public int JobId { get; set; }
        public Job Job { get; set; }
        public ICollection<BoxOffice> boxOffices { get; set; }
    }
    class BoxOffice
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int WorkerId { get; set; }
        public ICollection<Ticket> tickets { get; set; }
        public Worker Worker { get; set; }
    }
    class Director
    {
        public int Id { get; set; }
        public String FIO { set; get; }
        public ICollection<Film> films { get; set; }
    }

    class Film
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public String name_RU { set; get; }
        public String name_EN { set; get; }
        public String slogan { set; get; }
        public int DirectorId { get; set; }
        public Director Director { get; set; }
        public ICollection<Session> sessions { get; set; }
    }
    class Hall
    {
        public int Id { get; set; }
        public int Number { set; get; }
        public int capacity { set; get; }
        public ICollection<Session> sessions { get; set; }
    }
    class Session
    {
        public int Id { get; set; }
        public DateTime begin { set; get; }
        public DateTime end { set; get; }
        public int HallId { get; set; }
        public int FilmId { get; set; }
        public Hall Hall { get; set; }
        public Film film { get; set; }
        public ICollection<Ticket> tickets { get; set; }
    }

    class Ticket
    {
        public int Id { get; set; }
        public float cost { set; get; }
        public int seat_number { set; get; }
        public int SessionId { get; set; }
        public int BoxOfficeId { get; set; }
        public Session session { get; set; }
        public BoxOffice boxOffice { get; set; }
    }
    class AppContext : DbContext
    {
        public DbSet<Job> jobs { get; set; }
        public DbSet<Worker> workers { get; set; }
        public DbSet<BoxOffice> boxOffices { get; set; }
        public DbSet<Director> directors { get; set; }
        public DbSet<Film> films { get; set; }
        public DbSet<Hall> halls { get; set; }
        public DbSet<Session> sessions { get; set; }
        public DbSet<Ticket> tickets { get; set; }
        public AppContext() : base("MyDatabase")
        {
            Database.SetInitializer(new AppInitializer());
            SaveChanges();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Job>()
                .HasMany(x => x.workers)
                .WithRequired(x => x.Job)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<Worker>()
                .HasMany(x => x.boxOffices)
                .WithRequired(x => x.Worker)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<BoxOffice>()
                .HasMany(x => x.tickets)
                .WithRequired(x => x.boxOffice)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<Hall>()
                .HasMany(x => x.sessions)
                .WithRequired(x => x.Hall)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<Director>()
                .HasMany(x => x.films)
                .WithRequired(x => x.Director)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<Film>()
                .HasMany(x => x.sessions)
                .WithRequired(x => x.film)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<Session>()
                .HasMany(x => x.tickets)
                .WithRequired(x => x.session)
                .WillCascadeOnDelete(false);
        }
    }
    internal class AppInitializer : DropCreateDatabaseIfModelChanges<AppContext>
    {
        protected override void Seed(AppContext context)
        {
            IList<Job> jobs = new List<Job>()
            {
                new Job() {Name = "Кассир"},
                new Job() {Name = "Начальник"},
                new Job() {Name = "Охранник"},
            };
            context.jobs.AddRange(jobs);
            IList<Worker> workers = new List<Worker>()
            {
                new Worker(){FIO = "Работник1", Job = jobs.First(x => x.Name == "Кассир")},
                new Worker(){FIO = "Работник2", Job = jobs.First(x => x.Name == "Начальник")},
                new Worker(){FIO = "Работник3", Job = jobs.First(x => x.Name == "Кассир")},
            };
            context.workers.AddRange(workers);
            IList<BoxOffice> boxes = new List<BoxOffice>()
            {
                new BoxOffice(){ Number = 1, Worker = workers.First(x=>x.FIO == "Работник1")},
                new BoxOffice(){ Number = 2, Worker = workers.First(x=>x.FIO == "Работник3")}
            };
            context.workers.AddRange(workers);
            IList<Director> directors = new List<Director>()
            {
                new Director() {FIO = "директор1"},
                new Director() {FIO = "директор2"},
                new Director() {FIO = "директор3"}
            };
            context.directors.AddRange(directors);
            IList<Hall> halls = new List<Hall>()
            {
                new Hall(){Number = 1, capacity = 20},
                new Hall(){Number = 2, capacity = 40},
                new Hall(){Number = 3, capacity = 60}
            };
            context.halls.AddRange(halls);
            IList<Film> films = new List<Film>()
            {
                new Film(){Year = 2020, name_RU = "Название1", name_EN= "Name1", slogan = "slogan1", Director = directors.First(x=>x.FIO == "директор1")},
                new Film(){Year = 2019, name_RU = "Название2", name_EN= "Name2", slogan = "slogan2", Director = directors.First(x=>x.FIO == "директор2")},
                new Film() { Year = 2021, name_RU = "Название3", name_EN = "Name4", slogan = "slogan5", Director = directors.First(x => x.FIO == "директор1")}
            }; 
            context.films.AddRange(films);
            IList<Session> sessions = new List<Session>()
            {
                new Session(){
                    begin = DateTime.Parse("2020-10-01T19:35"),
                    end =   DateTime.Parse("2020-10-01T21:35"),
                    Hall = halls.First(x=>x.Number == 1),
                    film = films.First(x=>x.slogan == "slogan1")
                },
                new Session(){
                    begin = DateTime.Parse("2020-09-01T13:00"),
                    end =   DateTime.Parse("2020-09-01T14:30"),
                    Hall = halls.First(x=>x.Number == 3),
                    film = films.First(x=>x.slogan == "slogan2")
                }
            };
            context.sessions.AddRange(sessions);
            IList<Ticket> tickets = new List<Ticket>()
            {
                new Ticket()
                {
                    cost = 600,
                    seat_number =39,
                    session = sessions.First(x => x.Hall.capacity>=39),
                    boxOffice = boxes.First(x=>x.Number ==1)
                },
                new Ticket()
                {
                    cost = 500,
                    seat_number =15,
                    session = sessions.First(x => x.Hall.capacity>=15),
                    boxOffice = boxes.First(x=>x.Number ==2)
                },
            };
            context.SaveChanges();
            base.Seed(context);
        }
    }
}
