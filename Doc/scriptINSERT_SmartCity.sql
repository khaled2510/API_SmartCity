 
INSERT INTO dbo.Categorie(Libelle, [Image])
VALUES ('Soir�e', null),
		('Bar-Caf�', null),
		('Cin�ma', null),
		('Festival', null),
		('Concert', null),
		('Sport', null),
		('Loisir', null),
		('Foire-Salon', null),
		('Conf�rence', null),
		('Th��tre', null),
		('Exposition', null),
		('Mus�es', null);

INSERT INTO [dbo].[Utilisateur] (Pseudo, Nom, Prenom, EMail, MotDePasse, [Role])
 VALUES
 ('pseudo1', 'Armand', 'R�becca', 'reb@hotmsil.com', '123', 'Professional'),
 ('pseudo2', 'Max', 'Lenon', 'max@hotmsil.com', '456', 'Subscribe'),
 ('pseudo3', 'khaled', 'hammou', 'reb@hotmsil.com', '123', 'Admin');

 GO

INSERT INTO [dbo].[Evenement] (Nom, [Description], Rue, Numero, CodePostal, Localite, CategorieId, CreateurId, [ImageUrl])
 VALUES
 /*1er event Exposition*/
 ('RECUP �re 2018','Plus de 100 exposants pour tout voir et tout savoir sur l''�co-consommation, la r�cup'' et le r�emploi. Cette ann�e c�est la th�matique Art
  et Culture qui sera d�velopp�e avec l�organisation d�une foire aux artistes Du mat�riel sera mis � la disposition des artistes afin qu�ils cr�ent une ou plusieurs �uvres pendant 
  le salon pour le plus grand plaisir des visiteurs. Par ailleurs il y aura aussi comme nouveaut�s : des conf�rences le samedi et le dimanche, une � �re � Z�ro D�chet avec notamment 
  une partie de nos familles Z�ro D�chet qui sera pr�sente afin d��changer sur le sujet avec des visiteurs h�sitants � entamer la d�marche ou rencontrant des difficult�s dans leur parcours ZD.
   La Ville de Namur sera pr�sente au travers d�un stand de 100m2 ayant pour th�me � la R�cup� s�invite au jardin �. A cette occasion une nouvelle brochure sera �dit�e et diffus�e. www.recupere.be
    � Espace cr�ateurs : l''art de la r�cup'' et de l''�co-design � Espace de vente seconde main � Culture et r�cup'' � Z�ro d�chet &amp; �co-consommation � Ateliers r�cup'' � Repair caf� g�ant � 
	Foire aux artiste ** NOUVEAU ** � Conf�rences � Espace enfants : animations r�cup'' et espace garderie Vendredi samedi et dimanche de 10h � 18h info@recupere.be',
	'Avenue Sergent Vrithoff', '2', 5000, 'NAMUR', 11, 'pseudo1', null),
	/*2er event Exposition*/
	('Namineral 2018', 'Exposition et bourse de min�raux et fossiles. Samedi et dimanche de 10h � 18h Prix: 3� 0478 74 30 53 ghi.ruelle@skynet.be', 'Avenue Sergent Vrithoff',
	'2', 5000, 'NAMUR', 11, 'pseudo1', null),
	/*3er event Exposition*/
	('Meet Fred', 'Spectacle pour jeunes d�s 14 ans � 19h  ��Meet Fred�� est l�histoire d�une marionnette en tissu, de deux pieds de haut, qui lutte quotidiennement contre les pr�jug�s. Fred souhaite devenir un gar�on normal, appartenir au monde r�el, avoir un travail, une petite amie� Jusqu�au jour o� il perd son allocation de vie de marionnette. Comment va-t-il payer ses marionnettistes? Fred perd le contr�le de sa vie. Alors qu�ils s�appr�taient � cr�er �un spectacle de marionnettes fantaisiste et l�ger�, le Hijinx Theatre qui forme des personnes handicap�es aux pratiques artistiques et la compagnie de marionnettes Blind Summit ont �t� rattrap�s par la r�alit�. Pendant que les artistes entamaient le processus de cr�ation de Meet Fred, le gouvernement conservateur britannique r�visait le syst�me d�aide aux personnes handicap�es. Inspir� par ce contexte soci�tal, Meet Fred ne manque ni de d�rision ni de mordant.', 'Avenue Sergent Vrithoff',
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




