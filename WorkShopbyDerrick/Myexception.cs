using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkShopbyDerrick
{
    public class InsufficientBalanceException : ApplicationException
    {
        public InsufficientBalanceException() : base()
        {
        }
        public InsufficientBalanceException(string message) : base(message)
        {
        }
        public InsufficientBalanceException(string message, Exception innerExc)
        : base(message, innerExc)
        {
        }
    }
}
