using System;
using System.Xml;
using LifeIdea.LazyCure.Interfaces;
using NMock2;
using NUnit.Framework;

namespace LifeIdea.LazyCure.Core.Activities
{
    [TestFixture]
    public class ActivitySerializerTest:Mockery
    {
        private IActivity activity;
        [SetUp]
        public void SetUp()
        {
            activity = NewMock<IActivity>();
            Stub.On(activity).GetProperty("Duration").Will(Return.Value(TimeSpan.Parse("1:23:45")));
            Stub.On(activity).GetProperty("StartTime").Will(Return.Value(DateTime.Parse("5:00:00")));
        }
        [Test]
        public void SerializeActivity()
        {
            Stub.On(activity).GetProperty("Name").Will(Return.Value("reading"));

            XmlNode xml = ActivitySerializer.SerializeToXml(activity);

            Assert.AreEqual("reading", xml["Activity"].InnerText);
            Assert.AreEqual("5:00:00", xml["Start"].InnerText);
            Assert.AreEqual("1:23:45", xml["Duration"].InnerText);
        }
        [Test]
        public void DeserializeActivity()
        {
            XmlDocument doc = new XmlDocument();
            doc.InnerXml = "<Records>" +
                           "<Activity>activity</Activity>" +
                           "<Start>5:00:00</Start>" +
                           "<Duration>1:23:45</Duration>" +
                           "</Records>";

            activity = ActivitySerializer.Deserialize(doc.FirstChild);

            Assert.AreEqual("activity",activity.Name);
            Assert.AreEqual(DateTime.Parse("5:00:00"), activity.StartTime);
            Assert.AreEqual(TimeSpan.Parse("1:23:45"), activity.Duration);
        }
        [Test]
        public void SerializeAndDeserialize()
        {
            IActivity first = new Activity("activity", DateTime.Parse("5:00:00"), TimeSpan.Parse("1:23:45"));
            IActivity second = ActivitySerializer.Deserialize(ActivitySerializer.SerializeToXml(first));
            Assert.AreEqual(first, second);
        }
        [Test]
        public void SerializeSpecialSymbols()
        {
            string scarySymbols = "&><";
            Stub.On(activity).GetProperty("Name").Will(Return.Value(scarySymbols));

            XmlNode xml = ActivitySerializer.SerializeToXml(activity);

            Assert.AreEqual(scarySymbols, xml["Activity"].InnerText);
        }
        [Test]
        public void BeginSupport()
        {
            XmlDocument doc = new XmlDocument();
            doc.InnerXml = "<Records>" +
                           "<Activity>activity</Activity>" +
                           "<Begin>6:00:00</Begin>" +
                           "<Duration>2:34:50</Duration>" +
                           "</Records>";

            activity = ActivitySerializer.Deserialize(doc.FirstChild);

            Assert.AreEqual("activity", activity.Name);
            Assert.AreEqual(DateTime.Parse("6:00:00"), activity.StartTime);
            Assert.AreEqual(TimeSpan.Parse("2:34:50"), activity.Duration);
        }
        [Test]
        public void OldTimeFormat()
        {
            XmlDocument doc = new XmlDocument();
            doc.InnerXml = "<Records>" +
                           "<Activity>yoga</Activity>" +
                           "<Begin>PT7H1M</Begin>" +
                           "<Duration>PT9M38S</Duration>" +
                           "</Records>";
            
            activity = ActivitySerializer.Deserialize(doc.FirstChild);

            Assert.AreEqual("yoga", activity.Name);
            Assert.AreEqual(DateTime.Parse("7:01:00"), activity.StartTime);
            Assert.AreEqual(TimeSpan.Parse("0:09:38"), activity.Duration);
        }
        [Test]
        public void ParseOldFormatDateTime()
        {
            Assert.AreEqual(DateTime.Parse("7:00:00"), ActivitySerializer.ParseDateTime("PT7H"));
            Assert.AreEqual(DateTime.Parse("0:17:00"), ActivitySerializer.ParseDateTime("PT17M"));
            Assert.AreEqual(DateTime.Parse("0:00:25"), ActivitySerializer.ParseDateTime("PT25S"));
            Assert.AreEqual(DateTime.Parse("1:02:03"), ActivitySerializer.ParseDateTime("PT1H2M3S"));
        }
        [Test]
        public void ParseOldFormatTimeSpan()
        {
            Assert.AreEqual(TimeSpan.Parse("1:23:45"),ActivitySerializer.ParseTimeSpan("PT1H23M45S"));
        }
        [Test]
        public void ParseInvalidTimeSpan()
        {
            Assert.AreEqual(TimeSpan.Zero,ActivitySerializer.ParseTimeSpan("invalid"));
        }
        [Test]
        public void ParseInvalidDate()
        {
            Assert.AreEqual(DateTime.Now.Date,ActivitySerializer.ParseDateTime("invalid"));
        }
    }
}