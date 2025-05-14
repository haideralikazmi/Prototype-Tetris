using System.Collections.Generic;
using HAK.Gameplay.Grid;
using UnityEngine;
using HAK.Core;
using HAK.Core.SpecialEffects;
using HAK.Gameplay.LevelProgress;

namespace HAK.Gameplay.Shape
{
    public class Tray : BaseGameplayModule, ITray
    {
        [SerializeField] private TrayView _view;
        private List<BaseShape> _shapeList;
        
        public IGrid GridHandler { private get; set; }
        public IBaseCamera CameraHandler { private get; set; }
        public ISfx SfxHandler { private get; set; }
        public ILevelProgression LevelProgressionHandler { private get; set; }
        
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
            var currentLevel = LevelProgressionHandler.GetCurrentLevel();
            _shapeList = Configs.LevelConfigs.LevelData[currentLevel].ShapeTypes;
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
            GridHandler.OnTrayRelease(shape, (placement) =>
            {
                if (placement)
                {
                    SfxHandler.OnShapePlacementOnBoard();
                }
            });
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
            SfxHandler.OnShapePickUpFromBoard();
        }
        
        void ITray.MoveShapeToOriginalPosition()
        {
            _view.ReturnShapeToOriginalPosition();
        }

        Camera ITray.GetCamera()
        {
            return CameraHandler.GetMainCamera();
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