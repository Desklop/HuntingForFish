using System;
using System.Timers;

namespace Hunting_for_fish
{
    class GameTimer : ISubject
    {
        private IObserver observer;
        private Timer timerForGame = new Timer(1000.0);
        private string currentTimerContent = "00:00";

        public GameTimer()
        {
            timerForGame.Elapsed += new ElapsedEventHandler(TimerForGame_Content);
            timerForGame.AutoReset = true;
        }

        public void AddObserver(IObserver observer)
        {
            this.observer = observer;
        }

        public string GetGameTimerValue()
        {
            return currentTimerContent;
        }

        public void Notify(string option)
        {
            if (option == "set")
            {
                observer.UpateGameTimer(currentTimerContent);
            }
        }

        public void Reset()
        {
            currentTimerContent = "00:00";
            Notify("set");
        }

        public void Start()
        {
            if (!timerForGame.Enabled)
            {
                timerForGame.Start();
            }
        }

        public void Stop()
        {
            if (timerForGame.Enabled)
            {
                timerForGame.Stop();
            }
        }

        private void TimerForGame_Content(object sender, EventArgs e)
        {
            int min = Convert.ToInt32(currentTimerContent.Substring(0, 2));
            int sec = Convert.ToInt32(currentTimerContent.Substring(3));
            currentTimerContent = null;
            if (sec < 60)
            {
                sec++;
            }
            else
            {
                sec = 0;
                min++;
            }
            string minStr = Convert.ToString(min);
            string secStr = Convert.ToString(sec);
            if (min < 10)
            {
                currentTimerContent = "0" + minStr;
            }
            else
            {
                currentTimerContent = minStr;
            }
            currentTimerContent = currentTimerContent + ":";
            if (sec < 10)
            {
                currentTimerContent = currentTimerContent + "0" + secStr;
            }
            else
            {
                currentTimerContent = currentTimerContent + secStr;
            }
            Notify("set");
        }
    }
}
