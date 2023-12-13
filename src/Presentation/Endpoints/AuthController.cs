namespace CleanMinimalApi.Presentation.Endpoints;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

public class AuthController(IConfiguration configuration) : ControllerBase
{


    [HttpPost, Route("login")]
    public IActionResult Login(LoginModel model)
    {
        if (model == null)
        {
            return BadRequest("Invalid client request");
        }
        if (model.UserName == "johndoe" )
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("RfghZy7S8+a0VzzZJ1xSEvzMOJ1g4fAWf+IW0KA49XQJU0LuIMXKIo3+pl2qXDPu"));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokenOptions = new JwtSecurityToken(
                issuer: configuration.GetValue<string>("Authentication:Schemes:Bearer:ValidIssuer"),
                /* "Authentication:Schemes:Bearer:ValidAudiences:0"*/
                audience:  configuration.GetValue<string>("Authentication:Schemes:Bearer:ValidAudiences:0"), //"https://localhost:7032", ,
                claims: new List<Claim>(),
                expires: DateTime.Now.AddHours(2),
                signingCredentials: signinCredentials
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return Ok(new { Token = tokenString });
        }
        else
        {
            return Unauthorized();
        }
    }
}

public record LoginModel(string UserName)
{

}
