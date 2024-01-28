using System.Security.Cryptography;
using System.Text;
using System.Text.Unicode;
using API.Data;
using API.DTO;
using API.Entity;
using API.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API;

public class LoginController : ApiController
{
    private readonly AppUserData _appUser;
    private readonly ITokenService _tokenService;
    public LoginController(AppUserData appUser, ITokenService tokenService)  
    {
        _appUser = appUser;
    }  

    [HttpPost("login")] // POST METHOD api/api/login?username=nishant&password=ketu
    public async Task<ActionResult<UserDTO>> Login ([FromBody] LoginDTO loginDTO)
    {
        if (await UserExist(loginDTO.Name))
        return Ok("user login");

        using var hmac = new HMACSHA512();

        var user = new AppUser
        {
            Name = loginDTO.Name.ToLower(),
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password)),
            PasswordSalt = hmac.Key
        };
        _appUser.Users.Add(user);
        await _appUser.SaveChangesAsync();

        return new UserDTO
        {
            Name = user.Name,
            Token = _tokenService.CreateToken(user)
        };

    }

    [HttpPost("userLogin")]

    public async Task<ActionResult<UserDTO>> UserLogin(UserLoginDTO userLoginDTO)
    {
        var user = await _appUser.Users.SingleOrDefaultAsync(x =>
        x.Name == userLoginDTO.Name);

        if(user==null)
        return Unauthorized("user not found");      

        using var hmac = new HMACSHA512(user.PasswordSalt);

        var ComputedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userLoginDTO.Password));

        for(int i=0; i<ComputedHash.Length;i++)
        {
            if(ComputedHash[i] != user.PasswordHash[i])
            return Unauthorized("Password is wrong");
        } 

        return new UserDTO
        {
            Name = user.Name,
            Token = _tokenService.CreateToken(user)
        };

    }

    public async Task<bool> UserExist (string Name)
    {
        return await _appUser.Users.AnyAsync(x => x.Name == Name.ToLower());
    }

}
