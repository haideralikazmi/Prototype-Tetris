using HAK.Gameplay;
using UnityEngine;

namespace HAK.Core.SpecialEffects
{
    public partial class Sfx : BaseGameplayModule, ISfx
    {
        [SerializeField] protected AudioSource _bgAudioSource;
        [SerializeField] private AudioSource _gameplayAudioSource;

        public override void Initialize()
        {
            PlayAmbiance();
        }

        private AudioClip LoadAudioClip(string audioClipName)
        {
            var path = $"AudioClips/{audioClipName}";
            var audioClip = Resources.Load<AudioClip>(path);
            return audioClip;
        }

        protected void PlayAudioClip(string audioClip, float volume = 1)
        {
            var clip = LoadAudioClip(audioClip);
            _bgAudioSource.PlayOneShot(clip, volume);
        }
        
        private void PlayAmbiance()
        {
            var clip = Constants.AudioClip.AmbientMusic;
            var bgMusic = LoadAudioClip(clip);
            _gameplayAudioSource.loop = true;
            _gameplayAudioSource.clip = bgMusic;
            _gameplayAudioSource.volume = Configs.GameConfig.AmbianceMusicVolume;
            _gameplayAudioSource.Play();
        }

        void ISfx.PlayLevelFailSfx()
        {
            var audioClip = Constants.AudioClip.LevelFailSound;
            PlayAudioClip(audioClip);
        }
        
        void ISfx.LevelPassSfx()
        {
            var audioClip = Constants.AudioClip.LevelPassSound;
            PlayAudioClip(audioClip);
        }

        void ISfx.OnShapePickUpFromBoard()
        {
            var audioClip = Constants.AudioClip.SwitchPlugOutSound;
            PlayAudioClip(audioClip);
        }

        void ISfx.OnShapePlacementOnBoard()
        {
            var audioClip = Constants.AudioClip.SwitchPlugInSound;
            PlayAudioClip(audioClip);
        }
    }
}