﻿using Microsoft.EntityFrameworkCore;

namespace MvcStartApp2.Models.Db
{
    public sealed class RequestContext : DbContext
    {
        // Ссылка на таблицу RequestTable
        public DbSet<Request> Requests { get; set; }

        // Логика взаимодействия с таблицами в БД
        public RequestContext(DbContextOptions<RequestContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
