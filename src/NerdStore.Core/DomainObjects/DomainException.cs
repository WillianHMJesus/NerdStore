using System;

namespace NerdStore.Core.DomainObjects
{
    public class DomainException : Exception
    {
        public DomainException()
            : base()
        {
        }

        public DomainException(string message)
            : base(message)
        {
        }

        public DomainException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public string GetErrorMessage()
        {
            var errorMessage = string.IsNullOrEmpty(Message) ?
                InnerException?.Message :
                Message;

            return errorMessage;
        }
    }
}
