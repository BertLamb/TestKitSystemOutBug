using System;
using Akka.Actor;
using Akka.TestKit.VsTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestKitSystemOut.VsTest
{
    [TestClass]
    public class VsTest : TestKit
    {
        private static Props ActorProps => Props.Create(() => new SystemOutActor());

        [TestMethod]
        public void WriteToConsoleWithActorOf()
        {
            var actor = ActorOf(ActorProps);

            actor.Tell("Hello World!");
            actor.Tell("None of this will be output to Standard Out like it should be");

        }

        [TestMethod]
        public void WriteToConsoleWithActorOfAsTestActorRef()
        {
            var actor = ActorOfAsTestActorRef<SystemOutActor>(ActorProps);

            actor.Tell("Hello World!");
            actor.Tell("But this WILL be output to Standard Out like it should be");
        }
    }

    public class SystemOutActor : ReceiveActor
    {
        public SystemOutActor()
        {
            Receive<string>(str => Console.Out.WriteLine(str));
        }
    }
}
