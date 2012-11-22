using System;
using NUnit.Framework;
using NMock2;
using LifeIdea.LazyCure.Core.Time;

namespace LifeIdea.TimeLanguage
{
    [TestFixture]
    public class InterpreterTests: Mockery
    {
        ITimeSystem timeSystem;
        [SetUp]
        public void SetUp()
        {
            System.IO.File.Delete(FileManager.FILENAME);
            timeSystem = NewMock<ITimeSystem>();
        }
        [Test]
        public void InterpretSimpleLine()
        {
            using (Ordered)
            {
                Expect.Once.On(timeSystem).GetProperty("Now").Will(Return.Value(DateTime.Parse("4:55")));
                Expect.Once.On(timeSystem).GetProperty("Now").Will(Return.Value(DateTime.Parse("5:05")));
            }
            Interpreter interpreter = new Interpreter(timeSystem);
            interpreter.ProcessLine("get up");
            Assert.AreEqual("4:55:00\tget up\t0:10:00\t5:05:00", interpreter.LastLine);
        }
        [Test]
        public void LastLineWithoutEnd()
        {
            using (Ordered)
            {
                Expect.Once.On(timeSystem).GetProperty("Now").Will(Return.Value(DateTime.Parse("5:00")));
                Expect.Once.On(timeSystem).GetProperty("Now").Will(Return.Value(DateTime.Parse("13:18:03")));
            }
            Interpreter interpreter = new Interpreter(timeSystem);
            Assert.AreEqual(DateTime.Parse("13:18:03"),interpreter.CurrentActivity.End);
        }
        [Test]
        public void LastLineWhenThereAreNoLastActivity()
        {
            Interpreter interpreter = new Interpreter();
            Assert.AreEqual("", interpreter.LastLine);
        }
        [Test]
        public void SaveStateBetweenDifferentInstances()
        {
            Interpreter interpreter = new Interpreter();
            interpreter.ProcessLine("save and load");
            string before = interpreter.LastLine;
            interpreter = new Interpreter();
            Assert.AreEqual(before, interpreter.LastLine);
        }
    }
}
