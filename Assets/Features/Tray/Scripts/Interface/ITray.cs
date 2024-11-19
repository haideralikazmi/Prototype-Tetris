using System.Collections.Generic;
using UnityEngine;

namespace HAK.Gameplay.Shape
{
    public interface ITray
    {
        void OnTrayReleased(BaseShape shape);
        void OnInputDrag(Vector3 position, Vector3 plugPosition);
        void OnReselectionOfShape(List<Vector2Int> shapeTiles, Vector2Int placementPoint);
        List<Vector2Int> GetShapeTileIndices();
        int GetShapeCount();
        bool CheckIfAllShapesHaveBeenPlaced();
        void MoveShapeToOriginalPosition();
        Camera GetCamera();
    }
}