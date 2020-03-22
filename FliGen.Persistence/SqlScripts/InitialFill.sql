IF NOT EXISTS (SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = '20200309100228_Initial')
BEGIN
	INSERT INTO [LeagueType](Name)
	VALUES
	('None'),
	('Football'),
	('Hockey')

	INSERT INTO [LeaguePlayerRole](Name)
	VALUES
	('User'),
	('Admin')

	INSERT INTO [League](Name, Description, LeagueTypeId)
	SELECT 'FLI', 'Best League in the world!', Id
	FROM [LeagueType]
	WHERE [LeagueType].[Name] = 'Football'
	
	INSERT INTO [League](Name, Description, LeagueTypeId)
	SELECT 'FLIHockey', 'Worst League in the world!', Id
	FROM [LeagueType]
	WHERE [LeagueType].[Name] = 'Hockey'

    INSERT INTO [LeagueSettings](LeagueId, Visibility, RequireConfirmation)
	SELECT Id, 1, 0
	FROM [League]
	WHERE [League].[Name] = 'FLI'

	INSERT INTO [LeagueSettings](LeagueId, Visibility, RequireConfirmation)
	SELECT Id, 1, 1
	FROM [League]
	WHERE [League].[Name] = 'FLIHockey'

	INSERT INTO [LeagueSeasonLinks](LeagueId, SeasonId)
	SELECT a.Id, b.Id
	FROM [League] as a, [Season] as b WHERE a.Name='FLI' AND b.Start = '2020-01-01' AND b.Finish = '2020-12-31'

END
