using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBoard
{
    public class GameTile : MonoBehaviour
    {
        [SerializeField] private Transform arrow;

        private GameTile _west, _east, _north, _south, _nextOnPath;

        private int _distance;

        public bool HasPath => _distance != int.MaxValue;
        public bool IsAlternative { get; set; }
        
        private Quaternion _northRotation = Quaternion.Euler(90, 0, 0);
        private Quaternion _eastRotation = Quaternion.Euler(90, 90, 0);
        private Quaternion _southRotation = Quaternion.Euler(90, 180, 0);
        private Quaternion _westRotation = Quaternion.Euler(90, 270, 0);

        public static void SetEastWestNeighbors(GameTile east, GameTile west)
        {
            west._east = east;
            east._west = west;
        }

        public static void SetNorthSouthNeighbors(GameTile north, GameTile south)
        {
            north._south = south;
            south._north = north;
        }

        public void ClearPath()
        {
            _distance = int.MaxValue;
            _nextOnPath = null;
        }

        public void BecomeDestination()
        {
            _distance = 0;
            _nextOnPath = null;
        }

        private GameTile GrowPathTo(GameTile neighbor)
        {
            if (!HasPath || neighbor == null || neighbor.HasPath)
            {
                return null;
            }

            neighbor._distance = _distance + 1;
            neighbor._nextOnPath = this;
            return neighbor;
        }

        public GameTile GrowPathNorth() => GrowPathTo(_north);
        public GameTile GrowPathEast() => GrowPathTo(_east);
        public GameTile GrowPathWest() => GrowPathTo(_west);
        public GameTile GrowPathSouth() => GrowPathTo(_south);

        public void ShowPath()
        {
            if (_distance == 0)
            {
                arrow.gameObject.SetActive(false);
                return;
            }
            arrow.gameObject.SetActive(true);
            arrow.localRotation =
                _nextOnPath == _north ? _northRotation :
                _nextOnPath == _east ? _eastRotation :
                _nextOnPath == _south ? _southRotation :
                _westRotation;
        }
    }
}
