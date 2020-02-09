IF NOT EXISTS (SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = '20200208074745_InitialCreate')
BEGIN
	INSERT INTO [League](Name)
    VALUES('FLI')
	
	INSERT INTO [Season](Start, Finish, LeagueId)
	SELECT '01.01.2020','31.12.2020', Id
	FROM [League]
	WHERE [League].[Name] = 'FLI'

	INSERT INTO [LeagueSeasonLinks](LeagueId, SeasonId)
	SELECT a.Id, b.Id
	FROM [League] as a, [Season] as b WHERE a.Name='FLI' AND b.Start = '01.01.2020' AND b.Finish = '31.12.2020'

	INSERT INTO [Players](FirstName, LastName)
	VALUES
	('Матюнин', 'Валентин'),
	('Волчков', 'Вячеслав'),
	('Никитин', 'Евгений'),
	('Алтухов', 'Антон'),
	('Виноходов', 'Игорь'),
	('Волчков', 'Вячеслав'),
	('Галицкий', 'Вячеслав'),
	('Косенков', 'Олег'),
	('Мухин', 'Иван'),
	('Попов', 'Александр'),
	('Попов', 'Артем'),
	('Растаев', 'Дмитрий'),
	('Филинов', 'Павел'),
	('Масюк', 'Родион'),
	('Дубцов', 'Максим'),
	('Ахтямов', 'Руслан'),
	('Яшин', 'Анатолий'),
	('Зырянов', 'Егор'),
	('Сгибнев', 'Андрей'),
	('Ляшук', 'Алексей'),
	('Ларичкин', 'Алексей')
END
