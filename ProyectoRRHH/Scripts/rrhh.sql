CREATE TABLE Usuarios(
	Id SERIAL,
	Email VARCHAR(100),
	EmailNormalizado VARCHAR,
	Password VARCHAR(100),
	CONSTRAINT pk_usuario_id PRIMARY KEY (Id)
);

CREATE TABLE Competencias(
	Id SERIAL,
	Descripcion VARCHAR(100),
	Estado BOOLEAN,
	CONSTRAINT pk_competencia_id PRIMARY KEY (Id),
	CONSTRAINT uq_competencias_descripcionCompetencia UNIQUE (Descripcion)
);

INSERT INTO Competencias(Id, Descripcion, Estado) VALUES 
(1, 'manejo de Recursos Humanos', true),
(2, 'Uso de Herramientas Ofimaticas', true),
(3, 'Gestion de Presupuesto', true),
(4, 'hablar en publico', true);

CREATE TABLE Idiomas(
	Id SERIAL,
	Nombre VARCHAR(20),
	Nivel VARCHAR(20),
	CONSTRAINT pk_idiomas_id PRIMARY KEY (Id),
	CONSTRAINT uq_idiomas_nombre UNIQUE (Nombre)
);

INSERT INTO Idiomas(Id, Nombre, Nivel) VALUES 
(1, 'Ingles', 'C1'),
(2, 'Español', 'C1'),
(3, 'Frances', 'C1'),
(4, 'Italiano', 'C1');



CREATE TABLE Puestos(
	Id SERIAL,
	Nombre VARCHAR(50),
	NivelRiesgo VARCHAR(10),
	SalarioMin VARCHAR(20),
	SalarioMax VARCHAR(20),
	Estado BOOLEAN,
	CONSTRAINT pk_puesto_id PRIMARY KEY (Id),
	CONSTRAINT uq_puestos_nombre UNIQUE (Nombre)
);

CREATE TABLE Departamentos(
	Id SERIAL,
	Departamento VARCHAR(50),
	CONSTRAINT pk_departamento_id PRIMARY KEY (Id),
	CONSTRAINT uq_departamentos_departamento UNIQUE (Departamento)
);

/*CREATE TABLE ExpLaboral(
	Id SERIAL,
	Empresa VARCHAR(100),
	PuestoOcupado VARCHAR(100),
	FechaDesde DATE,
	FechaHasta DATE,
	Salario VARCHAR(20),
	CONSTRAINT pk_expLaboral_id PRIMARY KEY (Id),
	CONSTRAINT uq_explaboral_empresa UNIQUE (Empresa)
);*/

CREATE TABLE Candidatos(
	Id SERIAL,
	Cedula VARCHAR(30),
	Nombre VARCHAR(60),
	PuestoAspira VARCHAR(50),
	Departamento VARCHAR(30),
	SalarioAspira VARCHAR(10),
	ExpLaboral VARCHAR(50),
	Empresa VARCHAR(100),
	PuestoOcupado VARCHAR(100),
	FechaDesde DATE,
	FechaHasta DATE,
	Salario VARCHAR(20),
	RecomendadoPor VARCHAR(60),
	CONSTRAINT pk_candidatos_id PRIMARY KEY (Id),
	CONSTRAINT uq_candidatos_cedula UNIQUE (Cedula),
	CONSTRAINT fk_candidatos_puestoAspira FOREIGN KEY (PuestoAspira) REFERENCES Puestos(Nombre) ON DELETE CASCADE,
	CONSTRAINT fk_candidatos_departamento FOREIGN KEY (Departamento) REFERENCES Departamentos(Departamento) ON DELETE CASCADE
);

CREATE TABLE Capacitaciones(
	Id SERIAL,
	candidato_id INT,
	Descripcion VARCHAR(100),
	Nivel VARCHAR(20),
	FechaDesde DATE,
	FechaHasta DATE,
	Institucion VARCHAR(50),
	CONSTRAINT pk_capacitacion_id PRIMARY KEY (Id),
	/*CONSTRAINT uq_capacitaciones_descripcionCapacitacion UNIQUE (Descripcion),*/
	FOREIGN KEY (candidato_id) REFERENCES Candidatos (id) ON DELETE CASCADE

);
------------------------------------
CREATE TABLE CandidatosCompetencias (
	CandidatoId INT,
	CompetenciaId INT,
	CONSTRAINT pk_candidatos_competencias PRIMARY KEY (CandidatoId, CompetenciaId),
	CONSTRAINT fk_candidatos_competencias_candidatoId FOREIGN KEY (CandidatoId) REFERENCES Candidatos(Id) ON DELETE CASCADE,
	CONSTRAINT fk_candidatos_competencias_competenciaId FOREIGN KEY (CompetenciaId) REFERENCES Competencias(Id) ON DELETE CASCADE
);

/*CREATE TABLE CandidatosCapacitaciones (
	CandidatoId INT,
	CapacitacionesId INT,
	CONSTRAINT pk_candidatos_capacitaciones PRIMARY KEY (CandidatoId, CapacitacionesId),
	CONSTRAINT fk_candidatos_capacitaciones_candidatoId FOREIGN KEY (CandidatoId) REFERENCES Candidatos(Id),
	CONSTRAINT fk_candidatos_capacitaciones_competenciaId FOREIGN KEY (CapacitacionesId) REFERENCES Capacitaciones(Id)
);*/

CREATE TABLE CandidatosIdiomas (
	CandidatoId INT,
	IdiomasId INT,
	CONSTRAINT pk_candidatos_idiomas PRIMARY KEY (CandidatoId, IdiomasId),
	CONSTRAINT fk_candidatos_idiomas_candidatoId FOREIGN KEY (CandidatoId) REFERENCES Candidatos(Id) ON DELETE CASCADE,
	CONSTRAINT fk_candidatos_idiomas_idiomasId FOREIGN KEY (IdiomasId) REFERENCES Idiomas(Id) ON DELETE CASCADE
);
--------------------------------------
CREATE TABLE Empleados(
	Id SERIAL,
	Cedula VARCHAR(30),
	Nombre VARCHAR(50),
	FechaIngreso DATE,
	Departamento VARCHAR(50),
	Puesto VARCHAR(50),
	SalarioMensual VARCHAR(10),
	Estado BOOLEAN,
	CONSTRAINT pk_empleados_id PRIMARY KEY (Id),
	CONSTRAINT fk_empleados_cedula FOREIGN KEY (Cedula) REFERENCES Candidatos(Cedula) ON DELETE CASCADE,
	CONSTRAINT uq_empleados_cedula UNIQUE (Cedula),
	CONSTRAINT fk_empleados_departamento FOREIGN KEY (Departamento) REFERENCES Departamentos(Departamento) ON DELETE CASCADE,
	CONSTRAINT fk_empleados_puesto FOREIGN KEY (Puesto) REFERENCES Puestos(Nombre) ON DELETE CASCADE
);



INSERT INTO empleados (cedula, nombre, departamento_id, puesto_id)
VALUES (123456789, 'Juan Pérez', 1, 1);

INSERT INTO empleados (cedula, nombre, departamento_id, puesto_id)
VALUES (987654321, 'María Rodríguez', 2, 2);

SELECT e.Nombre, d.Departamento, e.Puesto
FROM empleados e
JOIN departamentos d ON e.departamento = d.departamento
JOIN puestos p ON e.Nombre = e.puesto
WHERE e.Cedula = '40214026128';

