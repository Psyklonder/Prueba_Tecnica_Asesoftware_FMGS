using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Prueba_Tecnica_Asesoftware_FMGS.DA.Entities;

namespace Prueba_Tecnica_Asesoftware_FMGS.DA.Context
{
    public partial class AsesoftwareDbContext : DbContext
    {
        public AsesoftwareDbContext()
        {
        }

        public AsesoftwareDbContext(DbContextOptions<AsesoftwareDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<comercios> comercios { get; set; } = null!;
        public virtual DbSet<servicios> servicios { get; set; } = null!;
        public virtual DbSet<turnos> turnos { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<comercios>(entity =>
            {
                entity.HasKey(e => e.id_comercio);

                entity.Property(e => e.nom_comercio)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<servicios>(entity =>
            {
                entity.HasKey(e => e.id_servicio);

                entity.Property(e => e.nom_servicio)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.id_comercioNavigation)
                    .WithMany(p => p.servicios)
                    .HasForeignKey(d => d.id_comercio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_servicios_comercios");
            });

            modelBuilder.Entity<turnos>(entity =>
            {
                entity.HasKey(e => e.id_turno);

                entity.Property(e => e.fecha_turno).HasColumnType("date");

                entity.HasOne(d => d.id_servicioNavigation)
                    .WithMany(p => p.turnos)
                    .HasForeignKey(d => d.id_servicio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_turnos_servicios");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
