 
INSERT INTO dbo.Categorie(Libelle, [Image])
VALUES ('Soirée', null),
		('Bar-Café', null),
		('Cinéma', null),
		('Festival', null),
		('Concert', null),
		('Sport', null),
		('Loisir', null),
		('Foire-Salon', null),
		('Conférence', null),
		('Théâtre', null),
		('Exposition', null),
		('Musées', null);

INSERT INTO [dbo].[Utilisateur] (Pseudo, Nom, Prenom, EMail, MotDePasse, [Role])
 VALUES
 ('pseudo1', 'Armand', 'Rébecca', 'reb@hotmsil.com', '123', 'Professional'),
 ('pseudo2', 'Max', 'Lenon', 'max@hotmsil.com', '456', 'Subscribe'),
 ('pseudo3', 'khaled', 'hammou', 'reb@hotmsil.com', '123', 'Admin');

 GO

INSERT INTO [dbo].[Evenement] (Nom, [Description], Rue, Numero, CodePostal, Localite, CategorieId, CreateurId, [ImageUrl])
 VALUES
 /*1er event Exposition*/
 ('RECUP ére 2018','Plus de 100 exposants pour tout voir et tout savoir sur l''éco-consommation, la récup'' et le réemploi. Cette année c’est la thématique Art
  et Culture qui sera développée avec l’organisation d’une foire aux artistes Du matériel sera mis à la disposition des artistes afin qu’ils créent une ou plusieurs œuvres pendant 
  le salon pour le plus grand plaisir des visiteurs. Par ailleurs il y aura aussi comme nouveautés : des conférences le samedi et le dimanche, une « ère » Zéro Déchet avec notamment 
  une partie de nos familles Zéro Déchet qui sera présente afin d’échanger sur le sujet avec des visiteurs hésitants à entamer la démarche ou rencontrant des difficultés dans leur parcours ZD.
   La Ville de Namur sera présente au travers d’un stand de 100m2 ayant pour thème « la Récup’ s’invite au jardin ». A cette occasion une nouvelle brochure sera éditée et diffusée. www.recupere.be
    • Espace créateurs : l''art de la récup'' et de l''éco-design • Espace de vente seconde main • Culture et récup'' • Zéro déchet &amp; éco-consommation • Ateliers récup'' • Repair café géant • 
	Foire aux artiste ** NOUVEAU ** • Conférences • Espace enfants : animations récup'' et espace garderie Vendredi samedi et dimanche de 10h à 18h info@recupere.be',
	'Avenue Sergent Vrithoff', '2', 5000, 'NAMUR', 11, 'pseudo1', null),
	/*2er event Exposition*/
	('Namineral 2018', 'Exposition et bourse de minéraux et fossiles. Samedi et dimanche de 10h à 18h Prix: 3€ 0478 74 30 53 ghi.ruelle@skynet.be', 'Avenue Sergent Vrithoff',
	'2', 5000, 'NAMUR', 11, 'pseudo1', null),
	/*3er event Exposition*/
	('Meet Fred', 'Spectacle pour jeunes dès 14 ans à 19h  « Meet Fred » est l’histoire d’une marionnette en tissu, de deux pieds de haut, qui lutte quotidiennement contre les préjugés. Fred souhaite devenir un garçon normal, appartenir au monde réel, avoir un travail, une petite amie… Jusqu’au jour où il perd son allocation de vie de marionnette. Comment va-t-il payer ses marionnettistes? Fred perd le contrôle de sa vie. Alors qu’ils s’apprêtaient à créer “un spectacle de marionnettes fantaisiste et léger”, le Hijinx Theatre qui forme des personnes handicapées aux pratiques artistiques et la compagnie de marionnettes Blind Summit ont été rattrapés par la réalité. Pendant que les artistes entamaient le processus de création de Meet Fred, le gouvernement conservateur britannique révisait le système d’aide aux personnes handicapées. Inspiré par ce contexte sociétal, Meet Fred ne manque ni de dérision ni de mordant.', 'Avenue Sergent Vrithoff',
	'2', 5000, 'NAMUR', 11, 'pseudo1', null);

 GO

 INSERT INTO [dbo].[Presentation] (DateHeureDebut, DateHeureFin, EvenementId)
 VALUES
 ('20181122 00:00:00 AM', '20181125 01:00:00 PM', 1),
 ('20181122 06:50:00 PM', '20181125 07:30:00 PM', 1),
 ('20181123 00:00:00 AM', '20181125 00:00:00 AM', 2),
 ('20181123 11:00:00 AM', '20181125 00:00:00 AM', 3);

 GO

INSERT INTO [dbo].[Participation] (ParticipantId, EvenementId)
 VALUES
 ('pseudo2', 1);

 GO

 INSERT INTO [dbo].[Participation] (ParticipantId, EvenementId)
 VALUES
 ('pseudo1', 1);

 GO

 INSERT INTO [dbo].[Participation] (ParticipantId, EvenementId)
 VALUES
 ('pseudo3', 1);

 GO

 INSERT INTO [dbo].[NombreJaime] (UtilisateurId, EvenementId)
 VALUES
 ('pseudo2', 2);

 GO

 INSERT INTO [dbo].[NombreJaime] (UtilisateurId, EvenementId)
 VALUES
 ('pseudo3', 2);

 GO

 INSERT INTO [dbo].[NombreJaime] (UtilisateurId, EvenementId)
 VALUES
 ('pseudo2', 1);

 GO

 INSERT INTO [dbo].[Commentaire] (Texte, AuteurId, EvenementId)
 VALUES
 ('bon evenement !','pseudo2', 1);

 GO

 INSERT INTO [dbo].[Commentaire] (Texte, AuteurId, EvenementId)
 VALUES
 ('evenement nul','pseudo2', 1);

 GO

 INSERT INTO [dbo].[Commentaire] (Texte, AuteurId, EvenementId)
 VALUES
 ('pas mal','pseudo2', 2);

 GO

 INSERT INTO [dbo].[Commentaire] (Texte, AuteurId, EvenementId)
 VALUES
 ('bon evenement !','pseudo2', 1);

 GO

 INSERT INTO [dbo].[Commentaire] (Texte, AuteurId, EvenementId)
 VALUES
 ('evenement nul','pseudo2', 1);

 GO

 INSERT INTO [dbo].[Commentaire] (Texte, AuteurId, EvenementId)
 VALUES
 ('pas mal','pseudo2', 2);

 GO

 INSERT INTO [dbo].[Commentaire] (Texte, AuteurId, EvenementId)
 VALUES
 ('bon evenement !','pseudo2', 1);

 GO

 INSERT INTO [dbo].[Commentaire] (Texte, AuteurId, EvenementId)
 VALUES
 ('evenement nul','pseudo2', 1);

 GO

 INSERT INTO [dbo].[Commentaire] (Texte, AuteurId, EvenementId)
 VALUES
 ('pas mal','pseudo2', 2);

 GO




