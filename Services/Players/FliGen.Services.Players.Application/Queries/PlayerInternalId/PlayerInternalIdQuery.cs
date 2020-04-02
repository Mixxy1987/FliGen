using FliGen.Services.Players.Application.Dto;
using MediatR;

namespace FliGen.Services.Players.Application.Queries.PlayerInternalId
{
    public class PlayerInternalIdQuery : IRequest<PlayerInternalIdDto>
    {
        public string ExternalId { get; set; }

        public PlayerInternalIdQuery(string externalId)
        {
            ExternalId = externalId;
        }
    }
}
