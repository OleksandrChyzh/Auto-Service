using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Validation
{
    public class AutoServiceException : Exception
    {

        public AutoServiceException(string message) : base(message)
        {
        }
        public AutoServiceException(string message, Exception innerExeption) : base(message, innerExeption)
        {

        }
    }
}
