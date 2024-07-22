using HAK.Gameplay.Shape;
using UnityEngine;
using HAK.Gameplay.LevelProgress;
using Grid = HAK.Gameplay.Grid.Grid;


namespace HAK.Core
{
    public class GameplayDependencyInjector : BaseDependencyInjector
    {
        [SerializeField] private Tray _tray;
        [SerializeField] private Grid _grid;
        [SerializeField] private LevelProgression _levelProgression;
        
        public override void InjectDependencies()
        {
            _tray.GridHandler = _grid;
            _grid.TrayHandler = _tray;
            _grid.LevelProgressionHandler = _levelProgression;
            _levelProgression.TrayHandler = _tray;
        }
    }
}