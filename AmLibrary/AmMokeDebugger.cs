using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AMClasses;

namespace AmLibrary
{
    class AmMokeDebugger: AmDebugger
    {
        public override void Stop()
        {
        }

        public override void Start(string ip = "127.0.0.1", ushort port = 8888)
        {
        }

        public override MessageForDebug RecieveMessage()
        {
            return new MessageForDebug();
        }

        public override void SendMessage(MessageForDebug message)
        {
        }
    }
}
