using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Xml;
using LifeIdea.LazyCure.Interfaces;
using NMock2;
using NUnit.Framework;

namespace LifeIdea.LazyCure.Core.Time
{
    [TestFixture]
    public class TimeLogSerializerTest: Mockery
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
            Stub.On(mockTimeLog).GetProperty("Date").Will(Return.Value(DateTime.Parse("2007-02-23")));
        }
        [TearDown]
        public void TearDown()
        {
            VerifyAllExpectationsHaveBeenMet();
        }
        [Test]
        public void CheckSerializedXmlHeader()
        {
            Stub.On(mockTimeLog).GetProperty("Activities").Will(Return.Value(new List<IActivity>()));
            XmlNode xml = TimeLogSerializer.Serialize(mockTimeLog);
            Assert.AreEqual("<?xml version=\"1.0\" standalone=\"yes\"?>", xml.FirstChild.OuterXml);
        }
        [Test]
        public void DeserializeCreatesTimeLog()
        {
            XmlDocument xml = new XmlDocument();
            ITimeLog timeLog = TimeLogSerializer.Deserialize(xml);
            Assert.IsNotNull(timeLog);
        }
        [Test]
        public void DeserializeFromTextReader()
        {
            StringReader reader = new StringReader("<LazyCureData />");
            ITimeLog timeLog = TimeLogSerializer.Deserialize(reader);
            Assert.IsNotNull(timeLog);
        }
        [Test]
        public void DeserializeSpecifiedTimeLog()
        {
            string sContent = "<?xml version=\"1.0\" standalone=\"yes\"?><LazyCureData><Records>" +
                              "<Activity>changed</Activity><Begin>14:35:02</Begin><Duration>0:00:07</Duration>" +
                              "</Records></LazyCureData>";
            ITimeLog timeLog = TimeLogSerializer.Deserialize(new StringReader(sContent));
            DataRow row = timeLog.Data.Rows[0];
            Assert.AreEqual("changed", row["Activity"], "activity name match");
            Assert.AreEqual(DateTime.Parse("14:35:02"), row["Start"], "start match");
            Assert.AreEqual(TimeSpan.Parse("0:00:07"), row["Duration"], "duration match");
        }
        [Test]
        public void DeserializeTimeLogWithOneActivity()
        {
            XmlDocument xml = new XmlDocument();
            xml.InnerXml = "<LazyCureData>" +
                           "<Records>" +
                           "<Activity>first</Activity>" +
                           "</Records>" +
                           "</LazyCureData>";
            ITimeLog timeLog = TimeLogSerializer.Deserialize(xml);
            Assert.AreEqual(1, timeLog.Activities.Count, "number of activities in TimeLog");
            Assert.AreEqual("first", timeLog.Activities[0].Name);
        }
        [Test]
        public void DeserializeOldFormatTimeLog()
        {
            XmlDocument xml = new XmlDocument();
            xml.InnerXml = "<LazyCureData>" +
                           "<Records>" +
                           "<Activity>exercises</Activity>" +
                           "<Begin>PT7H1M</Begin>" +
                           "<Duration>PT9M38S</Duration>" +
                           "</Records>" +
                           "<ActivitiesSummary>" +
                           "<Activity>exercises</Activity>" +
                           "<SpentTime>PT9M38S</SpentTime>" +
                           "</ActivitiesSummary>" +
                           "</LazyCureData>";
            ITimeLog timeLog = TimeLogSerializer.Deserialize(xml);
            Assert.AreEqual(1, timeLog.Activities.Count, "number of activities in TimeLog");
        }
        [Test]
        public void SerializeActivities()
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

            string content = TimeLogSerializer.Serialize(mockTimeLog).InnerXml;
            
            Assert.IsTrue(content.Contains("first"));
            Assert.IsTrue(content.Contains("second"));
            Assert.IsTrue(content.Contains("third"));
        }
        [Test]
        public void SerializeAndDeserialize()
        {
            ITimeLog timeLog1 = new TimeLog(DateTime.Now.Date);
            ITimeLog timeLog2 = TimeLogSerializer.Deserialize(TimeLogSerializer.Serialize(timeLog1));
            Assert.AreEqual(timeLog2, timeLog1);
        }
        [Test]
        public void TimeLogDateIsSerialized()
        {
            Stub.On(mockTimeLog).GetProperty("Activities").Will(Return.Value(new List<IActivity>()));
            string content = TimeLogSerializer.Serialize(mockTimeLog).InnerXml;
            Assert.IsTrue(content.Contains("2007-02-23"));
        }
        [Test]
        public void DateIsDeserialized()
        {
            XmlDocument xml = new XmlDocument();
            xml.InnerXml = "<LazyCureData Date=\"2013-10-14\">" +
                           "<Records>" +
                           "<Activity>first</Activity>" +
                           "</Records>" +
                           "</LazyCureData>";
            ITimeLog timeLog = TimeLogSerializer.Deserialize(xml);
            Assert.AreEqual("2013-10-14",timeLog.Date.ToString("yyyy-MM-dd"));
        }
        [Test]
        public void VersionIsSerialized()
        {
            Stub.On(mockTimeLog).GetProperty("Activities").Will(Return.Value(new List<IActivity>()));
            XmlNode data = TimeLogSerializer.Serialize(mockTimeLog)["LazyCureData"];
            Assert.AreEqual("3.1", data.Attributes["LazyCureVersion"].Value);
        }
    }
}
