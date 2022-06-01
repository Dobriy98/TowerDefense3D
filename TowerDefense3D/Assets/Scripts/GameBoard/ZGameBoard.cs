using System.Collections.Generic;
using UnityEngine;

namespace GameBoard
{
    public class ZGameBoard: MonoBehaviour
    {
        [SerializeField] private Transform ground;
        
        private Vector2Int _size;
        private GameTile[] _tiles;
        private Queue<GameTile> _searchFrontier = new Queue<GameTile>();

        public void Initialize(GameBoardSettings settings)
        {
            _size = settings.size;
            ground.localScale = new Vector3(_size.x, settings.size.y, 1);
            
            Vector2 offset = new Vector2((_size.x - 1) * 0.5f, (_size.y - 1) * 0.5f);
            _tiles = new GameTile[_size.x * _size.y];
            for (int i = 0, y = 0; y < _size.y; y++)
            {
                for (int x = 0; x < _size.x; x++, i++)
                {
                    GameTile tile = _tiles[i] = Instantiate(settings.tilePrefab);
                    tile.transform.SetParent(transform, false);
                    tile.transform.localPosition = new Vector3(x - offset.x, 0, y - offset.y);

                    if (x > 0)
                    {
                        GameTile.SetEastWestNeighbors(tile, _tiles[i - 1]);
                    }

                    if (y > 0)
                    {
                        GameTile.SetNorthSouthNeighbors(tile, _tiles[i-_size.x]);
                    }

                    tile.IsAlternative = (x & 1) == 0;
                    if ((y & 1) == 0)
                    {
                        tile.IsAlternative = !tile.IsAlternative;
                    }
                }
            }
            FindPath();
        }

        private void FindPath()
        {
            foreach (var tile in _tiles)
            {
                tile.ClearPath();
            }

            int destinationIndex = _tiles.Length / 2;
            _tiles[destinationIndex].BecomeDestination();
            _searchFrontier.Enqueue(_tiles[destinationIndex]);
            while (_searchFrontier.Count > 0)
            {
                GameTile tile = _searchFrontier.Dequeue();
                if (tile != null)
                {
                    if (tile.IsAlternative)
                    {
                        _searchFrontier.Enqueue(tile.GrowPathNorth());
                        _searchFrontier.Enqueue(tile.GrowPathSouth());
                        _searchFrontier.Enqueue(tile.GrowPathEast());
                        _searchFrontier.Enqueue(tile.GrowPathWest());
                    }
                    else
                    {
                        _searchFrontier.Enqueue(tile.GrowPathWest());
                        _searchFrontier.Enqueue(tile.GrowPathEast());
                        _searchFrontier.Enqueue(tile.GrowPathSouth());
                        _searchFrontier.Enqueue(tile.GrowPathNorth());
                    }
                }
            }

            foreach (var tile in _tiles)
            {
                tile.ShowPath();
            }
        }
    }
}
