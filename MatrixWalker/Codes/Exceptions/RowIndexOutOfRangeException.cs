using MatrixWalker.Codes.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixWalker.Codes.Exceptions
{
    public class RowIndexOutOfRangeException : Exception
    {
        public RowIndexOutOfRangeException()
            : base(ExceptionMessage.RowIndexOutOfBoundsException)
        {

        }

        public RowIndexOutOfRangeException(string message)
            : base(message)
        {

        }
    }
}
