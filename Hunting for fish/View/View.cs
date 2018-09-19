using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Hunting_for_fish
{
    class View : IObserver
    {
        public void AboutTheGameButtonClick_message(string context)
        {
            MessageBox.Show(context, "Об игре", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }

        private BitmapImage chooseForInvertedImage(int type)
        {
            if (type == 1)
            {
                return new BitmapImage(new Uri("Images/fish_Type1InvertedHorizontally.png", UriKind.Relative));
            }
            if (type == 2)
            {
                return new BitmapImage(new Uri("Images/fish_Type2InvertedHorizontally.png", UriKind.Relative));
            }
            return new BitmapImage(new Uri("Images/fish_Type3InvertedHorizontally.png", UriKind.Relative));
        }

        private BitmapImage chooseForInvertedImageHit(int type)
        {
            if (type == 1)
            {
                return new BitmapImage(new Uri("Images/fish_Type1InvertedHorizontallyHit.png", UriKind.Relative));
            }
            if (type == 2)
            {
                return new BitmapImage(new Uri("Images/fish_Type2InvertedHorizontallyHit.png", UriKind.Relative));
            }
            return new BitmapImage(new Uri("Images/fish_Type3InvertedHorizontallyHit.png", UriKind.Relative));
        }

        private BitmapImage chooseForNotInvertedImage(int type)
        {
            if (type == 1)
            {
                return new BitmapImage(new Uri("Images/fish_Type1.png", UriKind.Relative));
            }
            if (type == 2)
            {
                return new BitmapImage(new Uri("Images/fish_Type2.png", UriKind.Relative));
            }
            return new BitmapImage(new Uri("Images/fish_Type3.png", UriKind.Relative));
        }

        private BitmapImage chooseForNotInvertedImageHit(int type)
        {
            if (type == 1)
            {
                return new BitmapImage(new Uri("Images/fish_Type1Hit.png", UriKind.Relative));
            }
            if (type == 2)
            {
                return new BitmapImage(new Uri("Images/fish_Type2Hit.png", UriKind.Relative));
            }
            return new BitmapImage(new Uri("Images/fish_Type3Hit.png", UriKind.Relative));
        }

        public Thickness[] GetFishesThickness()
        {
            Thickness[] thicknessArray = new Thickness[arrayFish.Length];
            for (int i = 0; i < thicknessArray.Length; i++)
            {
                thicknessArray[i] = arrayFish[i].Margin;
            }
            return thicknessArray;
        }

        public void ResetCursor()
        {
            for (int i = 0; i < arrayFish.Length; i++)
            {
                arrayFish[i].Cursor = null;
            }
            waterFrame.Cursor = null;
            boulder.Cursor = null;
        }

        public void SetCursor()
        {
            Cursor cursor = new Cursor(Application.GetResourceStream(new Uri("Images/cross_type1.cur", UriKind.Relative)).Stream);
            for (int i = 0; i < arrayFish.Length; i++)
            {
                arrayFish[i].Cursor = cursor;
            }
            waterFrame.Cursor = cursor;
            boulder.Cursor = cursor;
        }

        public void SetForMuteButtonContext_muteImage()
        {
            Image image = new Image
            {
                Source = new BitmapImage(new Uri("Images/mute.png", UriKind.Relative))
            };
            forMuteButton.Content = image;
        }

        public void SetForMuteButtonContext_unMuteImage()
        {
            Image image = new Image
            {
                Source = new BitmapImage(new Uri("Images/unmute.png", UriKind.Relative))
            };
            forMuteButton.Content = image;
        }

        public void SetForAboutTheGameButtonContext_questionImage()
        {
            Image image = new Image();
            image.Source = new BitmapImage(new Uri("Images/question.png", UriKind.Relative));
            aboutTheGameButton.Content = image;
        }

        public void SetPauseAndResumeButtonCondition(bool condition)
        {
            pauseAndResumeButton.IsEnabled = condition;
        }

        public void SetPauseAndResumeButtonContent(string content)
        {
            pauseAndResumeButton.Content = content;
        }

        public void SetStartAndStopButtonContent(string content)
        {
            startAndStopButton.Content = content;
        }

        public void UpateGameTimer(string time)
        {
            //Иногда, если во время игры и при наличии убитых рыбок нажать на крестик (кнопку закрытия, что в правом верхнем углу)
            //вылетает ошибка System.Threading.Tasks.TaskCanceledException: "Отменена задача." именно вот в этом месте. Для решения
            //этой проблемы сделан костыль в виде try с пустым cath. Приложение при этом завершается нормально.
            try
            {
                gameTimerValue.Dispatcher.Invoke(() => gameTimerValue.Content = time);
            }
            catch { }
        }

        public void UpdateBoulderMargin(Thickness margin)
        {
            boulder.Margin = margin;
        }

        public void UpdateBoulderOpacity(int value)
        {
            boulder.Opacity = value;
        }

        public void UpdateFishMargin(int index, Thickness temporaryFishMargin)
        {
            //Иногда, если во время игры и при наличии убитых рыбок нажать на крестик (кнопку закрытия, что в правом верхнем углу)
            //вылетает ошибка System.Threading.Tasks.TaskCanceledException: "Отменена задача." именно вот в этом месте. Для решения
            //этой проблемы сделан костыль в виде try с пустым cath. Приложение при этом завершается нормально.
            try
            {
                arrayFish[index].Dispatcher.Invoke(() => arrayFish[index].Margin = temporaryFishMargin);
            }
            catch { }
        }

        public void UpdateFishOpacity(int index, int value)
        {
            arrayFish[index].Dispatcher.Invoke(() => arrayFish[index].Opacity = value);
        }

        public void UpdateInvertedImageForFish(int index, int type)
        {
            //Иногда, если во время игры и при наличии убитых рыбок нажать на крестик (кнопку закрытия, что в правом верхнем углу)
            //вылетает ошибка System.Threading.Tasks.TaskCanceledException: "Отменена задача." именно вот в этом месте. Для решения
            //этой проблемы сделан костыль в виде try с пустым cath. Приложение при этом завершается нормально.
            try
            {
                arrayFish[index].Dispatcher.Invoke(() => arrayFish[index].Source = chooseForInvertedImage(type));
            }
            catch { }
        }

        public void UpdateInvertedImageHitForFish(int index, int type)
        {
            arrayFish[index].Dispatcher.Invoke(() => arrayFish[index].Source = chooseForInvertedImageHit(type));
        }

        public void UpdateMaxScore(int value, string time)
        {
            object[] objArray1 = new object[] { "Рекорд: ", value, " за ", time };
            maxScore.Content = string.Concat(objArray1);
        }

        public void UpdateNotInvertedImageForFish(int index, int type)
        {
            //Иногда, если во время игры и при наличии убитых рыбок нажать на крестик (кнопку закрытия, что в правом верхнем углу)
            //вылетает ошибка System.Threading.Tasks.TaskCanceledException: "Отменена задача." именно вот в этом месте. Для решения
            //этой проблемы сделан костыль в виде try с пустым cath. Приложение при этом завершается нормально.
            try
            {
                arrayFish[index].Dispatcher.Invoke(() => arrayFish[index].Source = chooseForNotInvertedImage(type));
            }
            catch { }
        }

        public void UpdateNotInvertedImageHitForFish(int index, int type)
        {
            arrayFish[index].Dispatcher.Invoke(() => arrayFish[index].Source = chooseForNotInvertedImageHit(type));
        }

        public void UpdatePlayerRenderTransform(Transform transform)
        {
            player.RenderTransform = transform;
        }

        public void UpdateScore(int value)
        {
            score.Content = "Счёт: " + value;
        }

        public Button startAndStopButton { get; set; }

        public Button pauseAndResumeButton { get; set; }

        public Button forMuteButton { get; set; }

        public Button aboutTheGameButton { get; set; }

        public Label score { get; set; }

        public Label maxScore { get; set; }

        public Label gameTimerValue { get; set; }

        public Image[] arrayFish { get; set; }

        public Image boulder { get; set; }

        public Image player { get; set; }

        public Frame waterFrame { get; set; }

        public Grid mainGrid { get; set; }
    }
}
