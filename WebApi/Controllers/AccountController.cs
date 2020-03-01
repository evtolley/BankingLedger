using Domain.Accounts;
using Domain;
using Domain.ExtensionMethods;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
        public async Task<ActionResult<CreateAccountResultDto>> CreateAccount([FromBody] CreateAccountDto accountDto)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(new ErrorResult("Please enter a valid email and password."));
                }

                var result = await _accountService.CreateAccountAsync(accountDto);

                if(result.ResultType == AccountCreationResultTypeEnum.Success)
                {
                    return Ok(result);
                }

                return BadRequest(new ErrorResult(result.ResultType.GetDescription()));
            }
            catch
            {
                return BadRequest(new ErrorResult("Oops, something went wrong! Please try again"));
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<LoginResultDto>> Login([FromBody] LoginAttemptDto loginInfo)
        {
            try
            {
                var result = await _accountService.LoginAsync(loginInfo);

                if(result.ResultType == LoginResultTypeEnum.Success)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(new ErrorResult(result.ResultType.GetDescription()));
                }  
            }
            catch
            {
                return BadRequest(new ErrorResult("Oops, something went wrong! Please try again"));
            }
        }
    }
}
