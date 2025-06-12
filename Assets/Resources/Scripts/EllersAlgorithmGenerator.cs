using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class EllersAlgorithmGenerator : MonoBehaviour
{
    [SerializeField]
    private int _mazeWidth;
    [SerializeField]
    private int _mazeHeight;

    [SerializeField]
    private MazeCell _cellPrefab;

    [SerializeField]
    private MazeCell[,] _mazeStack;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitMaze(_mazeWidth, _mazeHeight);
    }

    private void InitMaze(int width, int height)
    {
        MazeCell[] firstrow = new MazeCell[width];

        for(int i = 0; i < width; i++)
        {
            firstrow[i] = Instantiate(_cellPrefab, new Vector3(i, 0, height), Quaternion.identity, transform);
            firstrow[i].cellID = i + height;
            firstrow[i].cellIndex = new int[] {i, height};


        }
    }

    private void CreateRow(MazeCell[] previousrow, MazeCell[] currentrow)
    {
        
    }
}
