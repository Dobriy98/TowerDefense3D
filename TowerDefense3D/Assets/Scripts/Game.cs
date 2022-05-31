using System.Collections;
using System.Collections.Generic;
using GameBoard;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private GameBoardSettings gameBoard;
    void Start()
    {
        CreateGameBoard();
    }

    private void CreateGameBoard()
    {
        if (gameBoard != null)
        {
            var board = Instantiate(gameBoard.gameBoardPrefab);
            board.Initialize(gameBoard.size);
        }
    }
}
