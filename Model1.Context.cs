﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Library
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Authors> Authors { get; set; }
        public virtual DbSet<Books> Books { get; set; }
        public virtual DbSet<Departments> Departments { get; set; }
        public virtual DbSet<Genders> Genders { get; set; }
        public virtual DbSet<Issuance> Issuance { get; set; }
        public virtual DbSet<Publishers> Publishers { get; set; }
        public virtual DbSet<Readers> Readers { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
    }
}
