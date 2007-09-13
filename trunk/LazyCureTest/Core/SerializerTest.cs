using System;
using System.Collections.Generic;
using NUnit.Framework;
using NMock2;
using System.Xml;
using LifeIdea.LazyCure.Interfaces;

namespace LifeIdea.LazyCure.Core
{
    [TestFixture]
    public class SerializerTest : Mockery
    {
        private IActivity activity;
        private ITimeLog mockTimeLog;
        [SetUp]
        public void SetUp()
        {
            activity = NewMock<IActivity>();
            mockTimeLog = NewMock<ITimeLog>();
            Stub.On(activity).GetProperty("Duration").Will(Return.Value(TimeSpan.Parse("1:23:45")));
            Stub.On(activity).GetProperty("StartTime").Will(Return.Value(DateTime.Parse("5:00:00")));
        }
        [TearDown]
        public void TearDown()
        {
            this.VerifyAllExpectationsHaveBeenMet();
        }
        [Test]
        public void SerializeActivitySpecialSymbols()
        {
            string scarySymbols = "&><";
            Stub.On(activity).GetProperty("Name").Will(Return.Value(scarySymbols));

            XmlNode xml = Serializer.ActivityToXml(activity);

            Assert.AreEqual(scarySymbols, xml["Records"]["Activity"].InnerText);
        }
        [Test]
        public void SerializeActivity()
        {
            Stub.On(activity).GetProperty("Name").Will(Return.Value("reading"));

            XmlNode xml = Serializer.ActivityToXml(activity);

            Assert.AreEqual("reading", xml["Records"]["Activity"].InnerText);
            Assert.AreEqual("5:00:00", xml["Records"]["Start"].InnerText);
            Assert.AreEqual("1:23:45", xml["Records"]["Duration"].InnerText);
        }
        // not finished
        public void XmlToTimeLog()
        {
            XmlDocument xml = new XmlDocument();
            ITimeLog timeLog = Serializer.XmlToTimeLog(xml);
        }
        [Test]
        public void TimeLogToXml()
        {
            using (Ordered)
            {
                Expect.Once.On(activity).GetProperty("Name").Will(Return.Value("first"));
                Expect.Once.On(activity).GetProperty("Name").Will(Return.Value("second"));
                Expect.Once.On(activity).GetProperty("Name").Will(Return.Value("third"));
            }
            List<IActivity> activities = new List<IActivity>();
            activities.Add(activity);
            activities.Add(activity);
            activities.Add(activity);
            Stub.On(mockTimeLog).GetProperty("Activities").Will(Return.Value(activities));

            XmlNode xml = Serializer.TimeLogToXml(mockTimeLog)["LazyCureData"];

            Assert.AreEqual("first", xml.ChildNodes[0]["Activity"].InnerText);
            Assert.AreEqual("second", xml.ChildNodes[1]["Activity"].InnerText);
            Assert.AreEqual("third", xml.ChildNodes[2]["Activity"].InnerText);
        }
        [Test]
        public void TimeLogXmlHeader()
        {
            Stub.On(mockTimeLog).GetProperty("Activities").Will(Return.Value(new List<IActivity>()));

            XmlDocument xml = (XmlDocument)Serializer.TimeLogToXml(mockTimeLog);

            Assert.AreEqual("<?xml version=\"1.0\" standalone=\"yes\"?>", xml.FirstChild.OuterXml);
        }
        [Test]
        public void SpecifyLazyCureVersionInTimeLog()
        {
            List<IActivity> activities = new List<IActivity>();
            Stub.On(mockTimeLog).GetProperty("Activities").Will(Return.Value(activities));
            XmlNode data = Serializer.TimeLogToXml(mockTimeLog)["LazyCureData"];
            Assert.AreEqual("3.1", data.Attributes["LazyCureVersion"].Value);
        }
    }
}
