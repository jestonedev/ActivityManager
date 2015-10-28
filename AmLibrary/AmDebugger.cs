using System;
using System.Net.Sockets;
using System.Text;
using AMClasses;
using Newtonsoft.Json;

namespace AmLibrary
{
    class AmDebugger: IDisposable
    {
        private TcpClient Client { get; set; }

        public virtual void SendMessage(MessageForDebug message)
        {
            if (Client == null || !Client.Connected) return;
            var array = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
            Client.GetStream().Write(array, 0, array.Length);
        }

        public virtual MessageForDebug RecieveMessage()
        {
            if (Client == null || !Client.Connected) return null;
            var buffer = new byte[Client.ReceiveBufferSize];
            var bytes = Client.GetStream().Read(buffer, 0, buffer.Length);
            var str = Encoding.UTF8.GetString(buffer, 0, bytes);
            var response = JsonConvert.DeserializeObject<MessageForDebug>(str);
            return response;
        }

        public virtual void Start(string ip = "127.0.0.1", ushort port = 8888)
        {
            Client = new TcpClient(ip, port);
        }

        public virtual void Stop()
        {
            if (Client == null || !Client.Connected) return;
            SendMessage(new MessageForDebug {{"debug", "done"}});
            Client.Close();
        }

        public void Dispose()
        {
            Stop();
        }
    }
}
