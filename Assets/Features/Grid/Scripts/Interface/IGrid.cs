using System;
using System.Collections.Generic;
using HAK.Gameplay.Shape;
using UnityEngine;

namespace HAK.Gameplay.Grid
{
    public interface IGrid
    {
        void IsWithinBoundsOfGrid(Vector3 position, Vector3 plugPosition);
        void OnTrayRelease(BaseShape shape, Action<bool> placement);
        void OnReselectionOfShape(List<Vector2Int> shapeTiles);
        Vector3 GetPositionAtIndex(Vector2Int index);
    }
}