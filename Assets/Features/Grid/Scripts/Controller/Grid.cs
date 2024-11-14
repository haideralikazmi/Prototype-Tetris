using System.Collections.Generic;
using HAK.Gameplay.LevelProgress;
using HAK.Core;
using HAK.Gameplay.Shape;
using HAK.UI.Grid;
using UnityEngine;

namespace HAK.Gameplay.Grid
{
    public class Grid : BaseGameplayModule, IGrid
    {
        [SerializeField] private GridView _view;
        [SerializeField] private Tile _baseTile;
        [SerializeField] private Tile _switchTile;
        private int _gridWidth;
        private int _gridHeight;
        private float _row0ffset;
        private float _column0ffset;
        private Cell[,] _grid;
        private List<Cell> _highlightedCells;
        private List<Cell> _currentlyOccupiedCells;
        private Vector2Int _currentClosestCell;
        private List<Vector2Int> _switchesOnGrid;
        private LevelGenerationData _levelData;

        
        public ITray TrayHandler { private get; set; }
        public ILevelProgression LevelProgressionHandler { private get; set; }

        public override void PreInitialize()
        {
            var currentLevel = PlayerPrefs.GetInt(Constants.LevelPrefKeys.CurrentLevel, 0);
            _levelData = Configs.LevelConfig.LevelData[currentLevel];
        }
        
        public override void Initialize()
        {
            base.Initialize();
            SetData();
            InitializeView();
            GenerateGrid();
            ActivateSwitchesOnGrid();
        }
        
        private void SetData()
        {
            _highlightedCells = new List<Cell>();
            _currentClosestCell = new Vector2Int();
            _gridWidth = Configs.LevelConfig.DefaultGridWidth;
            _gridHeight = Configs.LevelConfig.DefaultGridHeight;
            _row0ffset = _baseTile.width;
            _column0ffset = _baseTile.height;
            _switchesOnGrid = _levelData.SwitchesOnGrid;
        }
        
        public void GenerateGrid()
        {
            _grid = new Cell[_gridWidth, _gridHeight];
            
            for (var rowIndex = _gridWidth-1; rowIndex >=0 ; rowIndex--)
            {
                for (var columnIndex = _gridHeight-1; columnIndex >=0 ; columnIndex--)
                {
                    var xPos = rowIndex * _row0ffset;
                    var zPos = columnIndex * _column0ffset;
                    
                    var position = new Vector3(xPos, 0, zPos);
                    _grid[rowIndex, columnIndex] = CreateNewCell(rowIndex, columnIndex, position);
                }
            }
        }
        
        private Cell CreateNewCell(int rowIndex, int columnIndex, Vector3 position)
        {
            var cell = new Cell();
            var tile = _view.SpawnTile(position, TileType.DefaultTile);
            cell.Initialize(tile, new Vector2Int(rowIndex, columnIndex));
            return cell;
        }

        private void ActivateSwitchesOnGrid()
        {
            for (var index = 0; index < _switchesOnGrid.Count; index++)
            {
                var switchIndex = _switchesOnGrid[index];
                var cell = _grid[switchIndex.x, switchIndex.y];
                var position = cell.GetCellPosition();
                var tile = _view.SpawnTile(position, TileType.SwitchTile);
                cell.ActivateSwitch(tile);
            }
        }
        
        private void InitializeView()
        {
            _view.Initialize(new GridViewDataModel()
            {
                GridWidth = _gridWidth,
                GridHeight = _gridHeight,
                CellOffset = _column0ffset,
                DefaultTile = _baseTile,
                SwitchTile = _switchTile
            });
        }
        
        private bool IsWithinBoundsOfGrid(Vector2 position)
        {
            var isWithInBoundsOfGrid = _view.IsWithInBoundsOfGrid(position);
            return isWithInBoundsOfGrid;
        }

        void IGrid.IsWithinBoundsOfGrid(Vector3 shapePosition, Vector3 plugPosition)
        {
            var isWithInBoundsOfGrid = IsWithinBoundsOfGrid(shapePosition);
            RemoveHighlightFromPreviousCells();
            if (isWithInBoundsOfGrid)
            {
                HighlightShape(plugPosition);
            }
        }

