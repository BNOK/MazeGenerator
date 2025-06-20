using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    private List<EllerCell> _mazeList;


    [SerializeField]
    private int _mazeWidth;

    [SerializeField]
    private int _mazeHeight;

    [SerializeField]
    private GameObject _cellPrefab;

    [SerializeField]
    private List<(EllerCell, int)> _mazeRow;

    private static int currentID = 0;

    private void Start()
    {
        _mazeRow = CreateRow(_mazeWidth, _mazeHeight, 0);
        SpawnRow(_mazeRow);
    }

    private List<(EllerCell, int)> CreateRow(int width, int height, int rowindex)
    {
        List<(EllerCell, int)> result = new List<(EllerCell, int)> ();

        for(int i = 0; i< width; i++)
        {
            EllerCell tempcell = new EllerCell (true,true,true,true);
            tempcell.setCellIndex(new int[] { i, rowindex });

            result.Add((tempcell, currentID));
            currentID++;
        }

        return result;
    }



    #region HelperFunctions
    
    private void SpawnRow(List<(EllerCell, int)> row)
    {
        for(int i = 0;i< row.Count;i++)
        {
            EllerCell temp = row[i].Item1 as EllerCell;

            // DEEP COPY
            GameObject cellGO = Instantiate(_cellPrefab, new Vector3(temp.indexes[0], temp.indexes[1]), Quaternion.identity, transform);
            EllerCell comp = cellGO.GetComponent<EllerCell>();
            bool[] walls = temp.GetWallsState();
            comp.setLeftWall(walls[0]);
            comp.setRightWall(walls[1]);
            comp.setFrontWall(walls[2]);
            comp.setBackWall(walls[3]);

            _mazeList.Add(comp);
        }
    }

    #endregion

}
