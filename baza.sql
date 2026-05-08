USE master;
GO

CREATE DATABASE SignalR;
GO

USE SignalR;
GO


-- kreiranje tabele korisnici

CREATE TABLE Korisnici(
	
	Id INT IDENTITY(1,1) PRIMARY KEY,
	KorisnickoIme NVARCHAR(50) NOT NULL UNIQUE,
	EmailAdresa NVARCHAR(50) NOT NULL UNIQUE,
	LozinkaHash NVARCHAR(255) NOT NULL
	);
GO
-- razmena poruka izmedju dva korisnika

CREATE TABLE PrivatnePoruke(
	
	Id INT IDENTITY(1,1) PRIMARY KEY,
	PosiljalacId INT NOT NULL,
	PrimalacId INT NOT NULL,
	SadrzajPoruke NVARCHAR(MAX) NOT NULL,
	DatumSlanjaPoruke DATETIME DEFAULT GETDATE(),
	FOREIGN KEY(PosiljalacId) REFERENCES Korisnici(Id),
	FOREIGN KEY(PrimalacId) REFERENCES Korisnici(Id)
	);
GO
-- kreiranje grupa

CREATE TABLE Grupa(
	
	Id INT IDENTITY(1,1) PRIMARY KEY,
	NazivGrupe NVARCHAR(50) NOT NULL,
	GrupuKreiraoId INT NOT NULL,
	FOREIGN KEY(GrupuKreiraoId) REFERENCES Korisnici(Id)
	);
GO
CREATE TABLE ClanoviGrupe(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	GrupaId INT NOT NULL,
	KorisnikId INT NOT NULL,
	FOREIGN KEY(GrupaId) REFERENCES Grupa(Id),
	FOREIGN KEY(KorisnikId) REFERENCES Korisnici(Id)
	);
GO

CREATE TABLE GrupnePoruke(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	GrupaId INT NOT NULL,
	PosiljalacId INT NOT NULL,
	Poruka NVARCHAR(MAX) NOT NULL,
	DatumSlanjaPoruke DATETIME DEFAULT GETDATE(),
	FOREIGN KEY (GrupaId) REFERENCES Grupa(Id),
	FOREIGN KEY(PosiljalacId) REFERENCES Korisnici(Id)
	);
GO


