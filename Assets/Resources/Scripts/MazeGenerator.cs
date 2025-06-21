using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    private List<EllerCell> _mazeList;

    #region maze parameters
    [SerializeField]
    private int _mazeWidth;

    [SerializeField]
    private int _mazeHeight;

    [SerializeField]
    private GameObject _cellPrefab;
    #endregion

    #region generator parameters
    [SerializeField]
    private List<(EllerCell, int)> _mazeRow;

    private int currentID = 0;
    #endregion

    private void Start()
    {
        _mazeRow = CreateRow(_mazeWidth, _mazeHeight, 0, ref currentID);
        SpawnRow(_mazeRow);
    }

    private List<(EllerCell, int)> CreateRow(int width, int height, int rowindex, ref int IDref)
    {
        List<(EllerCell, int)> result = new List<(EllerCell, int)> ();

        for(int i = 0; i< width; i++)
        {
            EllerCell tempcell = new EllerCell (true,true,true,true);
            tempcell.SetCellPosition(new int[] { rowindex * width, i });
            result.Add((tempcell, IDref));
            IDref++;
        }
        return result;
    }



    #region HelperFunctions
    
    private void SpawnRow(List<(EllerCell, int)> row)
    {
        for(int i = 0;i< row.Count;i++)
        {
            SpawnCell(row[i].Item1);
        }
    }

    private void SpawnCell(EllerCell cell)
    {
        int[] cellPosition = cell.GetCellPosition();
        GameObject cellgameobject= Instantiate(_cellPrefab, new Vector3(cellPosition[1], 0, -cellPosition[0]), Quaternion.identity, transform);
        EllerCell scenecell = cellgameobject.GetComponent<EllerCell>();

        scenecell.setLeftWall(cell.GetLeftWallActive());
        scenecell.setRightWall(cell.GetRightWallActive());
        scenecell.setFrontWall(cell.GetFrontWallActive());
        scenecell.setBackWall(cell.GetBackWallActive());

        _mazeList.Add(scenecell);
    }

    #endregion

}
