﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CarRepair
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CarRepairEntities6 : DbContext
    {
        public CarRepairEntities6()
            : base("name=CarRepairEntities6")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Mark> Marks { get; set; }
        public virtual DbSet<ModelCar> ModelCars { get; set; }
        public virtual DbSet<OrderCar> OrderCars { get; set; }
        public virtual DbSet<Qualification> Qualifications { get; set; }
        public virtual DbSet<RepeatHistory> RepeatHistories { get; set; }
        public virtual DbSet<RoleStaff> RoleStaffs { get; set; }
        public virtual DbSet<ScheduleStaff> ScheduleStaffs { get; set; }
        public virtual DbSet<SparePart> SpareParts { get; set; }
        public virtual DbSet<Staff> Staffs { get; set; }
        public virtual DbSet<StatusCar> StatusCars { get; set; }
        public virtual DbSet<STO> STOes { get; set; }
        public virtual DbSet<TypeClient> TypeClients { get; set; }
        public virtual DbSet<UserCredential> UserCredentials { get; set; }
        public virtual DbSet<WorkloadCar> WorkloadCars { get; set; }
    }
}
