/* naming convention used on this project is from
 * https://www.c-sharpcorner.com/UploadFile/8a67c0/C-Sharp-coding-standards-and-naming-conventions/
 * thank you for your review
 */

using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField]
    private Vector3 _offsetPosition = Vector3.zero;
    private int _gridHeight { get; set; }
    private int _gridWidth { get; set; }

    private List<MazeCell> Cells = new List<MazeCell> ();
    public MazeCell _cell;

    private void Start()
    {
        _cell._cellHeight = 1;
        _cell._cellWidth = 1;
        CreateGrid(5, 5);
    }

    public void CreateGrid(int height, int width)
    {
        _gridHeight = height;
        _gridWidth = width;

        for (int i = 0; i < _gridHeight; i++)
        {
            for (int j = 0; j < _gridWidth; j++)
            {
                GameObject tempcell = Instantiate(_cell.gameObject, new Vector3(i, 0, j) + _offsetPosition, Quaternion.identity, this.transform);
                MazeCell cell = tempcell.GetComponent<MazeCell>();
                cell.cellIndex = new int[] { i, j };

                Cells.Add(cell);
                //Debug.Log("x changed = " + currentposition);
            }
        }
    }

    public void GenerateMaze(MazeCell previouscell, MazeCell currentcell)
    {
        currentcell.Visit();
        ClearWalls(previouscell, currentcell);

        // find neighbors of current cell


        
    }

    public void ClearWalls(MazeCell previouscell, MazeCell currentcell)
    {
        int hIndex = previouscell.cellIndex[0] - currentcell.cellIndex[0];
        int vIndex = previouscell.cellIndex[1] - currentcell.cellIndex[1];
        
        if(hIndex < 0 )
        {
            previouscell.ClearLeftWall();
            currentcell.ClearRightWall();
        }
        else if (hIndex != 0)
        {
            previouscell.ClearRightWall();
            currentcell.ClearLeftWall();
        }

        if( vIndex < 0)
        {
            previouscell.ClearFrontWall();
            currentcell.ClearBackWall();
        }
        else if ( vIndex != 0)
        {
            previouscell.ClearBackWall();
            currentcell.ClearFrontWall();
        }

    }

    void GetUnvisitedNeighbors(MazeCell cell)
    {
        // +1 hor

        // -1 hor

        // +1 vert

        // -1 vert
    }

    
}
