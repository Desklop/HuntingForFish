using System;
using System.Windows;
using System.Windows.Media;
using System.IO;

namespace Hunting_for_fish
{
    class ControlSounds
    {
        private MediaPlayer playerBackgroundMusic = new MediaPlayer();
        private MediaPlayer playerHitAndMissSound = new MediaPlayer();
        Uri backgroundSound;
        Uri hitSound;
        Uri missSound;
        private string backgroundSoundName = "background.mp3";
        private string hitSoundName = "hit.mp3";
        private string missSoundName = "miss.mp3";

        public ControlSounds()
        {
            Stream backgroundSoundStream = Application.GetResourceStream(new Uri("Sounds/" + backgroundSoundName, UriKind.Relative)).Stream;
            Stream backgroundSoundFileStream = File.OpenWrite(backgroundSoundName);
            backgroundSoundStream.CopyTo(backgroundSoundFileStream);
            backgroundSoundFileStream.Flush();
            backgroundSoundFileStream.Close();
            backgroundSoundStream.Close();
            backgroundSound = new Uri(backgroundSoundName, UriKind.Relative);

            Stream hitSoundStream = Application.GetResourceStream(new Uri("Sounds/" + hitSoundName, UriKind.Relative)).Stream;
            Stream hitSoundFileStream = File.OpenWrite(hitSoundName);
            hitSoundStream.CopyTo(hitSoundFileStream);
            hitSoundFileStream.Flush();
            hitSoundFileStream.Close();
            hitSoundStream.Close();
            hitSound = new Uri(hitSoundName, UriKind.Relative);

            Stream missSoundStream = Application.GetResourceStream(new Uri("Sounds/" + missSoundName, UriKind.Relative)).Stream;
            Stream missSoundFileStream = File.OpenWrite(missSoundName);
            missSoundStream.CopyTo(missSoundFileStream);
            missSoundFileStream.Flush();
            missSoundFileStream.Close();
            missSoundStream.Close();
            missSound = new Uri(missSoundName, UriKind.Relative);
        }

        public void Mute()
        {
            playerBackgroundMusic.IsMuted = false;
            playerHitAndMissSound.IsMuted = false;
        }

        public void PlayBackgroundMusic()
        {
            //Для воспроизведения музыки прямо из ресурса без необходимости распаковки и сохранения ресурса на диск
            //Не использую, т.к. нет возможности изменять громкость воспроизведения
            //System.Media.SoundPlayer SoundPlayer = new System.Media.SoundPlayer(Application.GetResourceStream(new Uri("Sounds/background.wav", UriKind.Relative)).Stream);
            //SoundPlayer.PlayLooping();

            playerBackgroundMusic.Open(backgroundSound);
            playerBackgroundMusic.MediaFailed += (s, e) => MessageBox.Show("Error media player!\n" + e);
            playerBackgroundMusic.MediaEnded += (s, e) => {
                playerBackgroundMusic.Stop();
                playerBackgroundMusic.Play();
            };
            playerBackgroundMusic.Play();  
            playerBackgroundMusic.Volume = 0.7;
        }

        public void PlayHitSound()
        {
            playerHitAndMissSound.Open(hitSound);
            playerHitAndMissSound.MediaFailed += (s, e) => MessageBox.Show("Error media player!\n" + e);
            playerHitAndMissSound.Play();
        }

        public void PlayMissSound()
        {
            playerHitAndMissSound.Open(missSound);
            playerHitAndMissSound.MediaFailed += (s, e) => MessageBox.Show("Error media player!\n" + e);
            playerHitAndMissSound.Play();
        }

        public void StopBackgroundMusic()
        {
            playerBackgroundMusic.Stop();
        }

        public void UnMute()
        {
            playerBackgroundMusic.IsMuted = true;
            playerHitAndMissSound.IsMuted = true;
        }
    }
}