using System;
using System.Timers;
using System.Windows;

namespace Hunting_for_fish
{
    class MovementFish : ISubject
    {
        private IObserver observer;
        private Timer timerForFish = new Timer(31.0);
        private Random rand;
        public int initialOffset;
        private int speedX;
        private int speedY;
        private int initialSpeed;
        private int forSaveSpeedX;
        private int forSaveSpeedY;
        private int currentLengthOfPath;
        private int lengthOfPathWithoutChangingDirection;
        private int typeFish;
        private int waterFrameHeight;
        private int indexCurrentFish;
        private Thickness waterFrameThickness;
        private Thickness initialFishThickness;
        public Thickness temporaryFishThickness;
        public bool forPause;
        private bool forInvertX;
        private bool forInvertImage;
        private bool forInvertY;

        public MovementFish(int indexCurrentFish, int initialSpeed, int type, Thickness initialFishThickness, Thickness waterFrameThickness, int waterFrameHeight)
        {
            timerForFish.Elapsed += new ElapsedEventHandler(TimerForFish_Content);
            timerForFish.AutoReset = true;
            rand = new Random((int)DateTime.Now.Ticks);
            this.indexCurrentFish = indexCurrentFish;
            speedX = speedY = this.initialSpeed = initialSpeed;
            forPause = false;
            typeFish = type;
            currentLengthOfPath = 0;
            lengthOfPathWithoutChangingDirection = 1;
            this.initialFishThickness = initialFishThickness;
            this.waterFrameThickness = waterFrameThickness;
            this.waterFrameHeight = waterFrameHeight;
        }

        public void Accelerate()
        {
            int interval = (int)timerForFish.Interval;
            if (interval > 1)
            {
                timerForFish.Interval -= 5.0;
            }
            else
            {
                AccelerateStep();
            }
        }

        public void AccelerateStep()
        {
            speedX++;
            speedY++;
        }

        public void AddObserver(IObserver observer)
        {
            this.observer = observer;
        }

        public void HitInFish()
        {
            Stop();
            if (forInvertImage)
            {
                Notify("hit image");
            }
            else
            {
                Notify("hit image");
            }
        }

        private void MovementToLeftBorder()
        {
            forInvertX = true;
            if ((temporaryFishThickness.Left <= (initialFishThickness.Left + speedX)) && forInvertImage)
            {
                forInvertX = false;
                forInvertImage = false;
                Notify("image");
            }
            else
            {
                Notify("image");
            }
            temporaryFishThickness.Left -= speedX;
            temporaryFishThickness.Right += speedX;
            if (((temporaryFishThickness.Bottom > waterFrameThickness.Bottom) && (temporaryFishThickness.Top > waterFrameThickness.Top)) && !forInvertY)
            {
                forInvertY = false;
                temporaryFishThickness.Top += speedY;
                temporaryFishThickness.Bottom -= speedY;
                if (currentLengthOfPath < lengthOfPathWithoutChangingDirection)
                {
                    currentLengthOfPath += speedX;
                }
                else
                {
                    currentLengthOfPath = 0;
                    lengthOfPathWithoutChangingDirection = rand.Next(50, waterFrameHeight);
                    forInvertY = true;
                }
            }
            else
            {
                forInvertY = true;
                temporaryFishThickness.Top -= speedY;
                temporaryFishThickness.Bottom += speedY;
                if ((temporaryFishThickness.Top <= (waterFrameThickness.Top + speedY)) && forInvertY)
                {
                    forInvertY = false;
                }
                if (currentLengthOfPath < lengthOfPathWithoutChangingDirection)
                {
                    currentLengthOfPath += speedX;
                }
                else
                {
                    currentLengthOfPath = 0;
                    lengthOfPathWithoutChangingDirection = rand.Next(50, waterFrameHeight);
                    forInvertY = false;
                }
            }
        }

        private void MovementToRightBorder()
        {
            forInvertX = false;
            if ((temporaryFishThickness.Left >= (initialFishThickness.Right - speedX)) && !forInvertImage)
            {
                forInvertImage = true;
                Notify("image");
            }
            else
            {
                Notify("image");
            }
            temporaryFishThickness.Left += speedX;
            temporaryFishThickness.Right -= speedX;
            if (((temporaryFishThickness.Bottom > waterFrameThickness.Bottom) && (temporaryFishThickness.Top > waterFrameThickness.Top)) && !forInvertY)
            {
                forInvertY = false;
                temporaryFishThickness.Top += speedY;
                temporaryFishThickness.Bottom -= speedY;
                if (currentLengthOfPath < lengthOfPathWithoutChangingDirection)
                {
                    currentLengthOfPath += speedX;
                }
                else
                {
                    currentLengthOfPath = 0;
                    lengthOfPathWithoutChangingDirection = rand.Next(50, waterFrameHeight);
                    forInvertY = true;
                }
            }
            else
            {
                forInvertY = true;
                temporaryFishThickness.Top -= speedY;
                temporaryFishThickness.Bottom += speedY;
                if ((temporaryFishThickness.Top <= (waterFrameThickness.Top + speedY)) && forInvertY)
                {
                    currentLengthOfPath = 0;
                    lengthOfPathWithoutChangingDirection = rand.Next(50, waterFrameHeight);
                    forInvertY = false;
                }
                if (currentLengthOfPath < lengthOfPathWithoutChangingDirection)
                {
                    currentLengthOfPath += speedX;
                }
                else
                {
                    currentLengthOfPath = 0;
                    lengthOfPathWithoutChangingDirection = rand.Next(50, waterFrameHeight);
                    forInvertY = false;
                }
            }
        }

        public void Notify(string option)
        {
            switch (option)
            {
                case "location":
                    observer.UpdateFishMargin(indexCurrentFish, temporaryFishThickness);
                    break;

                case "image":
                    if (forInvertImage)
                    {
                        observer.UpdateInvertedImageForFish(indexCurrentFish, typeFish);
                    }
                    else
                    {
                        observer.UpdateNotInvertedImageForFish(indexCurrentFish, typeFish);
                    }
                    break;

                case "hit image":
                    if (forInvertImage)
                    {
                        observer.UpdateInvertedImageHitForFish(indexCurrentFish, typeFish);
                    }
                    else
                    {
                        observer.UpdateNotInvertedImageHitForFish(indexCurrentFish, typeFish);
                    }
                    break;
            }
        }

        public void Pause()
        {
            forSaveSpeedX = speedX;
            forSaveSpeedY = speedY;
            speedX = speedY = 0;
        }

        public void ResetAccelerate()
        {
            timerForFish.Interval = 31.0;
            speedX = speedY = initialSpeed;
        }

        public void Resume()
        {
            speedX = forSaveSpeedX;
            speedY = forSaveSpeedY;
        }

        public void SloweStep()
        {
            speedX--;
            speedY--;
        }

        public void Start()
        {
            if (!timerForFish.Enabled)
            {
                Notify("location");
                temporaryFishThickness = initialFishThickness;
                temporaryFishThickness.Left += initialOffset;
                temporaryFishThickness.Right -= initialOffset;
                Notify("location");
                timerForFish.Start();
            }
        }

        public void Stop()
        {
            if (timerForFish.Enabled)
            {
                timerForFish.Stop();
            }
        }

        private void TimerForFish_Content(object sender, EventArgs e)
        {
            if ((temporaryFishThickness.Left < initialFishThickness.Right) && !forInvertX)
            {
                MovementToRightBorder();
            }
            else
            {
                MovementToLeftBorder();
            }
            Notify("location");
        }
    }
}
