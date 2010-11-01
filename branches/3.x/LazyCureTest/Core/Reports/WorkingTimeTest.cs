using System;
using System.Collections.Generic;
using System.Data;
using LifeIdea.LazyCure.Core.Activities;
using LifeIdea.LazyCure.Core.Tasks;
using LifeIdea.LazyCure.Core.Time;
using LifeIdea.LazyCure.Interfaces;
using NMock2;
using NUnit.Framework;
using LifeIdea.LazyCure.Core.IO;
using System.IO;

namespace LifeIdea.LazyCure.Core.Reports
{
    [TestFixture]
    public class WorkingTimeTest: Mockery
    {
        private WorkingTime workingTime;
        private ITaskCollection taskCollection;
        [SetUp]
        public void SetUp()
        {
            ITimeLog timeLog = NewMock<ITimeLog>();
            Stub.On(timeLog).GetProperty("Activities").Will(Return.Value(new List<IActivity>(
                new IActivity[] {
                    new Activity("work", DateTime.Parse("9:00"), TimeSpan.Parse("4:00")),
                    new Activity("rest", DateTime.Parse("14:30"), TimeSpan.Parse("3:30"))
                })));
            Stub.On(timeLog).GetProperty("Data").Will(Return.Value(new DataTable()));
            taskCollection = TaskCollection.Default;
            taskCollection.LinkActivityAndTask("work", "Work");
            taskCollection.LinkActivityAndTask("rest", "Rest");
            workingTime = new WorkingTime(timeLog, taskCollection);
        }
        [Test]
        public void DataColumns()
        {
            DataTable table = workingTime.Intervals;
            Assert.AreEqual(DateTime.MinValue.GetType(), table.Columns["Start"].DataType);
            Assert.AreEqual(DateTime.MinValue.GetType(), table.Columns["End"].DataType);
        }
        [Test]
        public void TimeOnWork()
        {
            Assert.AreEqual(TimeSpan.Parse("4:00"), workingTime.TimeOnWork);
        }
        [Test]
        public void WorkingIntervals()
        {
            ITimeLog timeLog = NewMock<ITimeLog>();
            Stub.On(timeLog).GetProperty("Activities").Will(Return.Value(new List<IActivity>(
                new IActivity[] {
                    new Activity("work1", DateTime.Parse("9:00"), TimeSpan.Parse("1:00")),
                    new Activity("dinner", DateTime.Parse("10:00"), TimeSpan.Parse("1:00")),
                    new Activity("work2", DateTime.Parse("11:00"), TimeSpan.Parse("0:30"))
                })));
            Stub.On(timeLog).GetProperty("Data").Will(Return.Value(new DataTable()));
            taskCollection = TaskCollection.Default;
            taskCollection.LinkActivityAndTask("work1", "Work");
            taskCollection.LinkActivityAndTask("dinner", "Rest");
            taskCollection.LinkActivityAndTask("work2", "Work");
            workingTime = new WorkingTime(timeLog, taskCollection);
            DataTable table = workingTime.Intervals;
            Assert.AreEqual(DateTime.Parse("9:00"), table.Rows[0]["Start"]);
            Assert.AreEqual(DateTime.Parse("10:00"), table.Rows[0]["End"]);
            Assert.AreEqual(DateTime.Parse("11:00"), table.Rows[1]["Start"]);
            Assert.AreEqual(DateTime.Parse("11:30"), table.Rows[1]["End"]);
        }
        [Test]
        public void WorkingIntervalsUpdatedWhenTimeLogIsChanged()
        {
            ITimeLog timeLog = new TimeLog(DateTime.Now.Date);
            workingTime = new WorkingTime(timeLog, taskCollection);
            timeLog.AddActivity(new Activity("work", DateTime.Parse("10:30"), TimeSpan.Parse("10:55")));
            DataTable table = workingTime.Intervals;

            Assert.AreEqual(DateTime.Parse("10:30"), table.Rows[0]["Start"]);
            Assert.AreEqual(1, table.Rows.Count, "rows count");
        }
        [Test]
        public void CalculatingAutomaticallyFalse()
        {
            ITimeLog timeLog = new TimeLog(DateTime.Now.Date);
            workingTime = new WorkingTime(timeLog, taskCollection);
            timeLog.AddActivity(new Activity("work", DateTime.Parse("9:30"), TimeSpan.Parse("0:25")));
            workingTime.CalculateAutomatically = false;
            timeLog.AddActivity(new Activity("work", DateTime.Parse("9:55"), TimeSpan.Parse("0:05")));
            DataTable table = workingTime.Intervals;

            Assert.AreEqual(DateTime.Parse("9:55"), table.Rows[0]["End"]);
        }
        [Test]
        public void CalculatingAutomaticallyTrue()
        {
            ITimeLog timeLog = new TimeLog(DateTime.Now.Date);
            workingTime = new WorkingTime(timeLog, taskCollection);
            timeLog.AddActivity(new Activity("work", DateTime.Parse("9:30"), TimeSpan.Parse("0:25")));
            workingTime.CalculateAutomatically = false;
            timeLog.AddActivity(new Activity("work", DateTime.Parse("9:55"), TimeSpan.Parse("0:05")));
            workingTime.CalculateAutomatically = true;
            DataTable table = workingTime.Intervals;

            Assert.AreEqual(DateTime.Parse("10:00"), table.Rows[0]["End"]);
        }
        [Test]
        public void PossibleWorkInterruptionLess()
        {
            ITimeLog timeLog = NewMock<ITimeLog>();
            Stub.On(timeLog).GetProperty("Activities").Will(Return.Value(new List<IActivity>(
                new IActivity[] {
                    new Activity("work1", DateTime.Parse("9:00"), TimeSpan.Parse("1:00")),
                    new Activity("dinner", DateTime.Parse("10:00"), TimeSpan.Parse("0:15")),
                    new Activity("work1", DateTime.Parse("10:15"), TimeSpan.Parse("0:30"))
                })));
            Stub.On(timeLog).GetProperty("Data").Will(Return.Value(new DataTable()));
            taskCollection = TaskCollection.Default;
            taskCollection.LinkActivityAndTask("work1", "Work");
            taskCollection.LinkActivityAndTask("dinner", "Rest");
            
            workingTime = new WorkingTime(timeLog, taskCollection);
            workingTime.PossibleWorkInterruption = TimeSpan.Parse("0:15");
            
            Assert.AreEqual(TimeSpan.Parse("1:45"),workingTime.TimeOnWork);
        }
        [Test]
        public void PossibleWorkInterruptionMore()
        {
            ITimeLog timeLog = NewMock<ITimeLog>();
            Stub.On(timeLog).GetProperty("Activities").Will(Return.Value(new List<IActivity>(
                new IActivity[] {
                    new Activity("work1", DateTime.Parse("9:00"), TimeSpan.Parse("1:00")),
                    new Activity("dinner", DateTime.Parse("10:00"), TimeSpan.Parse("0:15")),
                    new Activity("work1", DateTime.Parse("10:15"), TimeSpan.Parse("0:30"))
                })));
            Stub.On(timeLog).GetProperty("Data").Will(Return.Value(new DataTable()));
            taskCollection = TaskCollection.Default;
            taskCollection.LinkActivityAndTask("work1", "Work");
            taskCollection.LinkActivityAndTask("dinner", "Rest");

            workingTime = new WorkingTime(timeLog, taskCollection);
            workingTime.PossibleWorkInterruption = TimeSpan.Parse("0:14");

            Assert.AreEqual(TimeSpan.Parse("1:30"), workingTime.TimeOnWork);
        }
        [Test]
        public void WorkingTasksTime()
        {
            ITimeLog timeLog = NewMock<ITimeLog>();
            Stub.On(timeLog).GetProperty("Activities").Will(Return.Value(new List<IActivity>(
                new IActivity[] {
                    new Activity("work", DateTime.Parse("0:00"), TimeSpan.Parse("0:01"))
                })));
            Stub.On(timeLog).GetProperty("Data").Will(Return.Value(new DataTable()));
            workingTime = new WorkingTime(timeLog, taskCollection);
            Assert.AreEqual(TimeSpan.Parse("0:01"), workingTime.WorkingTasksTime);
        }
        [Test]
        public void WorkingTasksTimeExcludeRest()
        {
            ITimeLog timeLog = NewMock<ITimeLog>();
            Stub.On(timeLog).GetProperty("Activities").Will(Return.Value(new List<IActivity>(
                new IActivity[] {
                    new Activity("work", DateTime.Parse("0:00"), TimeSpan.Parse("0:04")),
                    new Activity("rest", DateTime.Parse("0:00"), TimeSpan.Parse("0:01"))
                })));
            Stub.On(timeLog).GetProperty("Data").Will(Return.Value(new DataTable()));
            workingTime = new WorkingTime(timeLog, taskCollection);
            Assert.AreEqual(TimeSpan.Parse("0:04"), workingTime.WorkingTasksTime);
        }
        [Test]
        public void WorkingTasksTimeForEmptyTask()
        {
            ITimeLog timeLog = NewMock<ITimeLog>();
            Stub.On(timeLog).GetProperty("Activities").Will(Return.Value(new List<IActivity>(
                new IActivity[] {
                    new Activity(null, DateTime.Parse("0:00"), TimeSpan.Parse("0:04")),
                    new Activity("rest", DateTime.Parse("0:00"), TimeSpan.Parse("0:01"))
                })));
            Stub.On(timeLog).GetProperty("Data").Will(Return.Value(new DataTable()));
            workingTime = new WorkingTime(timeLog, taskCollection);
            Assert.AreEqual(TimeSpan.Parse("0:00"), workingTime.WorkingTasksTime);
        }
        [Test]
        public void WorkingIntervalsExcludeRestOnRightEdge()
        {
            ITimeLog timeLog = NewMock<ITimeLog>();
            Stub.On(timeLog).GetProperty("Activities").Will(Return.Value(new List<IActivity>(
                new IActivity[] {
                    new Activity("work", DateTime.Parse("9:00"), TimeSpan.Parse("1:00")),
                    new Activity("rest", DateTime.Parse("10:00"), TimeSpan.Parse("0:15"))
                })));
            Stub.On(timeLog).GetProperty("Data").Will(Return.Value(new DataTable()));
            workingTime = new WorkingTime(timeLog, taskCollection);

            Assert.AreEqual(DateTime.Parse("10:00"), workingTime.Intervals.Rows[0]["End"]);
        }
        [Test]
        public void WorkingIntervalsExcludeRestOnLeftEdge()
        {
            ITimeLog timeLog = NewMock<ITimeLog>();
            Stub.On(timeLog).GetProperty("Activities").Will(Return.Value(new List<IActivity>(
                new IActivity[] {
                    new Activity("rest", DateTime.Parse("9:00"), TimeSpan.Parse("0:15")),
                    new Activity("work", DateTime.Parse("9:15"), TimeSpan.Parse("10:15"))
                })));
            Stub.On(timeLog).GetProperty("Data").Will(Return.Value(new DataTable()));
            workingTime = new WorkingTime(timeLog, taskCollection);

            Assert.AreEqual(DateTime.Parse("9:15"), workingTime.Intervals.Rows[0]["Start"]);
        }
        [Test]
        public void IntervalsChangesWhenActivityBecomesWorking()
        {
            ITaskCollection taskCollection = NewMock<ITaskCollection>();
            ITimeLog timeLog = NewMock<ITimeLog>();
            Expect.Once.On(taskCollection).Method("IsWorkingActivity").With("test").
                Will(Return.Value(false));
            Stub.On(timeLog).GetProperty("Data").Will(Return.Value(null));
            List<IActivity> activities = new List<IActivity>();
            activities.Add(new Activity("test",DateTime.Now,TimeSpan.FromMinutes(1)));
            Stub.On(timeLog).GetProperty("Activities").Will(Return.Value(activities));
            workingTime = new WorkingTime(timeLog, taskCollection);
            Expect.AtLeastOnce.On(taskCollection).Method("IsWorkingActivity").With("test").
                Will(Return.Value(true));
            TimeSpan workingTasksTime = workingTime.WorkingTasksTime;
            Assert.AreEqual(1, workingTime.Intervals.Rows.Count);
        }
    }
}
