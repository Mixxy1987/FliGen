IF NOT EXISTS (SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = '20200224070824_InitialCreate')
BEGIN
	INSERT INTO [LeagueType](Name)
	VALUES
	('None'),
	('Football'),
	('Hockey')

	INSERT INTO [League](Name, Description, LeagueTypeId)
	SELECT 'FLI', 'Best League in the world!', Id
	FROM [LeagueType]
	WHERE [LeagueType].[Name] = 'Football'
	
	INSERT INTO [League](Name, Description, LeagueTypeId)
	SELECT 'FLIHockey', 'Worst League in the world!', Id
	FROM [LeagueType]
	WHERE [LeagueType].[Name] = 'Hockey'

	INSERT INTO [Season](Start, Finish, LeagueId)
	SELECT '2020-01-01','2020-12-31', Id
	FROM [League]
	WHERE [League].[Name] = 'FLI'

	INSERT INTO [LeagueSeasonLinks](LeagueId, SeasonId)
	SELECT a.Id, b.Id
	FROM [League] as a, [Season] as b WHERE a.Name='FLI' AND b.Start = '2020-01-01' AND b.Finish = '2020-12-31'

END
