using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace AMClasses
{
    [Serializable()]
    public class AMException: ApplicationException
    {
        public AMException(string message)
            : base(message) { }

        public AMException()
            : base() { }

        public AMException(string message, Exception innerException) : base(message, innerException) { }

        protected AMException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
