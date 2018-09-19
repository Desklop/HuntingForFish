using System;
using System.IO;

namespace Hunting_for_fish
{
    class Score : ISubject
    {
        private IObserver observer;
        private GameTimer gameTimer;
        private int score;
        private int maxScore;
        private string maxScoreTime;
        private string pathToMaxScoreFile;

        public Score(GameTimer gameTimer)
        {
            pathToMaxScoreFile = AppDomain.CurrentDomain.BaseDirectory + "/MaxScore.txt";
            maxScore = 0;
            maxScoreTime = "00:00";
            if (File.Exists(pathToMaxScoreFile))
            {
                StreamReader reader = new StreamReader(pathToMaxScoreFile);
                maxScore = Convert.ToInt32(reader.ReadLine());
                maxScoreTime = reader.ReadLine();
                reader.Close();
            }
            score = 0;
            this.gameTimer = gameTimer;
        }

        public void AddObserver(IObserver observer)
        {
            this.observer = observer;
        }

        ~Score()
        {
            StreamWriter writer = new StreamWriter(File.OpenWrite(pathToMaxScoreFile));
            writer.WriteLine(maxScore);
            writer.WriteLine(maxScoreTime);
            writer.Close();
        }
        
        public void IncreaseScore()
        {
            score++;
            Notify("set");
            if (score > maxScore)
            {
                maxScore = score;
                maxScoreTime = gameTimer.GetGameTimerValue();
            }
            Notify("set max");
        }

        public void Notify(string option)
        {
            switch (option)
            {
                case "set":
                    observer.UpdateScore(score);
                    break;

                case "set max":
                    observer.UpdateMaxScore(maxScore, maxScoreTime);
                    break;

                case "reset":
                    observer.UpdateScore(0);
                    break;
            }
        }

        public void Reset()
        {
            score = 0;
            Notify("reset");
        }
    }
}
