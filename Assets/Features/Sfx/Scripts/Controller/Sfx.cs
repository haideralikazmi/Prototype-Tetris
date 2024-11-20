using HAK.Gameplay;
using UnityEngine;

namespace HAK.Core.SpecialEffects
{
    public partial class Sfx : BaseGameplayModule, ISfx
    {
        [SerializeField] protected AudioSource _bgAudioSource;
        [SerializeField] private AudioSource _gameplayAudioSource;
        private HapticsController _hapticsController;

        public override void Initialize()
        {
            InitializeHaptics();
            PlayAmbiance();
        }

        private void InitializeHaptics()
        {
            _hapticsController = new HapticsController();
            _hapticsController.InitializeVibrations();
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
            _gameplayAudioSource.volume = Configs.GameConfigs.AmbianceMusicVolume;
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
            _hapticsController.PlaySoftImpact();
        }

        void ISfx.OnShapePlacementOnBoard()
        {
            var audioClip = Constants.AudioClip.SwitchPlugInSound;
            PlayAudioClip(audioClip);
            _hapticsController.PlayRigidImpact();
        }
    }
}