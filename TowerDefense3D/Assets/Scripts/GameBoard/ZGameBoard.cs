using UnityEngine;

namespace GameBoard
{
    public class ZGameBoard: MonoBehaviour
    {
        [SerializeField] private Transform ground;
        
        private Vector2Int _size;

        public void Initialize(Vector2Int size)
        {
            _size = size;
            ground.localScale = new Vector3(size.x, size.y, 1);
        }
    }
}
