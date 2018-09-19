using System;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Hunting_for_fish
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Controller controller;
        private View view;
        
        public MainWindow()
        {
            InitializeComponent();
            view = new View();
            controller = new Controller(view);            
            Image[] imageArray = new Image[] { FishType1_1, FishType1_2, FishType1_3, FishType1_4, FishType2_1,
                FishType2_2, FishType2_3, FishType3_1, FishType3_2, FishType3_3 };
            for (int i = 0; i < imageArray.Length; i++)
            {
                imageArray[i].Opacity = 0.0;
            }
            BoulderType1.Opacity = 0.0;
            view.arrayFish = imageArray;
            view.boulder = BoulderType1;
            view.player = Player;
            view.startAndStopButton = StartAndStopButton;
            view.pauseAndResumeButton = PauseAndResumeButton;
            view.forMuteButton = ForMuteButton;
            view.aboutTheGameButton = AboutTheGameButton;
            view.score = ScoreValue;
            view.maxScore = MaxScore;
            view.gameTimerValue = GameTimerValue;
            view.waterFrame = WaterFrame;
            view.mainGrid = MainGrid;
            double actualHeight = this.ActualHeight;
            controller.Inizialize((int)this.Height, (int)this.Width);
            double height = Player.Height;
            Observer observer = new Observer();
            ISubject[] allMovementFish = controller.GetAllMovementFish();
            for (int j = 0; j < allMovementFish.Length; j++)
            {
                observer.AddSubject(allMovementFish[j]);
            }
            allMovementFish = controller.GetAllFish();
            for (int k = 0; k < allMovementFish.Length; k++)
            {
                observer.AddSubject(allMovementFish[k]);
            }
            observer.AddSubject(controller.GetMovementBoulder());
            observer.AddSubject(controller.GetScore());
            observer.AddSubject(controller.GetGameTimer());
            observer.AddObserver(view);
            this.Closing += MainWindow_Closing;
        }

        private void AboutTheGameButton_Click(object sender, RoutedEventArgs e)
        {
            controller.AboutTheGame();
        }

        private void ForMuteButton_Click(object sender, RoutedEventArgs e)
        {
            controller.ForMute();
        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            controller.PauseAndResume();
        }

        private void StartAndStopButton_Click(object sender, RoutedEventArgs e)
        {
            controller.StartAndStop();
        }

        private void WaterFrameOrFish_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Point position = Mouse.GetPosition(this);
            controller.Throw(position);
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            Thread thread = new Thread(() =>
            {
                File.Delete("background.mp3");
                File.Delete("hit.mp3");
                File.Delete("miss.mp3");
            });
            thread.Start();
        }
    }
}