namespace FliGen.Services.Players.IntegrationTests.Fixtures
{
    public class MockedData
    {
        public string PlayerExternalIdForDelete { get; set; }
        public string PlayerExternalIdForUpdate { get; set; }
        public int PlayerInternalIdForDelete { get; set; }
        public int PlayerInternalIdForUpdate { get; set; }

        public string ExistingPlayer { get; set; }
        public int ExistingPlayerInternalId { get; set; }
        public string NotExistingPlayer { get; set; }
    }
}