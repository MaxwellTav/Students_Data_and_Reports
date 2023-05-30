Create Database AcademiaDB;

Use AcademiaDB;

CREATE TABLE DatosPersonales (
    IDEstudiante INT PRIMARY KEY Identity,
    Cedula VARCHAR(255),
    Nombre VARCHAR(255),
    Apellido VARCHAR(255),
    Sexo VARCHAR(50),
    Correo VARCHAR(100),
    Foto Varchar(255),
    EstadoCivil VARCHAR(30),
    Telefono VARCHAR(30),
    RedesSociales VARCHAR(255),
    FechaNatal Varchar(30),
    Nacionalidad VARCHAR(255),
    Dirección VARCHAR(255),
    ComunidadIndegena VARCHAR(255),
    PaisResidencia VARCHAR(255),
    Estado Varchar(255),
    Ciudad VARCHAR(255),
    Creencias VARCHAR(255),
	--18
    
	GradoInstruccion VARCHAR(255),
    OtraCarrera VARCHAR(255),
    CarreraCursante VARCHAR(255),
    SemetreCursante INT,
    TurnoEnQueEstudia VARCHAR(255),
    Residencia VARCHAR(255),
    ModalidadEstudio VARCHAR(255),

	AsociacionAdventista VARCHAR(255)
	--26
);

INSERT INTO DatosPersonales 
(Cedula, Nombre, Apellido, Sexo, Correo, Foto, EstadoCivil, Telefono, RedesSociales, FechaNatal, 
Nacionalidad, Dirección, ComunidadIndegena, PaisResidencia, Estado, Ciudad, Creencias, 
GradoInstruccion, OtraCarrera, CarreraCursante, SemetreCursante, TurnoEnQueEstudia, Residencia, 
ModalidadEstudio, AsociacionAdventista)
VALUES (
    CAST(RAND() * 1000000 AS INT), 
    CONCAT('Nombre ', CAST(RAND() * 100 AS INT)), 
    CONCAT('Apellido ', CAST(RAND() * 100 AS INT)), 
    CONCAT('Sexo ', CAST(RAND() * 100 AS INT)), 
    CONCAT('correo ', CAST(RAND() * 100 AS INT), '@ejemplo.com'), 
    CONCAT('Foto ', CAST(RAND() * 100 AS INT), '.jpg'), 
    CONCAT('EstadoCivil ', CAST(RAND() * 100 AS INT)), 
    CONCAT('Telefono ', CAST(RAND() * 100 AS INT)), 
    CONCAT('RedesSociales ', CAST(RAND() * 100 AS INT)), 
    CONCAT('FechaNatal ', CAST(RAND() * 100 AS INT)), 
    CONCAT('Nacionalidad ', CAST(RAND() * 100 AS INT)), 
    CONCAT('Dirección ', CAST(RAND() * 100 AS INT)), 
    CONCAT('ComunidadIndegena ', CAST(RAND() * 100 AS INT)), 
    CONCAT('PaisResidencia ', CAST(RAND() * 100 AS INT)), 
    CONCAT('Estado ', CAST(RAND() * 100 AS INT)), 
    CONCAT('Ciudad ', CAST(RAND() * 100 AS INT)), 
    CONCAT('Creencias ', CAST(RAND() * 100 AS INT)),
    CONCAT('GradoInstruccion ', CAST(RAND() * 100 AS INT)),
    CONCAT('OtraCarrera ', CAST(RAND() * 100 AS INT)), 
    CONCAT('CarreraCursante ', CAST(RAND() * 100 AS INT)), 
    CAST(RAND() * 10 + 1 AS INT), 
    CONCAT('TurnoEnQueEstudia ', CAST(RAND() * 100 AS INT)), 
    CONCAT('Residencia ', CAST(RAND() * 100 AS INT)), 
    CONCAT('ModalidadEstudio ', CAST(RAND() * 100 AS INT)),
	CONCAT('Asociación Adventista ', CAST(RAND() * 100 AS INT))
);

Drop Table DatosPersonales;
