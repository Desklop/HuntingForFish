using System;
using System.Windows;
using System.Windows.Media;

namespace Hunting_for_fish
{
    interface IObserver
    {
        void UpateGameTimer(string time);
        void UpdateBoulderMargin(Thickness margin);
        void UpdateBoulderOpacity(int value);
        void UpdateFishMargin(int index, Thickness temporaryFishMargin);
        void UpdateFishOpacity(int index, int value);
        void UpdateInvertedImageForFish(int index, int type);
        void UpdateInvertedImageHitForFish(int index, int type);
        void UpdateMaxScore(int value, string time);
        void UpdateNotInvertedImageForFish(int index, int type);
        void UpdateNotInvertedImageHitForFish(int index, int type);
        void UpdatePlayerRenderTransform(Transform transform);
        void UpdateScore(int value);
    }
}
