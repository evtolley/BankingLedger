using System;
using System.Collections.Generic;
using System.Security.Claims;
using BusinessLogic.Transactions;
using Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class LedgerTransactionController : ControllerBase
    {
        private readonly ILedgerTransactionService _transactionService;
        public LedgerTransactionController(ILedgerTransactionService transactionService)
        {
            this._transactionService = transactionService;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<LedgerTransactionDto>> GetAll()
        {
            int accountId = GetCurrentUserAccountId();
            return Ok(_transactionService.GetLedgerTransactions());
        }

        // POST api/values
        [HttpPost]
        public ActionResult Post([FromBody] LedgerTransactionDto ledgerTransactionDto)
        {
            try
            {
                _transactionService.AddLedgerTransaction(ledgerTransactionDto);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        // quick and dirty method for extracting the account id from the ClaimsIdentity
        private int GetCurrentUserAccountId()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            return Convert.ToInt32(identity.FindFirst("AccountId").Value);           
        }
    }
}
