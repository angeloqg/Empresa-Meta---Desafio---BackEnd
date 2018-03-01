using System;
using System.Collections.Generic;
using System.Text;
using DataLogic.Domain;
using Microsoft.EntityFrameworkCore;

namespace DataLogic.BaseContext
{
    public class BaseContext: DbContext
    {
        private static bool _create = false;
        public BaseContext(DbContextOptions<BaseContext> options) : base(options)
        {
            if (!_create)
            {
                _create = true;
                Database.EnsureCreated();
            }
        }

        public virtual DbSet<Contato> Contato { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contato>()
                .Property(e => e.nome)
                .IsUnicode(false);

            modelBuilder.Entity<Contato>()
                .Property(e => e.canal)
                .IsUnicode(false);

            modelBuilder.Entity<Contato>()
                .Property(e => e.valor)
                .IsUnicode(false);

            modelBuilder.Entity<Contato>()
                .Property(e => e.obs)
                .IsUnicode(false);
        }
    }
}
