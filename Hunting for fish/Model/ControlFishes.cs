using System;
using System.Windows;

namespace Hunting_for_fish
{
    class ControlFishes
    {
        private Fish[] arrayFishLogic;
        private Random rand = new Random((int)DateTime.Now.Ticks);
        private int counterHit;
        private int numberOfActiveFish;

        public ControlFishes(int numberOfFish, Score scoreLogic)
        {
            arrayFishLogic = new Fish[numberOfFish];
            for (int i = 0; i < arrayFishLogic.Length; i++)
            {
                arrayFishLogic[i] = new Fish(scoreLogic);
            }
            counterHit = 0;
            numberOfActiveFish = 0;
        }

        public void Accelerate()
        {
            for (int i = 0; i < numberOfActiveFish; i++)
            {
                arrayFishLogic[i].AccelerateFish();
            }
        }

        public ISubject[] GetAllFish()
        {
            return arrayFishLogic;
        }

        public ISubject[] GetAllMovementFish()
        {
            ISubject[] subjectArray = new ISubject[arrayFishLogic.Length];
            for (int i = 0; i < arrayFishLogic.Length; i++)
            {
                subjectArray[i] = arrayFishLogic[i].GetMovementFish();
            }
            return subjectArray;
        }

        public Thickness GetFishMargin(int indexCurrentFish)
        {
            if (indexCurrentFish < numberOfActiveFish)
            {
                return arrayFishLogic[indexCurrentFish].GetFishMargin();
            }
            return new Thickness(-1.0);
        }

        public void Hit(int index)
        {
            if (arrayFishLogic[index].HitInFish())
            {
                counterHit++;
                if (counterHit == numberOfActiveFish)
                {
                    Start();
                }
            }
        }

        public void Initialize(Thickness[] initialFishesThickness, Thickness waterFrameThickness, int waterFrameHeight)
        {
            for (int i = 0; i < arrayFishLogic.Length; i++)
            {
                int initialSpeed = rand.Next(1, 3);
                if (i < 4)
                {
                    arrayFishLogic[i].Initialize(i, initialSpeed, 1, initialFishesThickness, waterFrameThickness, waterFrameHeight);
                }
                else if (i < 7)
                {
                    arrayFishLogic[i].Initialize(i, initialSpeed, 2, initialFishesThickness, waterFrameThickness, waterFrameHeight);
                }
                else
                {
                    arrayFishLogic[i].Initialize(i, initialSpeed, 3, initialFishesThickness, waterFrameThickness, waterFrameHeight);
                }
            }
        }

        public void Pause()
        {
            for (int i = 0; i < numberOfActiveFish; i++)
            {
                arrayFishLogic[i].PauseFish();
            }
        }

        public void Reset()
        {
            for (int i = 0; i < numberOfActiveFish; i++)
            {
                arrayFishLogic[i].ResetFish();
            }
            numberOfActiveFish = 0;
        }

        public void Resume()
        {
            for (int i = 0; i < numberOfActiveFish; i++)
            {
                arrayFishLogic[i].ResumeFish();
            }
        }

        public void Start()
        {
            counterHit = 0;
            if (numberOfActiveFish < arrayFishLogic.Length)
            {
                numberOfActiveFish++;
            }
            else
            {
                Accelerate();
            }
            for (int i = 0; i < numberOfActiveFish; i++)
            {
                arrayFishLogic[i].RunFish();
            }
        }

        public void Stop()
        {
            for (int i = 0; i < numberOfActiveFish; i++)
            {
                arrayFishLogic[i].StopFish();
            }
        }
    }
}
