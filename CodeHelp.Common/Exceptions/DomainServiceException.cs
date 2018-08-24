using System;

namespace CodeHelp.Common.Exceptions
{
    public class DomainServiceException : Exception
    {
        public DomainServiceException(string message) : base(message)
        {

        }
    }
}