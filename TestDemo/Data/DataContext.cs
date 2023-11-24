using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDemo.Data
{
    internal class DataContext : DbContext
    {
        public DbSet<Entity.Test> Tests { get; set; }

        public DbSet<Entity.Question> Questionts { get; set; }

        public DataContext() : base()
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder         // налаштування підключення до БД
                .UseSqlServer(     // з пакету SqlServer - драйвери МS SQL
                    @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\1\TestDemo\TestDemo\DatabaseTestDemo.mdf;Integrated Security=True"
                );                 // рядок підключення - до неіснуючої (або порожної) БД
        }
    }
}
