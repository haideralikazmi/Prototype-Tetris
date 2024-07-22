using System.Collections.Generic;
using HAK.Gameplay.Grid;
using UnityEngine;
using HAK.Core;

namespace HAK.Gameplay.Shape
{
    public class Tray : BaseGameplayModule, ITray
    {
        [SerializeField] private TrayView _view;
        private List<BaseShape> _shapeList;
        
        public IGrid GridHandler { private get; set; }
        
        public override void Initialize()
        {
            SetData();
            InitializeView();
        }
        
        public override void PostInitialize()
        {
            _view.Show();
        }

        private void SetData()
        {
            var currentLevel = PlayerPrefs.GetInt(Constants.LevelPrefKeys.CurrentLevel, Configs.LevelConfig.DefaultLevel);
            _shapeList = Configs.LevelConfig.LevelData[currentLevel].ShapeTypes;
        }

        private void InitializeView()
        {
            _view.Initialize(new TrayViewDataModel
            {
                TrayHandler = this,
                ShapeTypes = _shapeList
            });
        }
        
        void ITray.OnTrayReleased(BaseShape shape)
        {
            GridHandler.OnTrayRelease(shape);
        }

        void ITray.OnInputDrag(Vector3 position, Vector3 plugPosition)
        {
            GridHandler.IsWithinBoundsOfGrid(position, plugPosition);
        }

        void ITray.OnReselectionOfShape(List<Vector2Int> shapeTiles, Vector2Int placementPoint)
        {
            var cellsOnGrid = new List<Vector2Int>();
            for (var i = 0; i < shapeTiles.Count; i++)
            {
                var index = shapeTiles[i] + placementPoint;
                cellsOnGrid.Add(index);
            }
            GridHandler.OnReselectionOfShape(cellsOnGrid);
        }
        
        void ITray.MoveShapeToOriginalPosition()
        {
            _view.ReturnShapeToOriginalPosition();
        }

        public List<Vector2Int> GetShapeTileIndices()
        {
            return _view.GetTilesIndicesOfShape();
        }

        public int GetShapeCount()
        {
            return _shapeList.Count;
        }

        public bool CheckIfAllShapesHaveBeenPlaced()
        {
            return _view.HaveAllShapesBeenPlaced();
        }
    }
}