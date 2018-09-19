using System;
using System.Windows;

namespace Hunting_for_fish
{
    class Controller
    {
        private View view;
        private ControlFishes fishes;
        private Boulder boulderLogic;
        private Score score;
        private GameTimer gameTimer;
        private ControlSounds sounds;
        private bool forPause;
        private bool forStart;
        private bool forMute;

        public Controller(View view)
        {
            this.view = view;
            forPause = true;
            forStart = true;
            forMute = false;
        }

        public void AboutTheGame()
        {
            view.AboutTheGameButtonClick_message("  Вначале кол-во рыб постепенно увеличивается до максимального значения. Затем, каждый раз, после вылавливания всех рыб, увеличивается скорость их движения.\n\n  ВАЖНО: не разбрасывайтесь попросту булыжниками! Ведь с каждым броском рыбак устаёт...\n\nАвтор: Клим Владислав Олегович, гр. 521702, БГУИР, ФИТУ.");
        }

        public void ForMute()
        {
            if (forMute)
            {
                sounds.Mute();
                view.SetForMuteButtonContext_unMuteImage();
                forMute = false;
            }
            else
            {
                sounds.UnMute();
                view.SetForMuteButtonContext_muteImage();
                forMute = true;
            }
        }

        public ISubject[] GetAllFish()
        {
            return fishes.GetAllFish();
        }

        public ISubject[] GetAllMovementFish()
        {
            return fishes.GetAllMovementFish();
        }

        public ISubject GetGameTimer()
        {
            return gameTimer;
        }

        public ISubject GetMovementBoulder()
        {
            return boulderLogic.GetMovementBoulder();
        }

        public ISubject GetScore()
        {
            return score;
        }

        public void Inizialize(int height, int weight)
        {
            sounds = new ControlSounds();
            gameTimer = new GameTimer();
            score = new Score(gameTimer);
            fishes = new ControlFishes(view.arrayFish.Length, score);
            boulderLogic = new Boulder(fishes, sounds, view.arrayFish.Length, view.boulder.Margin, view.player.Margin, weight, height);
            view.SetForMuteButtonContext_unMuteImage();
            view.SetForAboutTheGameButtonContext_questionImage();
            fishes.Initialize(view.GetFishesThickness(), view.waterFrame.Margin, height - ((int)view.waterFrame.Margin.Bottom));
        }

        public void PauseAndResume()
        {
            if (forPause)
            {
                fishes.Pause();
                gameTimer.Stop();
                view.SetPauseAndResumeButtonContent("Продолжить");
                view.ResetCursor();
                forPause = false;
            }
            else
            {
                fishes.Resume();
                gameTimer.Start();
                view.SetPauseAndResumeButtonContent("Пауза");
                view.SetCursor();
                forPause = true;
            }
        }

        public void StartAndStop()
        {
            if (forStart)
            {
                fishes.Start();
                gameTimer.Start();
                sounds.PlayBackgroundMusic();
                view.SetStartAndStopButtonContent("Стоп");
                view.SetPauseAndResumeButtonCondition(true);
                view.SetCursor();
                forStart = false;
            }
            else
            {
                fishes.Stop();
                fishes.Reset();
                boulderLogic.Reset();
                score.Reset();
                gameTimer.Stop();
                gameTimer.Reset();
                sounds.StopBackgroundMusic();
                view.SetStartAndStopButtonContent("Старт");
                view.SetPauseAndResumeButtonCondition(false);
                view.ResetCursor();
                forStart = true;
            }
        }

        public void Throw(Point cursorPosition)
        {
            if (forPause && !forStart)
            {
                boulderLogic.ThrowBoulder(cursorPosition);
            }
        }
    }
}
