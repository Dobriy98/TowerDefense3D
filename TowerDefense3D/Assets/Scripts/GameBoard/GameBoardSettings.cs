using UnityEngine;

namespace GameBoard
{
    [CreateAssetMenu]
    public class GameBoardSettings : ScriptableObject
    {
        public ZGameBoard gameBoardPrefab;
        public Vector2Int size;

        public GameTile tilePrefab;
    }
}
