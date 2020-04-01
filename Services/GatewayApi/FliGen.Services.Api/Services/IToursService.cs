using FliGen.Services.Api.Models.Tours;
using FliGen.Services.Api.Queries;
using RestEase;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace FliGen.Services.Api.Services
{
    [SerializationMethods(Query = QuerySerializationMethod.Serialized)]
    public interface IToursService
    {
        [AllowAnyStatusCode]
        [Get("tours/{id}")]
        Task<Tour> GetAsync([Path] int id);

        [AllowAnyStatusCode]
        [Get("tours")]
        Task<IEnumerable<Tour>> GetAsync([Query]ToursByPlayerIdQuery toursByPlayerIdQuery);
    }
}