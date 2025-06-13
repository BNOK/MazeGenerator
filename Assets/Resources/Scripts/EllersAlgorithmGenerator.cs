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
    private MazeCell[,] _mazeStack;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private MazeCell[] CreateRow(int rowindex)
    {
        MazeCell[] result = new MazeCell[_mazeWidth];

        for(int ID = 0; ID < _mazeWidth; ID++)
        {
            MazeCell cell = new MazeCell();
            cell.cellID = ID;
            cell.cellIndex = new int[] {ID, rowindex};
            result[ID] = cell;
        }


        return result;
    }

    private void MergeRow( ref MazeCell[] currentrow)
    {
        System.Random random = new System.Random();
        for(int index = 0; index < currentrow.Length -1; index++)
        {
            if (random.Next(0,2) == 1 && (currentrow[index].cellID != currentrow[index + 1].cellID))
            {
                if(random.Next(0,2) == 1)
                {
                    currentrow[index + 1].cellID = currentrow[index].cellID;
                }
                else
                {
                    currentrow[index].cellID = currentrow[index + 1].cellID;
                }
            }
        }
    }

    private void ClearVerticalWalls(MazeCell[] row)
    {
        MazeCell currentcell = row[0];
        for(int index = 1;index < row.Length;index++)
        {
            if (row[index].cellID == currentcell.cellID)
            {
                row[index].ClearLeftWall();
                currentcell.ClearRightWall();
            }

            currentcell = row[index];
        }
    }
}
