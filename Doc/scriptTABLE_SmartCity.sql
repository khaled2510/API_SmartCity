
--DROP DATABASE [bdAgendNam]

--CREATE DATABASE [agendNam]

--GO


DROP TABLE [dbo].[NombreJaime]
DROP TABLE [dbo].[Commentaire]
DROP TABLE [dbo].[Presentation]
DROP TABLE [dbo].[Participation]
DROP TABLE [dbo].[Evenement]
DROP TABLE [dbo].[Categorie]
DROP TABLE [dbo].[Utilisateur]

USE [agendanam2];

GO

CREATE TABLE [dbo].[Utilisateur](
	[Pseudo] [nvarchar] (50) constraint PseudoUtilisateurPK primary key,
	[Nom] [nvarchar] (50) constraint Nom_NN_Utilisateur NOT NULL,
	[Prenom] [nvarchar] (50) constraint Prenom_NN_Utilisateur NOT NULL,
	[EMail] [nvarchar] (100) constraint EMail_NN_Utilisateur NOT NULL,
	[MotDePasse] [nvarchar] (100) constraint MotDePasse_NN_Utilisateur NOT NULL,
	[Role] [nvarchar] (20) constraint Role_NN_Utilisateur NOT NULL, 
	[RowVersion] [rowversion] constraint RowVersion_NN_Utilisateur NOT NULL, 
)

GO

CREATE TABLE [dbo].[Categorie](
	[Id] [int] IDENTITY (1,1) constraint IdCategoriePK primary key,
	[Libelle] [nvarchar] (100) constraint Libelle_NN_Categorie NOT NULL,
	[Image] [nvarchar] (100),
	[RowVersion] [rowversion] constraint RowVersion_NN_Categorie NOT NULL, 
)

GO

CREATE TABLE [dbo].[Evenement](
	[Id] [int] IDENTITY (1,1) constraint IdEvenementPK primary key,
	[Nom] [nvarchar] (50) constraint Nom_NN_Evenement NOT NULL,
	[Description] [nvarchar] (1500) constraint Description_NN_Evenement NOT NULL,
	[Rue] [nvarchar] (200) constraint Lieu_NN_Evenement NOT NULL,
	[Numero] [nvarchar] (10),
	[CodePostal] [int] constraint codePostal_NN_Evenement NOT NULL,
	[Localite] [nvarchar] (50) constraint localite_NN_Evenement NOT NULL,
	[DateCreationEvenement] DATE DEFAULT GETDATE (),
	[CategorieId] [int] constraint Categorie_NN_Evenement NOT NULL, 
	[CreateurId] [nvarchar] (50) constraint Createur_NN_Evenement NOT NULL, 
	[RowVersion] [rowversion] constraint RowVersion_NN_Evenement NOT NULL,
	[ImageUrl] [nvarchar] (100),
	constraint CategorieId_FK_Evenement foreign key([CategorieId]) references [dbo].[Categorie] ([Id]),
	constraint CreateurId_FK_Evenement foreign key([CreateurId]) references [dbo].[Utilisateur] ([Pseudo]) ON DELETE CASCADE
) 

GO

CREATE TABLE [dbo].[Presentation](
	[Id] [int] IDENTITY (1,1) constraint IdPresentationPK primary key,
	[DateHeureDebut] [DateTime] constraint DateHeureDebut_NN_Evenement NOT NULL,
	[DateHeureFin] [DateTime] constraint DateHeureFin_NN_Evenement NOT NULL,
	[EvenementId] [int] constraint Evenement_NN_Evenement NOT NULL, 
	[RowVersion] [rowversion] constraint RowVersion_NN_Presentation NOT NULL,
	constraint EvenementId_FK_Presentation foreign key([EvenementId]) references [dbo].[Evenement] ([Id]) ON DELETE CASCADE
)

GO

CREATE TABLE [dbo].[Commentaire](
	[Id] [int] IDENTITY (1,1) constraint IdCommentairePK primary key,
	[Texte] [nvarchar] (500) constraint Texte_NN_Commentaire NOT NULL,
	[Signaler] [int] DEFAULT 0,
	[AuteurId] [nvarchar] (50) constraint Auteur_NN_Commentaire NOT NULL, 
	[EvenementId] [int] constraint Evenement_NN_Commentaire NOT NULL, 
	[RowVersion] [rowversion] constraint RowVersion_NN_Commentaire NOT NULL,
	constraint AuteurId_FK_Commentaire foreign key([AuteurId]) references [dbo].[Utilisateur] ([Pseudo]),
	constraint EvenementId_FK_Commentaire foreign key([EvenementId]) references [dbo].[Evenement] ([Id]) ON DELETE CASCADE
)

GO

CREATE TABLE [dbo].[Participation](
	[Id] [int] IDENTITY (1,1) constraint IdParticipationPK primary key,
	[ParticipantId] [nvarchar] (50) constraint Participant_NN_Participation NOT NULL,
	[EvenementId] [int] constraint Evenement_NN_Participation NOT NULL, 
	[RowVersion] [rowversion] constraint RowVersion_NN_Participation NOT NULL,
	constraint AuteurId_FK_Participation foreign key([ParticipantId]) references [dbo].[Utilisateur] ([Pseudo]),
	constraint EvenementId_FK_Participation foreign key([EvenementId]) references [dbo].[Evenement] ([Id]) ON DELETE CASCADE
)

GO

CREATE TABLE [dbo].[NombreJaime](
	[Id] [int] IDENTITY (1,1) constraint IdNombreJaimePK primary key,
	[UtilisateurId] [nvarchar] (50) constraint IdUtilisateur_NN_NombreJaime NOT NULL,
	[EvenementId] [int] constraint Evenement_NN_NombreJaime NOT NULL,
	constraint EvenementId_FK_NombreJaime foreign key ([EvenementId]) references [dbo].[Evenement] ([Id]) ON DELETE CASCADE
)

GO


