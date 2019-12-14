using ageebSoft.SignlR.Web.Models.DB;
using Microsoft.EntityFrameworkCore;

namespace ageebSoft.SignlR.Web.Models.data
{
    public class MyDB : DbContext
    {
        private static readonly string connectionString = "Data Source=.;Initial Catalog=SignalRdb;Integrated Security=True";

        public MyDB() : base(new DbContextOptionsBuilder()
                .UseSqlServer(connectionString).Options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public virtual DbSet<MyUser> MyUsers { set; get; }
        public virtual DbSet<Message> Messages { set; get; }
        public virtual DbSet<UsersGroupsOnline> UsersGroupsOnline { set; get; }
        public virtual DbSet<GroupsOnline> GroupsOnline { set; get; }
        public virtual DbSet<UsersOnline> UsersOnline { set; get; }
    }


}
