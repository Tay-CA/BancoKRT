using GestaoLimitesContas.App.GestaoLimites.Commands.Create;
using GestaoLimitesContas.App.GestaoLimites.Commands.Delete;
using GestaoLimitesContas.App.GestaoLimites.Commands.Transfer;
using GestaoLimitesContas.App.GestaoLimites.Commands.Update;
using GestaoLimitesContas.App.GestaoLimites.Queries.Get;
using GestaoLimitesContas.App.GestaoLimites.Queries.List;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GestaoLimitesContas.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GestaoLimiteController : ControllerBase
    {
        private readonly IMediator mediator;

        public GestaoLimiteController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("Criar")]
        public async Task<IActionResult> Create([FromBody] CreateGestaoLimiteCommand command)
        {
            return Ok(await mediator.Send(command));
        }


        [HttpGet("Obter")]
        public async Task<IActionResult> Get([FromQuery] GetGestaoLimiteQuery get)
        {
            return Ok(await mediator.Send(get));
        }
        
        [HttpGet("Listar")]
        public async Task<IActionResult> List([FromQuery] ListGestaoLimiteQuery list)
        {
            return Ok(await mediator.Send(list));
        }

        [HttpPut("Alterar/limite")]
        public async Task<IActionResult> Update([FromBody] UpdateGestaoLimiteCommand command)
        {
            return Ok(await mediator.Send(command));
        }

        [HttpDelete("Excluir/conta")]
        public async Task<IActionResult> Delete([FromQuery] DeleteGestaoLimiteCommand command)
        {
            return Ok(await mediator.Send(command));
        }

        [HttpPost("Transferir/saldo")]
        public async Task<IActionResult> Transfer([FromBody] TransferGestaoLimiteCommand command)
        {
            return Ok(await mediator.Send(command));
        }
    }
}
