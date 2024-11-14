using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using HAK.Core;

namespace HAK.Gameplay.Shape
{
    public class BaseShape : MonoBehaviour
    {
        [SerializeField] protected List<Vector2Int> _tilesIndices;
        [SerializeField] private Transform _chargerTransform;
        [SerializeField] private Transform _plugTransform;
        private Vector3 _defaultPosition;

        private bool _hasBeenPlaced;
        private Vector2Int _placementPoint;
        
        public virtual void Initialize(Vector3 defaultPosition)
        {
            _defaultPosition = defaultPosition;
            _placementPoint = new Vector2Int();
            ZoomOutScale();
        }

        public List<Vector2Int> GetTileIndex()
        {
            return _tilesIndices;
        }
        
        public Vector3 GetPlugPosition()
        {
            return _plugTransform.position;
        }

        public bool HasBeenPlaced()
        {
            return _hasBeenPlaced;
        }

        public Vector2Int GetPlacementPoint()
        {
            return _placementPoint;
        }
        
        public void SetPlacementPoint(Vector2Int placementPoint)
        {
            _placementPoint = placementPoint;
        }

        public void SetPlacementState(bool state)
        {
            _hasBeenPlaced = state;
        }

        public void SetPlugState(bool state)
        {
            _plugTransform.gameObject.SetActive(state);
        }

        public void SetSelectedState()
        {
            SetPlugState(true);
            ZoomInScale();
        }

        private void ZoomInScale()
        {
            var scale = Configs.ViewConfig.ShapeZoomInScale;
            var duration = Configs.ViewConfig.ShapeZoomDuration;
            _chargerTransform.DOScale(scale, duration);
        }
        
        private void ZoomOutScale()
        {
            var scale = Configs.ViewConfig.ShapeZoomOutScale;
            var duration = Configs.ViewConfig.ShapeZoomDuration;
            _chargerTransform.DOScale(scale, duration);
        }
        
        public void SetShapePosition(Vector3 targetPosition, float movementSpeed)
        {
            var zOffset = Configs.ViewConfig.ZOffsetonShapePickup;
            targetPosition.z += zOffset;
            _chargerTransform.position = Vector3.Lerp(_chargerTransform.position, targetPosition, movementSpeed);
        }

        public void PlaceShapeOnCell(Vector3 cellPosition)
        {
            var placementDuration = Configs.ViewConfig.ShapePlacementDuration;
            var placementEase = Configs.ViewConfig.ShapePlacementAnimationCurve;
            cellPosition.y += Configs.ViewConfig.ShapePlacementYOffset;
            _chargerTransform.DOMove(cellPosition,placementDuration).SetEase(placementEase);
        }
        

        public void ReturnToOriginalPosition()
        {
            var returnDuration = Configs.ViewConfig.ShapeReturnToTrayDuration;
            _chargerTransform.DOMove(_defaultPosition,returnDuration).SetEase(Ease.InOutQuad);
            ZoomOutScale();
        }
    }
}