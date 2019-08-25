using BusinessLogic.Accounts;
using Core.Accounts;
using Core.ExtensionMethods;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            this._accountService = accountService;
        }
        [HttpPost]
        [Route("create")]
        public ActionResult<CreateAccountResultDto> CreateAccount([FromBody] CreateAccountDto accountDto)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest("Please enter a valid email and password.");
                }

                var result = _accountService.CreateAccount(accountDto);

                if(result.ResultType == AccountCreationResultTypeEnum.Success)
                {
                    return Ok(result.LoginData);
                }

                return BadRequest(result.ResultType.GetDescription());
            }
            catch
            {
                return BadRequest("Oops, something went wrong! Please try again");
            }
        }

        [HttpPost]
        [Route("login")]
        public ActionResult<LoginResultDto> Login([FromBody] LoginAttemptDto loginInfo)
        {
            try
            {
                var result = _accountService.Login(loginInfo);

                if(result.ResultType == LoginResultTypeEnum.Success)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result.ResultType.GetDescription());
                }  
            }
            catch
            {
                return BadRequest("Oops, something went wrong! Please try again");
            }
        }
    }
}
