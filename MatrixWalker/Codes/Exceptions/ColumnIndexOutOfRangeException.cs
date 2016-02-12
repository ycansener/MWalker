using MatrixWalker.Codes.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixWalker.Codes.Exceptions
{
    public class ColumnIndexOutOfRangeException : Exception
    {
        public ColumnIndexOutOfRangeException()
            : base(ExceptionMessage.ColumnIndexOutOfBoundsException)
        {

        }

        public ColumnIndexOutOfRangeException(string message)
            : base(message)
        {

        }
    }
}
