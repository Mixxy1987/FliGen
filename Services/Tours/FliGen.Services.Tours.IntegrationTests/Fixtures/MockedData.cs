using FliGen.Services.Tours.Domain.Entities;

namespace FliGen.Services.Tours.IntegrationTests.Fixtures
{
    public class MockedData
    {
        public int TourForCancelId { get; set; }
        public int TourForOpenId { get; set; }
        public int TourForReopenId { get; set; }
        public int TourForBackId { get; set; }
        public Tour TourForReadById { get; set; }
    }
}