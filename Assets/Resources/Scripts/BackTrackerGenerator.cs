using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* this Implementation of the BACKTRACKING algorithm which is a depth-first-randomized-search is not optimal 
 * since it can get stack overflow error in many environments in todays systems
 * you can check the IterativeBacktracker script for a better implementation using a custom Stack
 */
public class BackTrackerGenerator : MonoBehaviour
{
    [SerializeField]
    private int _mazeWidth;
    [SerializeField]
    private int _mazeHeight;

    private MazeCell[,] _mazeGrid;

    [SerializeField]
    private GameObject _mazeCell;

    private void Start()
    {
        _mazeGrid = new MazeCell[_mazeWidth, _mazeHeight];

        for (int width = 0; width < _mazeWidth; width++)
        {
            for (int height = 0; height < _mazeHeight; height++)
            {
                GameObject tempcell = Instantiate(_mazeCell, new Vector3(width, 0, height), Quaternion.identity, this.transform);
                MazeCell cellcomponent = tempcell.GetComponent<MazeCell>();
                cellcomponent.cellIndex[0] = width;
                cellcomponent.cellIndex[1] = height;

                _mazeGrid[width, height] = cellcomponent;
            }
        }

        StartCoroutine(GenerateMaze(null, _mazeGrid[0, 0]));
    }

    IEnumerator GenerateMaze(MazeCell previouscell, MazeCell currentcell)
    {
        currentcell.Visit();
        ClearWalls(previouscell, currentcell);

        yield return new WaitForSeconds(0.1f);

        MazeCell nextcell;
        do
        {
            nextcell = getNextUnivisitedCell(currentcell);
            if (nextcell != null)
            {
                yield return GenerateMaze(currentcell, nextcell);
            }
        } while (nextcell != null);
    }

    void ClearWalls(MazeCell previouscell, MazeCell currentcell)
    {
        if (previouscell == null) return;

        int widthdiff = currentcell.cellIndex[0] - previouscell.cellIndex[0];
        int heightdiff = currentcell.cellIndex[1] - previouscell.cellIndex[1];

        // Z axis
        if (widthdiff > 0)
        {
            previouscell.ClearRightWall();
            currentcell.ClearLeftWall();
        }
        else if (widthdiff < 0)
        {
            previouscell.ClearLeftWall();
            currentcell.ClearRightWall();
        }

        // X axis
        if (heightdiff > 0)
        {
            previouscell.ClearFrontWall();
            currentcell.ClearBackWall();
        }
        else if (heightdiff < 0)
        {
            previouscell.ClearBackWall();
            currentcell.ClearFrontWall();
        }
    }

    MazeCell getNextUnivisitedCell(MazeCell currentcell)
    {
        MazeCell[] resultcells = GetUnvisitedNeighbors(currentcell);

        if (resultcells.Length > 0)
        {
            return resultcells[Random.Range(0, resultcells.Length)];
        }
        return null;
    }
    MazeCell[] GetUnvisitedNeighbors(MazeCell currentcell)
    {
        List<MazeCell> result = new List<MazeCell>();

        int widthindex = currentcell.cellIndex[0];
        int heightindex = currentcell.cellIndex[1];
        MazeCell cellreference;

        if (widthindex + 1 < _mazeWidth)
        {
            cellreference = _mazeGrid[widthindex + 1, heightindex];
            if (!cellreference.getVisited())
            {
                result.Add(cellreference);
            }
        }

        if (widthindex - 1 >= 0)
        {
            cellreference = _mazeGrid[widthindex - 1, heightindex];
            if (!cellreference.getVisited())
            {
                result.Add(cellreference);
            }
        }

        if (heightindex + 1 < _mazeHeight)
        {
            cellreference = _mazeGrid[widthindex, heightindex + 1];
            if (!cellreference.getVisited())
            {
                result.Add(cellreference);
            }
        }

        if (heightindex - 1 >= 0)
        {
            cellreference = _mazeGrid[widthindex, heightindex - 1];
            if (!cellreference.getVisited())
            {
                result.Add(cellreference);
            }
        }


        return result.ToArray();
    }
}
