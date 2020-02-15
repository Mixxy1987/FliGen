IF NOT EXISTS (SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = '20200208074745_InitialCreate')
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
	
	INSERT INTO [Season](Start, Finish, LeagueId)
	SELECT '01.01.2020','31.12.2020', Id
	FROM [League]
	WHERE [League].[Name] = 'FLI'

	INSERT INTO [LeagueSeasonLinks](LeagueId, SeasonId)
	SELECT a.Id, b.Id
	FROM [League] as a, [Season] as b WHERE a.Name='FLI' AND b.Start = '01.01.2020' AND b.Finish = '31.12.2020'

END
