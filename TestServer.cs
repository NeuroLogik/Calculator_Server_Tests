using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Server.Test
{
    class TestServer
    {
        private Server server;
        private FakeTransport transport;

        [SetUp]
        public void Setup()
        {
            transport = new FakeTransport();
            server = new Server(transport);
        }

        [Test]
        public void TestInitZero()
        {
            Assert.That(server.FirstNum, Is.EqualTo(0f));
            Assert.That(server.SecondNum, Is.EqualTo(0f));
            Assert.That(server.Result, Is.EqualTo(0f));
            Assert.That(server.Response, Is.EqualTo(null));
            Assert.That(server.ReturnData, Is.EqualTo(null));
        }

        [Test]
        public void TestReturnDataSize()
        {
            Packet packet = new Packet(0, 3f, 5f);
            transport.SetFakeTransportPacket(packet);
            server.SingleStep();
            Assert.That(server.ReturnData.Length, Is.EqualTo(5));
        }

        [Test]
        public void TestFloatReceptionGreenLight()
        {
            Packet packet = new Packet(0, 3f, 5f);
            transport.SetFakeTransportPacket(packet);
            server.SingleStep();
            Assert.That(server.FirstNum, Is.EqualTo(3f));
            Assert.That(server.SecondNum, Is.EqualTo(5f));
        }
        
        [Test]
        public void TestAdditionGreenLight()
        {
            Packet packet = new Packet(0, 3f, 5f);
            transport.SetFakeTransportPacket(packet);
            server.SingleStep();
            Assert.That(server.Result, Is.EqualTo(8f));
        }

        [Test]
        public void TestAdditionRedLight()
        {
            Packet packet = new Packet(0, 3f, 5f);
            transport.SetFakeTransportPacket(packet);
            server.SingleStep();
            Assert.That(server.Result, Is.Not.EqualTo(12f));
        }

        [Test]
        public void TestSubtractionGreenLight()
        {
            Packet packet = new Packet(1, 3f, 5f);
            transport.SetFakeTransportPacket(packet);
            server.SingleStep();
            Assert.That(server.Result, Is.EqualTo(-2f));
        }

        [Test]
        public void TestSubtractionRedLight()
        {
            Packet packet = new Packet(1, 3f, 5f);
            transport.SetFakeTransportPacket(packet);
            server.SingleStep();
            Assert.That(server.Result, Is.Not.EqualTo(12f));
        }

        [Test]
        public void TestProductGreenLight()
        {
            Packet packet = new Packet(2, 3f, 5f);
            transport.SetFakeTransportPacket(packet);
            server.SingleStep();
            Assert.That(server.Result, Is.EqualTo(15f));
        }

        [Test]
        public void TestProductRedLight()
        {
            Packet packet = new Packet(2, 3f, 5f);
            transport.SetFakeTransportPacket(packet);
            server.SingleStep();
            Assert.That(server.Result, Is.Not.EqualTo(12f));
        }

        [Test]
        public void TestDivisionGreenLight()
        {
            Packet packet = new Packet(3, 21f, 7f);
            transport.SetFakeTransportPacket(packet);
            server.SingleStep();
            Assert.That(server.Result, Is.EqualTo(3f));
        }

        [Test]
        public void TestDivisionRedLight()
        {
            Packet packet = new Packet(3, 3f, 5f);
            transport.SetFakeTransportPacket(packet);
            server.SingleStep();
            Assert.That(server.Result, Is.Not.EqualTo(12f));
        }
    }
}
