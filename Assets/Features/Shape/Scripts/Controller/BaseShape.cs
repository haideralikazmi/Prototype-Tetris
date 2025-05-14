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
        [SerializeField] private Transform _plugPivotTransform;
        private Vector3 _plugToShapeOffset;
        private Vector3 _defaultPosition;
        private Vector3 _plugDefaultPosition;

        private bool _hasBeenPlaced;
        private Vector2Int _placementPoint;
        
        public virtual void Initialize(Vector3 defaultPosition)
        {
            _defaultPosition = defaultPosition;
            _plugDefaultPosition = _plugTransform.localPosition;
            _placementPoint = new Vector2Int();
            ZoomOutScale();
            CalculatePlugToShapeOffset();
        }

        private void CalculatePlugToShapeOffset()
        {
            var pivotlocalPosition = _plugPivotTransform.localPosition; 
            _plugToShapeOffset = _chargerTransform.TransformVector(pivotlocalPosition);
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
            var scale = Configs.ViewConfigs.ShapeZoomInScale;
            var duration = Configs.ViewConfigs.ShapeZoomDuration;
            _chargerTransform.DOScale(scale, duration);
        }
        
        private void ZoomOutScale()
        {
            var scale = Configs.ViewConfigs.ShapeZoomOutScale;
            var duration = Configs.ViewConfigs.ShapeZoomDuration;
            _chargerTransform.DOScale(scale, duration);
        }
        
        public void SetShapePosition(Vector3 targetPosition)
        {
            var zOffset = Configs.ViewConfigs.ZOffsetonShapePickup;
            targetPosition.z += zOffset;
            _chargerTransform.position = targetPosition;
            PlugOutSequence();
        }
        
        public void PlaceShapeOnCell(Vector3 cellPosition)
        {
            var placementDuration = Configs.ViewConfigs.ShapePlacementDuration;
            var placementEase = Configs.ViewConfigs.ShapePlacementAnimationCurve;
            var adjustedPosition = cellPosition - _plugToShapeOffset;
            adjustedPosition.y += Configs.ViewConfigs.ShapePlacementYOffset;

            var placementSequence = DOTween.Sequence();
            placementSequence.AppendCallback(PlugInSequence);
            placementSequence.Join(_chargerTransform.DOMove(adjustedPosition, placementDuration).SetEase(placementEase));
            placementSequence.Play();
        }

        public void ReturnToOriginalPosition()
        {
            var returnDuration = Configs.ViewConfigs.ShapeReturnToTrayDuration;
            _chargerTransform.DOMove(_defaultPosition,returnDuration).SetEase(Ease.InOutQuad);
            ZoomOutScale();
        }

        private void PlugInSequence()
        {
            var pluginSequence = DOTween.Sequence();
            var endPosition = _plugPivotTransform.localPosition;
            var duration = Configs.GameConfigs.PlugInMovementDuration;
            var ease = Configs.GameConfigs.PlugInAnimationCurve;
            pluginSequence.Append(_plugTransform.DOLocalMove(endPosition, duration).SetEase(ease));
            pluginSequence.Play();
        }

        private void PlugOutSequence()
        {
            var plugOutSequence = DOTween.Sequence();
            var endPosition = _plugDefaultPosition;
            var duration = Configs.GameConfigs.PlugOutMovementDuration;
            var ease = Configs.GameConfigs.PlugOutAnimationCurve;
            plugOutSequence.Append(_plugTransform.DOLocalMove(endPosition, duration).SetEase(ease));
            plugOutSequence.Play();
        }
    }
}