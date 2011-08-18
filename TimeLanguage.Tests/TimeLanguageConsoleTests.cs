using System;
using System.IO;
using NUnit.Framework;
using NMock2;

namespace LifeIdea.TimeLanguage
{
    [TestFixture]
    public class TimeLanguageConsoleTests:Mockery
    {
        private IInterpreter realInterpreter;

        [SetUp]
        public void SetUp()
        {
            TimeLanguageConsole.Writer = new StringWriter();
            realInterpreter = TimeLanguageConsole.Interpreter;
        }
        [TearDown]
        public void TearDown()
        {
            TimeLanguageConsole.Interpreter = realInterpreter;
        }
        [Test]
        public void OutputsText()
        {
            TimeLanguageConsole.Main("test", "activity");
            Assert.That(TimeLanguageConsole.Writer.ToString().Contains("test activity"));
        }
        [Test]
        public void CallsInterpreter()
        {
            TimeLanguageConsole.Interpreter = NewMock<IInterpreter>();
            Stub.On(TimeLanguageConsole.Interpreter).GetProperty("LastLines").Will(Return.Value(new string[] { "test" }));
            Expect.Once.On(TimeLanguageConsole.Interpreter).Method("ProcessLine").With("activity");
            TimeLanguageConsole.Main("activity");
            VerifyAllExpectationsHaveBeenMet();
        }
        [Test]
        public void OutputLast3InterpreterLines()
        {
            TimeLanguageConsole.Interpreter = NewMock < IInterpreter>();
            Stub.On(TimeLanguageConsole.Interpreter).Method("ProcessLine");
            Expect.Once.On(TimeLanguageConsole.Interpreter).GetProperty("LastLines").Will(Return.Value(new string[] { "test last line" }));
            TimeLanguageConsole.Main("any");
            Assert.AreEqual("test last line\r\n", TimeLanguageConsole.Writer.ToString());
            VerifyAllExpectationsHaveBeenMet();
        }
        [Test]
        public void EmptyInputDontCallProcessLine()
        {
            TimeLanguageConsole.Interpreter = NewMock<IInterpreter>();
            Stub.On(TimeLanguageConsole.Interpreter).GetProperty("LastLines").Will(Return.Value(new string[] { "test" }));
            TimeLanguageConsole.Main(new string[] { });
            VerifyAllExpectationsHaveBeenMet();
        }
    }
}
