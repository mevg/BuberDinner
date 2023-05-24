using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers;

[Route("api/[controller]")]
public class DinnersController : BaseApiController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    [HttpGet]
    public IActionResult ListDinners()
    {
        return Ok();
    }
}