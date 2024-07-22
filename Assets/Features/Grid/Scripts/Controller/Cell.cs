using UnityEngine;

namespace HAK.Gameplay.Grid
{
    public class Cell
    {
        private Tile _currentTile;
        private bool _isOccupied;
        private Vector2Int _index;
        private bool _activeSwitch = false;
        private bool _isCellActive;
        
        public void Initialize(Tile _defaultTile, Vector2Int index)
        {
            AddDefaultTile(_defaultTile);
            _isCellActive = true;
            SetIndex(index);
        }

        private void SetIndex(Vector2Int index)
        {
            _index = index;
        }
        
        private void AddDefaultTile(Tile tile)
        {
            _currentTile = tile;
        }

        public void HighlightTile()
        {
            _currentTile.HighlightTile();
        }
        
        public void RemoveHighlightTile()
        {
            _currentTile.RemoveHighlight();
        }

        public Vector3 GetCellPosition()
        {
            return _currentTile.GetPosition();
        }
        
        public bool IsCellOccupied()
        {
            return _isOccupied;
        }
        
        public void SetOccupationState(bool state)
        {
            _isOccupied = state;
        }

        public void ActivateSwitch(Tile switchTile)
        {
            var tile = _currentTile;
            tile.DeactivateTile();
            _currentTile = switchTile;
            _activeSwitch = true;
        }

        public void DeactivateCell()
        {
            SetCellState(false);
            _currentTile.DeactivateTile();
        }

        public bool HasActiveSwitch()
        {
            return _activeSwitch;
        }

        public bool IsCellActive()
        {
            return _isCellActive;
        }

        private void SetCellState(bool state)
        {
            _isCellActive = state;
        }
    }
}