using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hunting_for_fish
{
    class Fish : ISubject
    {
        private IObserver observer;
        private MovementFish movementFish;
        private Score scoreLogic;
        private Random rand;
        private int indexCurrentFish;
        private bool hitInFish;

        public Fish(Score scoreLogic)
        {
            this.scoreLogic = scoreLogic;
            rand = new Random((int)DateTime.Now.Ticks);
        }

        public void AccelerateFish()
        {
            movementFish.Accelerate();
        }

        public void AddObserver(IObserver observer)
        {
            this.observer = observer;
        }

        public Thickness GetFishMargin()
        {
            return movementFish.temporaryFishThickness;
        }

        public MovementFish GetMovementFish()
        {
            return movementFish;
        }

        public bool HitInFish()
        {
            if (!hitInFish)
            {
                movementFish.HitInFish();
                scoreLogic.IncreaseScore();
                hitInFish = true;
                return true;
            }
            return false;
        }

        public void Initialize(int indexCurrentFish, int initialSpeed, int type, Thickness[] initialFishesThickness, Thickness waterFrameThickness, int waterFrameHeight)
        {
            movementFish = new MovementFish(indexCurrentFish, initialSpeed, type, initialFishesThickness[indexCurrentFish], waterFrameThickness, waterFrameHeight);
            this.indexCurrentFish = indexCurrentFish;
        }

        public void Notify(string option)
        {
            switch (option)
            {
                case "100":
                    observer.UpdateFishOpacity(indexCurrentFish, 100);
                    break;

                case "0":
                    observer.UpdateFishOpacity(indexCurrentFish, 0);
                    break;
            }
        }

        public void PauseFish()
        {
            movementFish.Pause();
            hitInFish = true;
        }

        public void ResetFish()
        {
            movementFish.ResetAccelerate();
        }

        public void ResumeFish()
        {
            movementFish.Resume();
            hitInFish = false;
        }

        public void RunFish()
        {
            movementFish.initialOffset = rand.Next(0, 500);
            hitInFish = false;
            Notify("100");
            movementFish.Start();
        }

        public void StopFish()
        {
            Notify("0");
            movementFish.Stop();
        }
    }
}
