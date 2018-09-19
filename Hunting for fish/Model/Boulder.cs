using System;
using System.Windows;

namespace Hunting_for_fish
{
    class Boulder
    {
        private MovementBoulder movementBoulder;
        private ControlFishes fishes;
        private ControlSounds sounds;
        private Point cursorPosition;
        private int hitArea;
        private int counterThrows;
        private int numberOfFish;
        private int mainWindowWidth;
        private int mainWindowHeight;
        private bool forPlayMissSound;

        public Boulder(ControlFishes fishes, ControlSounds sounds, int numberOfFish, Thickness initialCoordinates, Thickness playerThickness, int mainWindowWidth, int mainWindowHeight)
        {
            movementBoulder = new MovementBoulder(this, initialCoordinates, playerThickness, mainWindowWidth, mainWindowHeight);
            this.fishes = fishes;
            this.sounds = sounds;
            this.numberOfFish = numberOfFish;
            this.mainWindowWidth = mainWindowWidth;
            this.mainWindowHeight = mainWindowHeight;
            hitArea = 20;
            counterThrows = 0;
        }

        public void CheckHit()
        {
            forPlayMissSound = true;
            for (int i = 0; i < numberOfFish; i++)
            {
                Thickness fishMargin = fishes.GetFishMargin(i);
                if (((fishMargin.Left != -1.0) || (fishMargin.Bottom != -1.0)) 
                    && ((((cursorPosition.X + hitArea) > fishMargin.Left) 
                    && ((cursorPosition.X - hitArea) < ((mainWindowWidth - 15) - fishMargin.Right))) 
                    && (((cursorPosition.Y + hitArea) > fishMargin.Top) 
                    && ((cursorPosition.Y - hitArea) < ((mainWindowHeight - 37) - fishMargin.Bottom)))))
                {
                    fishes.Hit(i);
                    sounds.PlayHitSound();
                    forPlayMissSound = false;
                }
            }
            if (forPlayMissSound)
            {
                sounds.PlayMissSound();
            }
        }

        public ISubject GetMovementBoulder()
        {
            return movementBoulder;
        }

        public void Reset()
        {
            movementBoulder.ResetStep();
        }

        public void ThrowBoulder(Point temp_CursorPosition)
        {
            cursorPosition = temp_CursorPosition;
            counterThrows++;
            if (counterThrows > 50)
            {
                movementBoulder.SloweStep();
                counterThrows = 0;
            }
            movementBoulder.Start(cursorPosition);
        }
    }
}
