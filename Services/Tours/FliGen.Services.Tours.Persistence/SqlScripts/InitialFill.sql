IF NOT EXISTS (SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = '20200309100228_Initial')
BEGIN
	INSERT INTO [TourStatus](Name)
	VALUES
	('Planned'),
	('RegistrationOpened'),
	('RegistrationClosed'),
	('InProgress'),
	('Completed'),
	('Canceled')
END
