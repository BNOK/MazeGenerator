using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    [SerializeField]
    private int _mazeWidth;
    [SerializeField]
    private int _mazeHeight;

    [SerializeField]
    private MazeCell _cellPrefab;

    [SerializeField]
    private MazeCell[,] _cellMatrix;


    private void CreateMatrix()
    {
        _cellMatrix = new MazeCell[_mazeWidth, _mazeHeight];

        for (int i = 0; i < _mazeWidth; i++)
        {
            for (int j = 0; j < _mazeHeight; j++)
            {
                MazeCell cell = new MazeCell();
                cell.cellID = i + j;
                cell.cellIndex = new int[] { i, j };

                _cellMatrix[i,j] = cell;
            }
        }
    }

    private void GenerateMaze(int currentrowindex)
    {
        MazeCell[] currentrow = GetMazeRow(currentrowindex);
        MergeRow(currentrow);

    }

    private MazeCell[] GetMazeRow(int rowindex)
    {
        MazeCell[] result = new MazeCell[_mazeHeight];

        for(int index = 0; index < _mazeHeight; index++)
        {
            result[index] = _cellMatrix[rowindex, index];
        }

        return result;
    }

    private void MergeRow(MazeCell[] currentrow)
    {
        System.Random random = new System.Random();
        for (int index = 0; index < currentrow.Length - 1; index++)
        {
            if (random.Next(0, 2) == 1 && (currentrow[index].cellID != currentrow[index + 1].cellID))
            {
                if (random.Next(0, 2) == 1)
                {
                    currentrow[index + 1].cellID = currentrow[index].cellID;
                }
                else
                {
                    currentrow[index].cellID = currentrow[index + 1].cellID;
                }
            }

            currentrow[index].Visit();

        }

        currentrow[currentrow.Length - 1].Visit();
    }





}
