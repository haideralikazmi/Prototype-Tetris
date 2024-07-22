using System.Collections.Generic;
using HAK.Core;
using HAK.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace HAK.Gameplay.Shape
{
    public class TrayView: BaseView, IPointerDownHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private TrayViewRefs _viewRefs;
        private ITray _handler;
        private List<BaseShape> _shapes;
        private TrayViewDataModel _dataModel;
        private BaseShape _currentlySelecedShape;
        private List<Vector3> _spawnPositions;
        private Vector3 _previousTouchPosition;
        private bool _isDragging;
        
        public override void Initialize(object dataModel)
        {
            base.Initialize(dataModel);
            _dataModel = dataModel as TrayViewDataModel;
            _shapes = new List<BaseShape>();
            _handler = _dataModel.TrayHandler;
            SetSpawnPoints();
        }

        private void SetSpawnPoints()
        {
            _spawnPositions = new List<Vector3>();
            var spawnTransforms = _viewRefs.SpawnTransforms;
            var pointCount = spawnTransforms.Count;
            for (var index = 0; index < pointCount; index++)
            {
                _spawnPositions.Add(spawnTransforms[index].position);
            }
        }
        
        public override void Show()
        {
            base.Show();
            SpawnShapes();
        }

        public void SpawnShapes()
        {
            for (var index = 0; index < _dataModel.ShapeTypes.Count; index++)
            {
                var position = _spawnPositions[index];
                var shape = Instantiate(_dataModel.ShapeTypes[index], position, Quaternion.identity, _viewRefs.SpawnParent);
                shape.Initialize(position);
                _shapes.Add(shape);
            }
        }

        private void OnTrayReleased()
        {
            _handler.OnTrayReleased(_currentlySelecedShape);
            _currentlySelecedShape = null;
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            _currentlySelecedShape = SelectShape(eventData.position);
            if (_currentlySelecedShape != null)
            {
                _currentlySelecedShape.SetSelectedState();
                var placementStatus = _currentlySelecedShape.HasBeenPlaced();
                if (placementStatus)
                {
                    var shapeTiles = _currentlySelecedShape.GetTileIndex();
                    var anchorPoint = _currentlySelecedShape.GetPlacementPoint();
                    _handler.OnReselectionOfShape(shapeTiles, anchorPoint);
                    _currentlySelecedShape.SetPlacementState(false);
                }
            }
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            if (_currentlySelecedShape != null)
            {
                var shapePosition = eventData.position;
                var plugPosition = _currentlySelecedShape.GetPlugPosition();
                PickUpShape(shapePosition);
                _handler.OnInputDrag(shapePosition, plugPosition);
            }
        }
        
        public void OnEndDrag(PointerEventData eventData)
        {
            _isDragging = false;
            if(_currentlySelecedShape!=null){  OnTrayReleased();}
        }
        
        private BaseShape SelectShape(Vector2 touchPosition)
        {
            var ray = Camera.main.ScreenPointToRay(touchPosition);
            var  rayCastHit = new RaycastHit();
        
            if (Physics.Raycast(ray, out rayCastHit))
            {
                var selectedObject = rayCastHit.collider.gameObject;
                var shape = selectedObject.GetComponent<BaseShape>();
                _previousTouchPosition = touchPosition;
                return shape;
            }
            return null;
        }
        
        private void PickUpShape(Vector2 touchPosition)
        {
            if (_currentlySelecedShape == null) return; 
        
            var dragSpeed = Configs.ViewConfig.ShapeDragSpeed;
            var yOffset = Configs.ViewConfig.YOffsetonShapePickup;
            
            var ray = Camera.main.ScreenPointToRay(touchPosition);
            var plane = new Plane(Vector3.up, new Vector3(0, _currentlySelecedShape.transform.position.y, 0));
        
            if (plane.Raycast(ray, out float enter))
            { 
                Vector3 hitPoint = ray.GetPoint(enter);
                if (!_isDragging)
                {
                    hitPoint.y =  yOffset;
                    _isDragging = true;
                }
                _currentlySelecedShape.SetShapePosition(hitPoint,dragSpeed);
            }
            _previousTouchPosition = touchPosition;
        }
        
        public List<Vector2Int> GetTilesIndicesOfShape()
        {
            return _currentlySelecedShape.GetTileIndex();
        }

        public bool HaveAllShapesBeenPlaced()
        {
            var allShapesHaveBeenPlaced = true;
            foreach (var shape in _shapes)
            {
                if (!shape.HasBeenPlaced())
                {
                    allShapesHaveBeenPlaced = false;
                }
            }
            return allShapesHaveBeenPlaced;
        }

        public void ReturnShapeToOriginalPosition()
        {
            _currentlySelecedShape.ReturnToOriginalPosition();
        }
    }
}