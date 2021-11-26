using Quantum.ClassLibrary.Enum;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Quantum.ClassLibrary.CustomException
{
    [Serializable]
    internal class ValidationException : Exception
    {
        public ValidationException()
        {
        }
        public ValidationException(string message) : base(message)
        {

        }
        /// <summary>
        /// Message based on List of quantumExceptions
        /// </summary>
        /// <param name="quantumExceptions"></param>
        public ValidationException(List<QuantumException> quantumExceptions ) : base(string.Join(Environment.NewLine,  quantumExceptions.ToString()))
        {
           
        }

        public ValidationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
