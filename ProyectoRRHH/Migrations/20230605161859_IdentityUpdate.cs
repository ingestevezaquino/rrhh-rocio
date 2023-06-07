using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ProyectoRRHH.Migrations
{
    /// <inheritdoc />
    public partial class IdentityUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.InsertData("AspNetRoles",
               columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
               values: new object[,] {
                       { "1", "Admin", "ADMIN", Guid.NewGuid().ToString() },
               });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "competencias",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    descripcion = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    estado = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_competencia_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "departamentos",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    departamento = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_departamento_id", x => x.id);
                    table.UniqueConstraint("AK_departamentos_departamento", x => x.departamento);
                });

            migrationBuilder.CreateTable(
                name: "idiomas",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    nivel = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_idiomas_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "puestos",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    nivelriesgo = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    salariomin = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    salariomax = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    estado = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_puesto_id", x => x.id);
                    table.UniqueConstraint("AK_puestos_nombre", x => x.nombre);
                });

            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    emailnormalizado = table.Column<string>(type: "character varying", nullable: true),
                    password = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_usuario_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "candidatos",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cedula = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    nombre = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: true),
                    puestoaspira = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    departamento = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    salarioaspira = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    explaboral = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    empresa = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    puestoocupado = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    fechadesde = table.Column<DateOnly>(type: "date", nullable: true),
                    fechahasta = table.Column<DateOnly>(type: "date", nullable: true),
                    salario = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    recomendadopor = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_candidatos_id", x => x.id);
                    table.UniqueConstraint("AK_candidatos_cedula", x => x.cedula);
                    table.ForeignKey(
                        name: "fk_candidatos_departamento",
                        column: x => x.departamento,
                        principalTable: "departamentos",
                        principalColumn: "departamento",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_candidatos_puestoaspira",
                        column: x => x.puestoaspira,
                        principalTable: "puestos",
                        principalColumn: "nombre",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "candidatoscompetencias",
                columns: table => new
                {
                    candidatoid = table.Column<int>(type: "integer", nullable: false),
                    competenciaid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_candidatos_competencias", x => new { x.candidatoid, x.competenciaid });
                    table.ForeignKey(
                        name: "fk_candidatos_competencias_candidatoid",
                        column: x => x.candidatoid,
                        principalTable: "candidatos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_candidatos_competencias_competenciaid",
                        column: x => x.competenciaid,
                        principalTable: "competencias",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "candidatosidiomas",
                columns: table => new
                {
                    candidatoid = table.Column<int>(type: "integer", nullable: false),
                    idiomasid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_candidatos_idiomas", x => new { x.candidatoid, x.idiomasid });
                    table.ForeignKey(
                        name: "fk_candidatos_idiomas_candidatoid",
                        column: x => x.candidatoid,
                        principalTable: "candidatos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_candidatos_idiomas_idiomasid",
                        column: x => x.idiomasid,
                        principalTable: "idiomas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "capacitaciones",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    candidato_id = table.Column<int>(type: "integer", nullable: true),
                    descripcion = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    nivel = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    fechadesde = table.Column<DateOnly>(type: "date", nullable: true),
                    fechahasta = table.Column<DateOnly>(type: "date", nullable: true),
                    institucion = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_capacitacion_id", x => x.id);
                    table.ForeignKey(
                        name: "capacitaciones_candidato_id_fkey",
                        column: x => x.candidato_id,
                        principalTable: "candidatos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "empleados",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cedula = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    fechaingreso = table.Column<DateOnly>(type: "date", nullable: true),
                    departamento = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    puesto = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    salariomensual = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    estado = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_empleados_id", x => x.id);
                    table.ForeignKey(
                        name: "fk_empleados_cedula",
                        column: x => x.cedula,
                        principalTable: "candidatos",
                        principalColumn: "cedula",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_empleados_departamento",
                        column: x => x.departamento,
                        principalTable: "departamentos",
                        principalColumn: "departamento",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_empleados_puesto",
                        column: x => x.puesto,
                        principalTable: "puestos",
                        principalColumn: "nombre",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_candidatos_departamento",
                table: "candidatos",
                column: "departamento");

            migrationBuilder.CreateIndex(
                name: "IX_candidatos_puestoaspira",
                table: "candidatos",
                column: "puestoaspira");

            migrationBuilder.CreateIndex(
                name: "uq_candidatos_cedula",
                table: "candidatos",
                column: "cedula",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_candidatoscompetencias_competenciaid",
                table: "candidatoscompetencias",
                column: "competenciaid");

            migrationBuilder.CreateIndex(
                name: "IX_candidatosidiomas_idiomasid",
                table: "candidatosidiomas",
                column: "idiomasid");

            migrationBuilder.CreateIndex(
                name: "IX_capacitaciones_candidato_id",
                table: "capacitaciones",
                column: "candidato_id");

            migrationBuilder.CreateIndex(
                name: "uq_competencias_descripcioncompetencia",
                table: "competencias",
                column: "descripcion",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "uq_departamentos_departamento",
                table: "departamentos",
                column: "departamento",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_empleados_departamento",
                table: "empleados",
                column: "departamento");

            migrationBuilder.CreateIndex(
                name: "IX_empleados_puesto",
                table: "empleados",
                column: "puesto");

            migrationBuilder.CreateIndex(
                name: "uq_empleados_cedula",
                table: "empleados",
                column: "cedula",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "uq_idiomas_nombre",
                table: "idiomas",
                column: "nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "uq_puestos_nombre",
                table: "puestos",
                column: "nombre",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "candidatoscompetencias");

            migrationBuilder.DropTable(
                name: "candidatosidiomas");

            migrationBuilder.DropTable(
                name: "capacitaciones");

            migrationBuilder.DropTable(
                name: "empleados");

            migrationBuilder.DropTable(
                name: "usuarios");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "competencias");

            migrationBuilder.DropTable(
                name: "idiomas");

            migrationBuilder.DropTable(
                name: "candidatos");

            migrationBuilder.DropTable(
                name: "departamentos");

            migrationBuilder.DropTable(
                name: "puestos");
        }
    }
}
