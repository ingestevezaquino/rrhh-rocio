using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProyectoRRHH.Models;

namespace ProyectoRRHH;

public partial class rrhhContext : IdentityDbContext<IdentityUser>
{
    public rrhhContext()
    {
    }

    public rrhhContext(DbContextOptions<rrhhContext> options)
        : base(options)
    {
    }

    public virtual DbSet<candidato> candidatos { get; set; }

    public virtual DbSet<capacitacione> capacitaciones { get; set; }

    public virtual DbSet<competencia> competencias { get; set; }

    public virtual DbSet<departamento> departamentos { get; set; }

    public virtual DbSet<empleado> empleados { get; set; }

    public virtual DbSet<idioma> idiomas { get; set; }

    public virtual DbSet<puesto> puestos { get; set; }

    public virtual DbSet<usuario> usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<candidato>(entity =>
        {
            entity.HasKey(e => e.id).HasName("pk_candidatos_id");

            entity.HasIndex(e => e.cedula, "uq_candidatos_cedula").IsUnique();

            entity.Property(e => e.cedula)
                .IsRequired()
                .HasMaxLength(30);
            entity.Property(e => e.departamento).HasMaxLength(30);
            entity.Property(e => e.empresa).HasMaxLength(100);
            entity.Property(e => e.explaboral).HasMaxLength(50);
            entity.Property(e => e.nombre).HasMaxLength(60);
            entity.Property(e => e.puestoaspira).HasMaxLength(50);
            entity.Property(e => e.puestoocupado).HasMaxLength(100);
            entity.Property(e => e.recomendadopor).HasMaxLength(60);
            entity.Property(e => e.salario).HasMaxLength(20);
            entity.Property(e => e.salarioaspira).HasMaxLength(10);

            entity.HasOne(d => d.departamentoNavigation).WithMany(p => p.candidatos)
                .HasPrincipalKey(p => p.departamento1)
                .HasForeignKey(d => d.departamento)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_candidatos_departamento");

            entity.HasOne(d => d.puestoaspiraNavigation).WithMany(p => p.candidatos)
                .HasPrincipalKey(p => p.nombre)
                .HasForeignKey(d => d.puestoaspira)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_candidatos_puestoaspira");

            entity.HasMany(d => d.competencia).WithMany(p => p.candidatos)
                .UsingEntity<Dictionary<string, object>>(
                    "candidatoscompetencia",
                    r => r.HasOne<competencia>().WithMany()
                        .HasForeignKey("competenciaid")
                        .HasConstraintName("fk_candidatos_competencias_competenciaid"),
                    l => l.HasOne<candidato>().WithMany()
                        .HasForeignKey("candidatoid")
                        .HasConstraintName("fk_candidatos_competencias_candidatoid"),
                    j =>
                    {
                        j.HasKey("candidatoid", "competenciaid").HasName("pk_candidatos_competencias");
                        j.ToTable("candidatoscompetencias");
                    });

            entity.HasMany(d => d.idiomas).WithMany(p => p.candidatos)
                .UsingEntity<Dictionary<string, object>>(
                    "candidatosidioma",
                    r => r.HasOne<idioma>().WithMany()
                        .HasForeignKey("idiomasid")
                        .HasConstraintName("fk_candidatos_idiomas_idiomasid"),
                    l => l.HasOne<candidato>().WithMany()
                        .HasForeignKey("candidatoid")
                        .HasConstraintName("fk_candidatos_idiomas_candidatoid"),
                    j =>
                    {
                        j.HasKey("candidatoid", "idiomasid").HasName("pk_candidatos_idiomas");
                        j.ToTable("candidatosidiomas");
                    });
        });

        modelBuilder.Entity<capacitacione>(entity =>
        {
            entity.HasKey(e => e.id).HasName("pk_capacitacion_id");

            entity.Property(e => e.descripcion).HasMaxLength(100);
            entity.Property(e => e.institucion).HasMaxLength(50);
            entity.Property(e => e.nivel).HasMaxLength(20);

            entity.HasOne(d => d.candidato).WithMany(p => p.capacitaciones)
                .HasForeignKey(d => d.candidato_id)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("capacitaciones_candidato_id_fkey");
        });

        modelBuilder.Entity<competencia>(entity =>
        {
            entity.HasKey(e => e.id).HasName("pk_competencia_id");

            entity.HasIndex(e => e.descripcion, "uq_competencias_descripcioncompetencia").IsUnique();

            entity.Property(e => e.descripcion).HasMaxLength(100);
        });

        modelBuilder.Entity<departamento>(entity =>
        {
            entity.HasKey(e => e.id).HasName("pk_departamento_id");

            entity.HasIndex(e => e.departamento1, "uq_departamentos_departamento").IsUnique();

            entity.Property(e => e.departamento1)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("departamento");
        });

        modelBuilder.Entity<empleado>(entity =>
        {
            entity.HasKey(e => e.id).HasName("pk_empleados_id");

            entity.HasIndex(e => e.cedula, "uq_empleados_cedula").IsUnique();

            entity.Property(e => e.cedula).HasMaxLength(30);
            entity.Property(e => e.departamento).HasMaxLength(50);
            entity.Property(e => e.nombre).HasMaxLength(50);
            entity.Property(e => e.puesto).HasMaxLength(50);
            entity.Property(e => e.salariomensual).HasMaxLength(10);

            entity.HasOne(d => d.cedulaNavigation).WithOne(p => p.empleado)
                .HasPrincipalKey<candidato>(p => p.cedula)
                .HasForeignKey<empleado>(d => d.cedula)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_empleados_cedula");

            entity.HasOne(d => d.departamentoNavigation).WithMany(p => p.empleados)
                .HasPrincipalKey(p => p.departamento1)
                .HasForeignKey(d => d.departamento)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_empleados_departamento");

            entity.HasOne(d => d.puestoNavigation).WithMany(p => p.empleados)
                .HasPrincipalKey(p => p.nombre)
                .HasForeignKey(d => d.puesto)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_empleados_puesto");
        });

        modelBuilder.Entity<idioma>(entity =>
        {
            entity.HasKey(e => e.id).HasName("pk_idiomas_id");

            entity.HasIndex(e => e.nombre, "uq_idiomas_nombre").IsUnique();

            entity.Property(e => e.nivel).HasMaxLength(20);
            entity.Property(e => e.nombre).HasMaxLength(20);
        });

        modelBuilder.Entity<puesto>(entity =>
        {
            entity.HasKey(e => e.id).HasName("pk_puesto_id");

            entity.HasIndex(e => e.nombre, "uq_puestos_nombre").IsUnique();

            entity.Property(e => e.nivelriesgo).HasMaxLength(10);
            entity.Property(e => e.nombre)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.salariomax).HasMaxLength(20);
            entity.Property(e => e.salariomin).HasMaxLength(20);
        });

        modelBuilder.Entity<usuario>(entity =>
        {
            entity.HasKey(e => e.id).HasName("pk_usuario_id");

            entity.Property(e => e.email).HasMaxLength(100);
            entity.Property(e => e.emailnormalizado).HasColumnType("character varying");
            entity.Property(e => e.password).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
