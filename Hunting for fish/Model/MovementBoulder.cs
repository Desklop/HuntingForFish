using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace Hunting_for_fish
{
    class MovementBoulder : ISubject
    {
        private IObserver observer;
        private DispatcherTimer timerForBoulder;
        private Thickness initialBoulderThickness;
        private Thickness temporaryBoulderThickness;
        private Thickness playerThickness;
        private RotateTransform rotate;
        private int speedX;
        private int speedY;
        private int step;
        private int mainWindowWidth;
        private int mainWindowHeight;
        private bool forStop;
        private Point cursorPosition;
        private Boulder boulderLogic;

        public MovementBoulder(Boulder boulderLogic, Thickness initialBoulderThickness, Thickness playerThickness, int mainWindowWidth, int mainWindowHeight)
        {
            this.boulderLogic = boulderLogic;
            this.mainWindowWidth = mainWindowWidth;
            this.mainWindowHeight = mainWindowHeight;
            this.initialBoulderThickness = initialBoulderThickness;
            this.playerThickness = playerThickness;
            step = 20;
            timerForBoulder = new DispatcherTimer();
            timerForBoulder.Interval = new TimeSpan(0, 0, 0, 0, 1);
            timerForBoulder.Tick += new EventHandler(TimerForBoulder_Content);
        }

        public void AddObserver(IObserver observer)
        {
            this.observer = observer;
        }

        public void Notify(string option)
        {
            switch (option)
            {
                case "opacity = 100":
                    observer.UpdateBoulderOpacity(100);
                    break;

                case "opacity = 0":
                    observer.UpdateBoulderOpacity(0);
                    break;

                case "rotate player":
                    observer.UpdatePlayerRenderTransform(rotate);
                    break;

                case "location":
                    observer.UpdateBoulderMargin(temporaryBoulderThickness);
                    break;

                case "reset location":
                    observer.UpdateBoulderMargin(initialBoulderThickness);
                    break;
            }
        }

        private void PlayerRotate()
        {
            rotate = new RotateTransform();
            rotate.CenterX = ((mainWindowWidth - 21) - (playerThickness.Left + playerThickness.Right)) / 2.0;
            rotate.CenterY = ((mainWindowHeight - 37) - (playerThickness.Top + playerThickness.Bottom)) / 2.0;
            double num = 0.0;
            if (cursorPosition.X < ((mainWindowWidth / 2) - 120))
            {
                num = 315.0;
            }
            else if (cursorPosition.X > ((mainWindowWidth / 2) + 120))
            {
                num = 45.0;
            }
            rotate.Angle = num;
            Notify("rotate player");
        }

        public void ResetStep()
        {
            step = 20;
        }

        public void SloweStep()
        {
            step += 3;
        }

        public void Start(Point Cursor)
        {
            if (timerForBoulder.IsEnabled)
            {
                StopTimer();
            }
            cursorPosition = Cursor;
            PlayerRotate();
            cursorPosition.X += 15.0;
            Notify("opacity = 100");
            if (initialBoulderThickness.Left > cursorPosition.X)
            {
                speedX = ((int)(initialBoulderThickness.Left - cursorPosition.X)) / step;
                if (speedX == 0)
                {
                    speedX = 1;
                }
            }
            else if (initialBoulderThickness.Left < cursorPosition.X)
            {
                speedX = ((int)(cursorPosition.X - initialBoulderThickness.Left)) / step;
                if (speedX == 0)
                {
                    speedX = 1;
                }
            }
            speedY = ((int)(initialBoulderThickness.Top - cursorPosition.Y)) / step;
            forStop = false;
            temporaryBoulderThickness = initialBoulderThickness;
            timerForBoulder.Start();
        }

        private void StopTimer()
        {
            timerForBoulder.Stop();
            Notify("reset location");
            Notify("opacity = 0");
            boulderLogic.CheckHit();
        }

        private void TimerForBoulder_Content(object sender, EventArgs e)
        {
            Dispatcher.CurrentDispatcher.Invoke(() => {
                forStop = true;
                if ((cursorPosition.X < (mainWindowWidth / 2)) && (temporaryBoulderThickness.Left >= (cursorPosition.X + speedX)))
                {
                    temporaryBoulderThickness.Left -= speedX;
                    temporaryBoulderThickness.Right += speedX;
                    forStop = false;
                }
                if ((cursorPosition.X > (mainWindowWidth / 2)) && (temporaryBoulderThickness.Left <= (cursorPosition.X - speedX)))
                {
                    temporaryBoulderThickness.Left += speedX;
                    temporaryBoulderThickness.Right -= speedX;
                    forStop = false;
                }
                if (temporaryBoulderThickness.Top >= (cursorPosition.Y + speedY))
                {
                    temporaryBoulderThickness.Top -= speedY;
                    temporaryBoulderThickness.Bottom += speedY;
                    forStop = false;
                }
                Notify("location");
                if (forStop)
                {
                    StopTimer();
                }
            });
        }
    }
}
