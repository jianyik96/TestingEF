using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingEF.Domain;

namespace TestingEF.Data
{
    public class SchoolDBContext: DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Grade> Grades { get; set; }

        private DB iConnectionNum = 0;
        public DB ConnectionNum
        {
            get { return iConnectionNum; }
            set { iConnectionNum = value; }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            switch(iConnectionNum)
            {
                case DB.Online:
                    optionsBuilder.UseSqlServer(
                            @"Data Source= (localdb)\MSSQLLocalDB; Initial Catalog=SchoolAppData");
                    break;

                case DB.Offline:
                    optionsBuilder.UseSqlServer(
                            @"Data Source= (localdb)\ProjectsV13; Initial Catalog=SchoolAppData1");
                    break;
            }
            
        }
    }

    public enum DB
    {
        Online,
        Offline
    }
}
