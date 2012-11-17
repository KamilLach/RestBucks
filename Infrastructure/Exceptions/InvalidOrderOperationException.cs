using System;

namespace Infrastructure.Exceptions
{
    public class InvalidOrderOperationException : Exception
    {
        public InvalidOrderOperationException(string a_message)
            : base(a_message)
        {}
    }
}