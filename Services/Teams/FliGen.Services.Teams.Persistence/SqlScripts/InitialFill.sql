IF NOT EXISTS (SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = '20200309100228_Initial')
BEGIN
	INSERT INTO [TeamRole](Name)
	VALUES
	('Home'),
	('Guest')
END
