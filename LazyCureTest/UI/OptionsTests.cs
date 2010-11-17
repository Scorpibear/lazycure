using System;
using NUnit.Framework;
using NMock2;
using LifeIdea.LazyCure.Interfaces;

namespace LifeIdea.LazyCure.UI
{
    [TestFixture]
    public class OptionsTests: Mockery
    {
        [Test]
        public void OptionsAreLoadedFromSettings()
        {
            ISettings settings = NewMock<ISettings>();
            
            Expect.Once.On(settings).GetProperty("ActivitiesNumberInTray").Will(Return.Value(0));
            Expect.Once.On(settings).GetProperty("HotKeyToActivate");
            Expect.Once.On(settings).GetProperty("HotKeyToSwitch");
            Expect.Once.On(settings).GetProperty("LeftClickOnTray").Will(Return.Value(false));
            Expect.Once.On(settings).GetProperty("MaxActivitiesInHistory").Will(Return.Value(0));
            Expect.Once.On(settings).GetProperty("ReminderTime").Will(Return.Value(TimeSpan.Zero));
            Expect.Once.On(settings).GetProperty("SaveAfterDone").Will(Return.Value(false));
            Expect.Once.On(settings).GetProperty("SplitByComma").Will(Return.Value(false));
            Expect.Once.On(settings).GetProperty("SwitchOnLogOff").Will(Return.Value(false));
            Expect.Once.On(settings).GetProperty("SwitchTimeLogAtMidnight").Will(Return.Value(false));
            Expect.Once.On(settings).GetProperty("TimeLogsFolder");
            Expect.Once.On(settings).GetProperty("TweetingActivity");
            Expect.Once.On(settings).GetProperty("UseTweetingActivity").Will(Return.Value(true));
            Expect.Once.On(settings).GetProperty("TwitterEnabled").Will(Return.Value(false));
            Expect.Once.On(settings).GetProperty("TwitterPassword");
            Expect.Once.On(settings).GetProperty("TwitterUsername");

            Options options = new Options(settings);
            VerifyAllExpectationsHaveBeenMet();
        }

        [Test]
        public void UpdateSettings()
        {
            // Given
            Options options = new Options();
            ISettings settings = NewMock<ISettings>();
            options.Settings = settings;
            Expect.Once.On(settings).SetProperty("ActivitiesNumberInTray");
            Expect.Once.On(settings).SetProperty("HotKeyToActivate");
            Expect.Once.On(settings).SetProperty("HotKeyToSwitch");
            Expect.Once.On(settings).SetProperty("LeftClickOnTray");
            Expect.Once.On(settings).SetProperty("MaxActivitiesInHistory");
            Expect.Once.On(settings).SetProperty("ReminderTime");
            Expect.Once.On(settings).SetProperty("SaveAfterDone");
            Expect.Once.On(settings).SetProperty("SplitByComma");
            Expect.Once.On(settings).SetProperty("SwitchOnLogOff");
            Expect.Once.On(settings).SetProperty("SwitchTimeLogAtMidnight");
            Expect.Once.On(settings).SetProperty("TimeLogsFolder");
            Expect.Once.On(settings).SetProperty("TweetingActivity");
            Expect.Once.On(settings).SetProperty("UseTweetingActivity");
            Expect.Once.On(settings).SetProperty("TwitterEnabled");
            Expect.Once.On(settings).SetProperty("TwitterPassword");
            Expect.Once.On(settings).SetProperty("TwitterUsername");
            // When
            options.UpdateSettings(TimeSpan.Zero);
            // Then
            VerifyAllExpectationsHaveBeenMet();
        }

        

    }
}
