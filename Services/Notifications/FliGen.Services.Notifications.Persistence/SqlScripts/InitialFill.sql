IF NOT EXISTS (SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = '20200309100228_Initial')
BEGIN
	INSERT INTO [NotificationType](Name)
	VALUES
	('TourRegistrationOpened'),
	('TourRegistrationClosed')

END
