using API.Data;
using API.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[Authorize]
public class UserController : ApiController
{
    private readonly AppUserData _appUser;
    public UserController(AppUserData appUser)  
    {
        _appUser = appUser;
    }  
     
    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUser()
    {
        var users = await _appUser.Users.ToListAsync();

        return users;

    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AppUser>>GetUserById(int id)
    {
        return await _appUser.Users.FindAsync(id);
    }    

}
