using System;
namespace MaximumBrainfuck
{
    [Serializable]
    class MethodUndefinedException : Exception 
    {
        public MethodUndefinedException ()
        {}

        public MethodUndefinedException (string message) 
            : base(message)
        {}

        public MethodUndefinedException (string message, Exception innerException)
            : base (message, innerException)
        {}        
    }
}