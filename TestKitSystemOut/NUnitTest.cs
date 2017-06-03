using System;
using Akka.Actor;
using Akka.TestKit.NUnit3;
using NUnit.Framework;

namespace TestKitSystemOut.Nunit3
{
    [TestFixture]
    public class NUnitTest : TestKit
    {
        private static Props ActorProps => Props.Create(() => new SystemOutActor());

        [Test]
        public void WriteToConsoleWithActorOf()
        {
            Console.Out.WriteLine("This will show up");
            var actor = ActorOf(ActorProps);
            Console.Out.WriteLine("So will this");
            actor.Tell("Console writes from the actor however will not be output");
            actor.Tell("This one won't either");
            Console.Out.WriteLine("But this line will show up");
        }

        [Test]
        public void WriteToConsoleWithActorOfAsTestActorRef()
        {
            var actor = ActorOfAsTestActorRef<SystemOutActor>(ActorProps);

            actor.Tell("Hello World!");
            actor.Tell("These WILL be output to Standard Out like it should be");
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
