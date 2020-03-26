﻿using System;
using Moq;
using NUnit.Framework;
using Xharness.Listeners;
using Xharness.Logging;

namespace Xharness.Tests.Listeners.Tests {

	[TestFixture]
	public class SimpleListenerFactoryTest {

		Mock<ILog> log;
		Mock<IMetro> metro;
		SimpleListenerFactory factory;
		string deviceName = "My IPhone";

		[SetUp]
		public void SetUp ()
		{
			log = new Mock<ILog> ();
			metro = new Mock<IMetro> ();
			factory = new SimpleListenerFactory (metro.Object);
		}

		[TearDown]
		public void TearDown ()
		{
			log = null;
			factory = null;
		}

		[Test]
		public void CreateNotWatchListener ()
		{
			var (transport, listener, listenerTmpFile) = factory.Create (deviceName, RunMode.iOS, log.Object, log.Object, true, true, true, false);
			Assert.AreEqual (ListenerTransport.Tcp, transport, "transport");
			Assert.IsInstanceOf (typeof (SimpleTcpListener), listener, "listener");
			Assert.IsNull (listenerTmpFile, "tmp file");
		}

		[Test]
		public void CreateWatchOSSimulator ()
		{
			var logFullPath = "myfullpath.txt";
			_ = log.Setup (l => l.FullPath).Returns (logFullPath);

			var (transport, listener, listenerTmpFile) = factory.Create (deviceName, RunMode.WatchOS, log.Object, log.Object, true, true, true, false);
			Assert.AreEqual (ListenerTransport.File, transport, "transport");
			Assert.IsInstanceOf (typeof (SimpleFileListener), listener, "listener");
			Assert.IsNotNull (listenerTmpFile, "tmp file");
			Assert.AreEqual (logFullPath + ".tmp", listenerTmpFile);

			log.Verify (l => l.FullPath, Times.Once);

		}

		[Test]
		public void CreateWatchOSDevice ()
		{
			var (transport, listener, listenerTmpFile) = factory.Create (deviceName, RunMode.WatchOS, log.Object, log.Object, false, true, true, false);
			Assert.AreEqual (ListenerTransport.Http, transport, "transport");
			Assert.IsInstanceOf (typeof (SimpleHttpListener), listener, "listener");
			Assert.IsNull (listenerTmpFile, "tmp file");
		}
	}
}
