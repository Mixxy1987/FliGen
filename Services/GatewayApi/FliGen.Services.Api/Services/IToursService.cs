using FliGen.Services.Api.Models.Tours;
using RestEase;
using System;
using System.Threading.Tasks;


namespace FliGen.Services.Api.Services
{
    [SerializationMethods(Query = QuerySerializationMethod.Serialized)]
    public interface IToursService
    {
        [AllowAnyStatusCode]
        [Get("orders/{id}")]
        Task<Tour> GetAsync([Path] int id);
    }
}