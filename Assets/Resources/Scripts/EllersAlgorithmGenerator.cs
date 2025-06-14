using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;


/* Spawn cells on X axis then change rows by changing Z 
 * DO NOT CREATE THE MAZE ON THE Z BY DEFAULT TO NOT BE CONFUSED ON THE VISUALS
 */

public class EllersAlgorithmGenerator : MonoBehaviour
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

                _cellMatrix[i, j] = cell;
            }
        }
    }

    private void GenerateMaze(int currentrowindex)
    {
        MazeCell[] currentrow = GetMazeRow(currentrowindex);
        MergeRowHorizontal(currentrow);
        ClearRowWalls(currentrow);



    }

    private MazeCell[] GetMazeRow(int rowindex)
    {
        MazeCell[] result = new MazeCell[_mazeHeight];

        for (int index = 0; index < _mazeHeight; index++)
        {
            result[index] = _cellMatrix[rowindex, index];
        }

        return result;
    }

    private void MergeRowHorizontal(MazeCell[] currentrow)
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

    private void ClearRowWalls(MazeCell[] currentrow)
    {
        MazeCell currentcell = currentrow[0];
        for (int i = 1; i < currentrow.Length; i++)
        {
            if (currentcell.cellID == currentrow[i].cellID)
            {
                currentcell.ClearRightWall();
                currentrow[i].ClearLeftWall();
            }

            currentcell = currentrow[i];
        }
    }

    private void MergeRowVertical(MazeCell[] currentrow)
    {
        List<List<MazeCell>> sets = CreateSets(currentrow);

        foreach (var set in sets)
        {
            CreateBranch(set);
        }
    }

    private List<List<MazeCell>> CreateSets(MazeCell[] currentrow)
    {
        List<List<MazeCell>> cellsets = new List<List<MazeCell>>();
        List<MazeCell> tempList = new List<MazeCell> { currentrow[0] };

        for (int i = 1; i < currentrow.Length; i++)
        {
            if (currentrow[i].cellID == tempList[0].cellID)
            {
                tempList.Add(currentrow[i]);
            }
            else
            {
                // add set to the list
                cellsets.Add(tempList);

                //reset and add current element
                tempList = new List<MazeCell>
                {
                    currentrow[i]
                };
            }
        }

        return cellsets;
    }

    private void CreateBranch(List<MazeCell> branchset)
    {

    }
}
