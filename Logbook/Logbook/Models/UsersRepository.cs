using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Logbook.Models
{
    public static class UsersRepository
    {
        static LogbookDBEntities db;
        static UsersRepository()
        {
            db = new LogbookDBEntities();
        }

        public static bool ContainsUser(string login)
        {
            if (db.Users.Where(p => p.Login == login).Count() > 0)
                return true;
            return false;
        }
        public static void AddUser(string login, string name, string password, int userType = 0)
        {
            string pass = HashPassword(password);   
            if (!ContainsUser(login))
            {
                User user = new User()
                {
                    Login = login,
                    Name = name,
                    Password = pass,
                    UserType = userType,
                    Date_Reg = DateTime.Now.Date
                };

                db.Users.Add(user);

                //Закинуть первый визит как уникального юзера
                AddVisit(user);
            }
        }
        public static List<User> GetUsers()
        {
            return db.Users.ToList();
        }

        public static User GetUser(string login)
        {
            return db.Users.Where(p => p.Login == login).First();
        }

        public static void AddVisit(User user)
        {
            if(user.UserType == 0)
            {
                DateTime currentTime = DateTime.Now.Date;
                Visit visit = new Visit()
                {
                    UserID = user.ID,
                    Date_Visit = currentTime
                };

                if (db.Visits.Where(p => p.UserID == user.ID && p.Date_Visit == currentTime).Count() < 1)
                {
                    db.Visits.Add(visit);
                    db.SaveChanges();
                }
            }
        }

        public static List<Visit> GetUniqueVisits(int days = 7)
        {
            List<Visit> uniqueVisits = new List<Visit>();

            DateTime today = DateTime.Now.Date;
            DateTime beginDate = DateTime.Now.Date.AddDays(-days);

            foreach(User user in db.Users.Where(p => EntityFunctions.TruncateTime(p.Date_Reg) <= EntityFunctions.TruncateTime(today) && EntityFunctions.TruncateTime(p.Date_Reg) >= EntityFunctions.TruncateTime((beginDate))))
            {
                Visit visit = db.Visits.Where(p => EntityFunctions.TruncateTime(p.Date_Visit) == EntityFunctions.TruncateTime(user.Date_Reg) && p.UserID == user.ID).First();
                uniqueVisits.Add(visit);
            }

            return uniqueVisits;
        }


        public static List<Visit> GetVisits(int days = 7)
        {
            DateTime today = DateTime.Now.Date;
            DateTime beginDate = DateTime.Now.Date.AddDays(-days);

            List<Visit> visits = db.Visits.Where(p => EntityFunctions.TruncateTime(p.Date_Visit) >= EntityFunctions.TruncateTime(beginDate) && EntityFunctions.TruncateTime(p.Date_Visit) <= EntityFunctions.TruncateTime(today)).ToList();
            return visits;
        }


        public static User GetUserByID(int id)
        {
            return db.Users.Where(p => p.ID == id).First();
        }

        public static string HashPassword(string password)
        {
            var sha = new SHA512CryptoServiceProvider();
            Byte[] inputBytes = Encoding.UTF8.GetBytes(password);

            Byte[] hashedBytes = sha.ComputeHash(inputBytes);

            return BitConverter.ToString(hashedBytes);
        }
    }
}
