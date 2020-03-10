using System;
using SharedKernel;

namespace LedgerDomain
{
    public class Ledger : AggregateRoot
    {
        public int TestMethod()
        {
            return 42;
        }
    }
}
