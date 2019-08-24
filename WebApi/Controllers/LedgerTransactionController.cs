using System;
using System.Collections.Generic;
using BusinessLogic.Transactions;
using Core;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
    }
}
