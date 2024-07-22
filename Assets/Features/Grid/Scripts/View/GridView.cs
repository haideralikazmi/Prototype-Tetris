using HAK.Core;
using HAK.Gameplay.Grid;
using UnityEngine;

namespace HAK.UI.Grid
{
    public class GridView : BaseView
    {
        [SerializeField] private GridViewRefs _refs;
        private Vector2 _startingPosition;
        private GridViewDataModel _model;
        
        public override void Initialize(object model)
        {
            _model = model as GridViewDataModel;
            CalculateStartingPosition();
        }
        
        private void CalculateStartingPosition()
        {
            var gridWidthPixels = _model.GridWidth * _model.CellOffset;
            var gridHeightPixels = _model.GridHeight * _model.CellOffset;
            var heightFactor = Configs.GameConfig.HeightDividingFactor;
            var widthFactor = Configs.GameConfig.WidthDividingFactor;
            
            _startingPosition = new Vector2(Screen.width / widthFactor - gridWidthPixels / widthFactor, Screen.height / heightFactor - gridHeightPixels / heightFactor);
        }


        public Tile SpawnTile(Vector3 position, TileType type)
        {
            var tileToSpawn = new Tile();
            switch (type)
            {
                case TileType.DefaultTile:
                    tileToSpawn = _model.DefaultTile;
                    break;
                case TileType.SwitchTile:
                    tileToSpawn = _model.SwitchTile;
                    break;
                default:
                    tileToSpawn = _model.DefaultTile;
                    break;
            }
            var tile = Instantiate(tileToSpawn, position, Quaternion.identity, _refs.GridTransform);
            tile.Initialize();
            return tile;
        }

        public bool IsWithInBoundsOfGrid(Vector2 position)
        {
            var isWithinBounds = true;
            return isWithinBounds;
        }
    }
}