using bettingRouletteAPI.Entities;
using bettingRouletteAPI.Helpers.Results;
using bettingRouletteAPI.Model;
using Microsoft.AspNetCore.Mvc;
using System;

namespace bettingRouletteAPI.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly TokensModel _tokenModel;
        public AuthenticationController(TokensModel tokenModel)
        {
            _tokenModel = tokenModel;
        }

        [HttpPost]
        public IActionResult CreateToken()
        {
            var authenticationResult = new SuccessAuthenticationResult();
            var token = new Token();
            token.StringToken = Guid.NewGuid().ToString();
            authenticationResult.Token = token.StringToken;
            _tokenModel.CreateToken(token);

            return Ok(authenticationResult);
        }

    }
}
