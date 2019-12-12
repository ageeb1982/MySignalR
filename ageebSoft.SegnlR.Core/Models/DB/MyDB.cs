using ageebSoft.SignlR.Core.Models.DB;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ageebSoft.SignlR.Core.Models.data
{
    public class MyDB: DbContext
    {
        static string connectionString = "Data Source=.;Initial Catalog=SignalRdb;Integrated Security=True";
        
        public MyDB() :base(  new DbContextOptionsBuilder()
                .UseSqlServer(connectionString).Options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public virtual DbSet<MyUser> MyUsers { set; get; }
        public virtual DbSet<Message> Messages { set; get; }
public virtual DbSet<GroupsOnline> GroupsOnline { set; get; }
public virtual DbSet<UsersOnline> UsersOnline { set; get; }
    }

     
}
