using System;
using System.Threading.Tasks;
using FliGen.Services.Operations.Dto;
using FliGen.Services.Operations.Services;
using Microsoft.AspNetCore.Mvc;

namespace FliGen.Services.Operations.Controllers
{
    [Route("[controller]")]
    public class OperationsController : BaseController
    {
        private readonly IOperationsStorage _operationsStorage;

        public OperationsController(IOperationsStorage operationsStorage)
        {
            _operationsStorage = operationsStorage;
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<OperationDto>> Get(Guid id)
            => Single(await _operationsStorage.GetAsync(id));
    }
}