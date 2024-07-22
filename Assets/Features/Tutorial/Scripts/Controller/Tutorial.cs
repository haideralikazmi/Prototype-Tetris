using UnityEngine;
using HAK.Gameplay;
using HAK.Gameplay.Grid;

namespace HAK.Core.Tutorial
{
    public class Tutorial : BaseGameplayModule, ITutorial
    {
        [SerializeField] private TutorialView _view;
        private int _currentLevel;
        private LevelGenerationData _levelData;
        private Vector3 _firstCellPosition;
        private Vector3 _secondCellPosition;
        public IGrid GridHandler;
        
        public override void PreInitialize()
        {
            _currentLevel = PlayerPrefs.GetInt(Constants.LevelPrefKeys.CurrentLevel, 1);
            _levelData = Configs.LevelConfig.LevelData[_currentLevel];
        }

        public override void Initialize()
        {
            if (_currentLevel > 1) {return; }

            _firstCellPosition = GridHandler.GetPositionAtIndex(_levelData.SwitchesOnGrid[0]);
            _secondCellPosition = GridHandler.GetPositionAtIndex(_levelData.SwitchesOnGrid[1]);
            _view.Initialize(this);
        }
        
        public override void PostInitialize()
        {
            _view.Show();
        }


        Vector3 ITutorial.GetPositionOfFirstCell()
        {
            var position = ConvertPositionToScreenSpace(_firstCellPosition);
            return position;
        }

        Vector3 ITutorial.GetPositionOfSecondCell()
        {
            var position = ConvertPositionToScreenSpace(_secondCellPosition);
            return position;
        }

        Vector3 ConvertPositionToScreenSpace(Vector3 position)
        {
            var screenPosition = Camera.main.WorldToScreenPoint(position);
            return screenPosition;
        }
    }
}