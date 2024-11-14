using System.Collections;
using DG.Tweening;
using HAK.Gameplay.LevelProgress;
using HAK.Core;
using HAK.UI.LevelProgression;
using UnityEngine;
using UnityEngine.UI;


namespace HAK.UI.LevelProgress
{
    public class LevelProgressionView : BaseView
    {
        [SerializeField] private LevelProgressionViewRefs _viewRefs;

        private ILevelProgression _handler;
        
        private float _fillValue;
        private float _chargingAnimationDuration;
        private Image _batteryFillImage;
        private Battery _currentBattery;
        
        public override void Initialize(object model=null)
        {
            base.Initialize(model);
            _handler = model as ILevelProgression;
            _chargingAnimationDuration = Configs.ViewConfig.ChargingAnimationDuration;
        }

        private int _currentLevel;

        public override void Register()
        {
            _viewRefs.OkayButton.onClick.AddListener(OnOkayButtonClick);
            _viewRefs.ReloadSceneButton.onClick.AddListener(OnReloadButtonClick);
        }

        private void OnOkayButtonClick()
        {
            _handler.OnNextButtonClicked();
        }
        
        private void OnReloadButtonClick()
        {
            _handler.OnReloadButtonClick();
        }

        public void ShowLevelCompleteScreen()
        {
            _viewRefs.InGameScreen.SetActive(false);
            _viewRefs.CompletionScreen.SetActive(true);
        }

        public void HideLevelCompleteScreen()
        {
            _viewRefs.CompletionScreen.SetActive(false);
            Unregister();
        }

    }
}