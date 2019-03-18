using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Test
{
    class FakeTransport : ITransport
    {
        Packet packet;

        public void Bind(string address, int port)
        {
            
        }

        public byte[] Receive()
        {
            return packet.GetData();
        }

        public bool Send(byte[] data)
        {
            return true;
        }

        public void SetFakeTransportPacket(Packet packet)
        {
            this.packet = packet;
        }
    }
}
