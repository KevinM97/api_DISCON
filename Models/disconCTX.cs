using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace api_DISCON.Models
{
    public partial class disconCTX : DbContext
    {
        public disconCTX()
        {
        }

        public disconCTX(DbContextOptions<disconCTX> options)
            : base(options)
        {
        }

        public virtual DbSet<Articulo> Articulo { get; set; }
        public virtual DbSet<Clasemodulo> Clasemodulo { get; set; }
        public virtual DbSet<Credenciales> Credenciales { get; set; }
        public virtual DbSet<Curso> Curso { get; set; }
        public virtual DbSet<CursoModulo> CursoModulo { get; set; }
        public virtual DbSet<ImagenesObra> ImagenesObra { get; set; }
        public virtual DbSet<ModuloClasemod> ModuloClasemod { get; set; }
        public virtual DbSet<Modulos> Modulos { get; set; }
        public virtual DbSet<Obras> Obras { get; set; }
        public virtual DbSet<PreguntasClas> PreguntasClas { get; set; }
        public virtual DbSet<Productos> Productos { get; set; }
        public virtual DbSet<ProductosTiendas> ProductosTiendas { get; set; }
        public virtual DbSet<RespuestaPregunta> RespuestaPregunta { get; set; }
        public virtual DbSet<Respuestas> Respuestas { get; set; }
        public virtual DbSet<Revista> Revista { get; set; }
        public virtual DbSet<Secciones> Secciones { get; set; }
        public virtual DbSet<SeccionesArticulos> SeccionesArticulos { get; set; }
        public virtual DbSet<Tiendas> Tiendas { get; set; }
        public virtual DbSet<UsuarioCursos> UsuarioCursos { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Articulo>(entity =>
            {
                entity.HasKey(e => e.IdArticulo)
                    .HasName("PRIMARY");

                entity.Property(e => e.ImagenArticulo)
                    .HasCharSet("utf8")
                    .HasCollation("utf8_spanish2_ci");

                entity.Property(e => e.TextoArticulo)
                    .HasCharSet("utf8")
                    .HasCollation("utf8_spanish2_ci");

                entity.Property(e => e.TituloArticulo)
                    .HasCharSet("utf8")
                    .HasCollation("utf8_spanish2_ci");
            });

            modelBuilder.Entity<Clasemodulo>(entity =>
            {
                entity.HasKey(e => e.IdClasmod)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.IdPregunta)
                    .HasName("FK_RELATIONSHIP_2");

                entity.Property(e => e.NombreClasmod)
                    .HasCharSet("utf8")
                    .HasCollation("utf8_spanish2_ci");

                entity.Property(e => e.VideoClasmod)
                    .HasCharSet("utf8")
                    .HasCollation("utf8_spanish2_ci");
            });

            modelBuilder.Entity<Credenciales>(entity =>
            {
                entity.HasKey(e => e.IdCreden)
                    .HasName("PRIMARY");

                entity.Property(e => e.PasswordCreden)
                    .HasCharSet("utf8")
                    .HasCollation("utf8_spanish2_ci");

                entity.Property(e => e.SaltPassword)
                    .HasCharSet("utf8")
                    .HasCollation("utf8_spanish2_ci");

                entity.Property(e => e.UsernameCreden)
                    .HasCharSet("utf8")
                    .HasCollation("utf8_spanish2_ci");
            });

            modelBuilder.Entity<Curso>(entity =>
            {
                entity.HasKey(e => e.IdCurso)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.IdModulo)
                    .HasName("FK_RELATIONSHIP_4");

                entity.HasIndex(e => e.IdUsuario)
                    .HasName("FK_RELATIONSHIP_5");

                entity.Property(e => e.DescripcionCurso)
                    .HasCharSet("utf8")
                    .HasCollation("utf8_spanish2_ci");

                entity.Property(e => e.ImagenCurso)
                    .HasCharSet("utf8")
                    .HasCollation("utf8_spanish2_ci");

                entity.Property(e => e.NombreCurso)
                    .HasCharSet("utf8")
                    .HasCollation("utf8_spanish2_ci");
            });

            modelBuilder.Entity<CursoModulo>(entity =>
            {
                entity.HasKey(e => e.IdCursomod)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.IdCurso)
                    .HasName("FK_RELATIONSHIP_20");

                entity.HasIndex(e => e.IdModulo)
                    .HasName("FK_RELATIONSHIP_19");
            });

            modelBuilder.Entity<ImagenesObra>(entity =>
            {
                entity.HasKey(e => e.IdImagenobra)
                    .HasName("PRIMARY");

                entity.Property(e => e.UrlImagenobra)
                    .HasCharSet("utf8")
                    .HasCollation("utf8_spanish2_ci");
            });

            modelBuilder.Entity<ModuloClasemod>(entity =>
            {
                entity.HasKey(e => e.IdModclas)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.IdClasmod)
                    .HasName("FK_RELATIONSHIP_18");

                entity.HasIndex(e => e.IdModulo)
                    .HasName("FK_RELATIONSHIP_17");
            });

            modelBuilder.Entity<Modulos>(entity =>
            {
                entity.HasKey(e => e.IdModulo)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.IdClasmod)
                    .HasName("FK_RELATIONSHIP_3");

                entity.Property(e => e.TituloModulo)
                    .HasCharSet("utf8")
                    .HasCollation("utf8_spanish2_ci");
            });

            modelBuilder.Entity<Obras>(entity =>
            {
                entity.HasKey(e => e.IdObra)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.IdImagenobra)
                    .HasName("FK_RELATIONSHIP_10");

                entity.Property(e => e.TituloObra)
                    .HasCharSet("utf8")
                    .HasCollation("utf8_spanish2_ci");
            });

            modelBuilder.Entity<PreguntasClas>(entity =>
            {
                entity.HasKey(e => e.IdPregunta)
                    .HasName("PRIMARY");

                entity.Property(e => e.TituloPregunta)
                    .HasCharSet("utf8")
                    .HasCollation("utf8_spanish2_ci");
            });

            modelBuilder.Entity<Productos>(entity =>
            {
                entity.HasKey(e => e.IdProducto)
                    .HasName("PRIMARY");

                entity.Property(e => e.DescripcionProd)
                    .HasCharSet("utf8")
                    .HasCollation("utf8_spanish2_ci");

                entity.Property(e => e.NombreProducto)
                    .HasCharSet("utf8")
                    .HasCollation("utf8_spanish2_ci");
            });

            modelBuilder.Entity<ProductosTiendas>(entity =>
            {
                entity.HasKey(e => e.IdProdtiend)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.IdProducto)
                    .HasName("FK_RELATIONSHIP_12");

                entity.HasIndex(e => e.IdTienda)
                    .HasName("FK_RELATIONSHIP_11");
            });

            modelBuilder.Entity<RespuestaPregunta>(entity =>
            {
                entity.HasKey(e => e.IdPregresp)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.IdPregunta)
                    .HasName("FK_RELATIONSHIP_14");

                entity.HasIndex(e => e.IdRespuesta)
                    .HasName("FK_RELATIONSHIP_13");
            });

            modelBuilder.Entity<Respuestas>(entity =>
            {
                entity.HasKey(e => e.IdRespuesta)
                    .HasName("PRIMARY");

                entity.Property(e => e.TituloRespuesta)
                    .HasCharSet("utf8")
                    .HasCollation("utf8_spanish2_ci");
            });

            modelBuilder.Entity<Revista>(entity =>
            {
                entity.HasKey(e => e.IdRevista)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.IdSeccion)
                    .HasName("FK_RELATIONSHIP_8");

                entity.Property(e => e.NombreRevista)
                    .HasCharSet("utf8")
                    .HasCollation("utf8_spanish2_ci");
            });

            modelBuilder.Entity<Secciones>(entity =>
            {
                entity.HasKey(e => e.IdSeccion)
                    .HasName("PRIMARY");

                entity.Property(e => e.NombreSeccion)
                    .HasCharSet("utf8")
                    .HasCollation("utf8_spanish2_ci");
            });

            modelBuilder.Entity<SeccionesArticulos>(entity =>
            {
                entity.HasKey(e => e.IdSeccart)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.IdArticulo)
                    .HasName("FK_RELATIONSHIP_15");

                entity.HasIndex(e => e.IdSeccion)
                    .HasName("FK_RELATIONSHIP_16");
            });

            modelBuilder.Entity<Tiendas>(entity =>
            {
                entity.HasKey(e => e.IdTienda)
                    .HasName("PRIMARY");

                entity.Property(e => e.NombreTienda)
                    .HasCharSet("utf8")
                    .HasCollation("utf8_spanish2_ci");
            });

            modelBuilder.Entity<UsuarioCursos>(entity =>
            {
                entity.HasKey(e => e.IdUsucu)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.IdCurso)
                    .HasName("FK_RELATIONSHIP_22");

                entity.HasIndex(e => e.IdUsuario)
                    .HasName("FK_RELATIONSHIP_21");
            });

            modelBuilder.Entity<Usuarios>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.IdCreden)
                    .HasName("FK_RELATIONSHIP_6");

                entity.Property(e => e.EmailUsuario)
                    .HasCharSet("utf8")
                    .HasCollation("utf8_spanish2_ci");

                entity.Property(e => e.NombreUsuario)
                    .HasCharSet("utf8")
                    .HasCollation("utf8_spanish2_ci");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