        void IGrid.OnTrayRelease(BaseShape shape)
        {
            var plugPosition = shape.GetPlugPosition();
            var isPlugWithinbounds = IsWithinBoundsOfGrid(plugPosition);
            var tileCount = shape.GetTileIndex().Count;
            if (isPlugWithinbounds && CanPlaceShape(tileCount))
            {
                var cell = _grid[_currentClosestCell.x, _currentClosestCell.y];
                shape.PlaceShapeOnCell(cell.GetCellPosition());
                shape.SetPlacementState(true);
                shape.SetPlugState(false);
                shape.SetPlacementPoint(_currentClosestCell);
                SetOccupationStateOfCells(true);
                RemoveHighlightFromPreviousCells();
                return;
            }
            TrayHandler.MoveShapeToOriginalPosition();
        }
        
        public void OnReselectionOfShape(List<Vector2Int> shapeTiles)
        {
            for (var i = 0; i < shapeTiles.Count; i++) 
            {
                var tileIndex = shapeTiles[i];
                _grid[tileIndex.x,tileIndex.y].SetOccupationState(false);
            }
        }

        Vector3 IGrid.GetPositionAtIndex(Vector2Int index)
        {
            var cell = _grid[index.x, index.y];
            var position = cell.GetCellPosition();
            return position;
        }

        private bool CanPlaceShape(int shapeTiles)
        {
            return _highlightedCells.Count == shapeTiles;
        }

        private void SetOccupationStateOfCells(bool state)
        {
            for (var i = 0; i < _highlightedCells.Count; i++)
            {
                _highlightedCells[i].SetOccupationState(state);
            }
        }

        private void HighlightShape(Vector3 plugPosition)
        {
            var closestCell = GetClosestCell(plugPosition);
            if (!CheckIfCellHasActiveSwitch(closestCell)) { return;}
            
            var shapeTiles = TrayHandler.GetShapeTileIndices();
            _currentClosestCell = closestCell;
            
            for (var i=0; i< shapeTiles.Count ; i++)
            {
                var index = shapeTiles[i] + closestCell;
                if (index.x >= _gridWidth || index.y >= _gridHeight || index.x < 0 || index.y < 0)
                {
                    _highlightedCells = new List<Cell>(); 
                    return;
                }
                var cell = _grid[index.x, index.y];
                if (cell.IsCellOccupied())
                {
                    _highlightedCells = new List<Cell>(); 
                    return;
                }
                _highlightedCells.Add(cell);
            }
            HighlightCells();
        }

        private bool CheckIfCellHasActiveSwitch(Vector2Int index)
        {
            var cell = _grid[index.x, index.y];
            return cell.HasActiveSwitch();
        }
        
        
        private bool CheckIfCellIsActive(Vector2Int index)
        {
            var cell = _grid[index.x, index.y];
            return cell.IsCellActive();
        }
        
        private void HighlightCells()
        {
            for (var i = 0; i < _highlightedCells.Count; i++)
            {
                _highlightedCells[i].HighlightTile();
            }
        }

        private void RemoveHighlightFromPreviousCells()
        {
            if (_highlightedCells.Count == 0) {return;}
            
            for (var i = 0; i < _highlightedCells.Count; i++)
            {
                _highlightedCells[i].RemoveHighlightTile();
            }
            _highlightedCells = new List<Cell>();
        }
        
        private Vector2Int GetClosestCell(Vector3 plugPosition)
        {
            var minDistance = Mathf.Infinity;
            var closestIndex = new Vector2Int(-1, -1);

            for (int xIndex = 0; xIndex < _gridWidth; xIndex++)
            {
                for (int yIndex = 0; yIndex < _gridHeight; yIndex++)
                {
                    var cell = _grid[xIndex, yIndex];
                    var cellPosition = cell.GetCellPosition();
                    var distance = Vector3.Distance(cellPosition, plugPosition);

                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        closestIndex.x = xIndex;
                        closestIndex.y = yIndex;
                    }
                }
            }
            return closestIndex;
        }
    }
}