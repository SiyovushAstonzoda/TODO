using Microsoft.AspNetCore.Mvc;
using Infrastructure;
using Domain.Models;
using Domain.Wrapper;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TODOController : ControllerBase
{
    private TODOService _todoService;
    public TODOController()
    {
        _todoService = new TODOService();
    }

    [HttpGet("GetAllDOTOs")]
    public Responce<List<TODO>> GetAllTODOs()
    {
        return _todoService.GetAllTODOs();
    }

    [HttpPost("AddTask")]
     public Responce<TODO> AddTODO(TODO todo)
    {
        return _todoService.AddTODO(todo);
    }

    [HttpPut("Edit")]
    public Responce<TODO> UpdateTODO(TODO todo)
    {
        return _todoService.UpdateTODO(todo);
    }

    [HttpDelete("Remove")]
    public Responce<string> DeleteTODO(int id)
    {
        return _todoService.DeleteTODO(id);
    }
}
