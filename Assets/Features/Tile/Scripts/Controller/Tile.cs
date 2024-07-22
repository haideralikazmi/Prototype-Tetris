using UnityEngine;

namespace HAK.Gameplay.Grid
{
    public class Tile: MonoBehaviour
    {
        [SerializeField] private Transform _modelTransform;
        [SerializeField] private Vector2Int _index;
        [SerializeField] private GameObject _highlight;
        [SerializeField] private GameObject _model;
        public float height => _modelTransform.localScale.y;
        public float width => _modelTransform.localScale.z;
        
        public void Initialize()
        {
            Register();
        }

        public Vector2Int GetTileIndex()
        {
            return _index;
        }
        
        private void Register()
        {
           
        }

        public void HighlightTile()
        {
            _highlight.SetActive(true);
        }

        public void RemoveHighlight()
        {
            _highlight.SetActive(false);
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }

        public void DeactivateTile()
        {
            _model.SetActive(false);
        }
    }
}