using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
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
    private int _ID = 0;

    private void Start()
    {
        StartCoroutine(GenerateMaze());
    }

    private IEnumerator GenerateMaze()
    {
        // maze init
        int currentrowIndex = 0;
        EllerCell[] row = CreateRow(null, _mazeWidth, currentrowIndex, ref _ID);
        JoinCells(row);
        CreateBranches(null, row);
        currentrowIndex++;
        yield return new WaitForSeconds(0.1f);

        EllerCell[] currentrow;
        //maze generation
        do
        {
            currentrow = CreateRow(row, _mazeWidth, currentrowIndex, ref _ID);
            currentrowIndex++;
            // get the new row ready for processing
            RowPreProcessing(currentrow);
            Debug.Log("finished pre processing");
            yield return new WaitForSeconds(0.2f);
            JoinCells(currentrow);
            Debug.Log("finished joining cells");
            yield return new WaitForSeconds(0.2f);
            CreateBranches(row, currentrow);
            Debug.Log("finished creating branches");
            yield return new WaitForSeconds(0.2f);
            row = currentrow;
            Debug.Log("finished this row");
        } while (currentrowIndex < _mazeHeight);

        yield break;
    }

    private EllerCell[] CreateRow(EllerCell[] previous, int width, int row, ref int ID)
    {
        List<EllerCell> result = new List<EllerCell>();


        if(previous == null)
        {
            for (int i = 0; i < width; i++)
            {
                GameObject go = Instantiate(_cellPrefab, new Vector3(i +1, 0, -row-2), Quaternion.identity, transform);
                EllerCell cell = go.GetComponent<EllerCell>();
                if(cell.GetCellID() == -1)
                {
                    cell.SetCellID(ID);
                    ID++;
                }
                // maze walls
                if (i == 0)
                {
                    cell.setLeftWall(true);
                }
                if(i == width-1)
                {
                    cell.setRightWall(true);
                }
                if(row == 0)
                {
                    cell.setFrontWall(true);
                }

                result.Add(cell);
            }
        }
        else
        {
            // DO A CUSTOM DEEP COPY USING THE INSTANTIATE
            for (int i = 0; i < width; i++)
            {
                GameObject go = Instantiate(_cellPrefab, new Vector3(i + 1, 0, -row -2), Quaternion.identity, transform);
                EllerCell cell = go.GetComponent<EllerCell>();
                cell.SetCellID(previous[i].GetCellID());

                cell.setLeftWall(previous[i].GetLeftWallActive());
                cell.setRightWall(previous[i].GetRightWallActive());
                cell.setFrontWall(previous[i].GetBackWallActive());
                cell.setBackWall(previous[i].GetBackWallActive());

                result.Add(cell);
            }
        }

        return result.ToArray();
    }

    private void JoinCells(EllerCell[] row)
    {
        
        for(int i=0; i< row.Length -1; i++)
        {
            if (row[i].GetCellID() != row[i + 1].GetCellID())
            {
                float random = UnityEngine.Random.Range(0.0f, 1.0f);
                //Debug.Log("JOINING !!" + random);
                if ( random > 0.5f)
                {
                    // joining
                    row[i + 1].SetCellID(row[i].GetCellID());
                }
                else
                {
                    row[i + 1].setLeftWall(true);
                    row[i].setRightWall(true);
                }
            }
            else
            {
                row[i + 1].setLeftWall(true);
                row[i].setRightWall(true);
            }
        }
    }

    private void CreateBranches(EllerCell[] previous, EllerCell[] row)
    {
        int currentID = row[0].GetCellID();
        List<EllerCell> tempset = new List<EllerCell>();

        for(int i=0; i< row.Length ; i++)
        {
            if (row[i].GetCellID() !=  currentID)
            {
                int index = UnityEngine.Random.Range(0, tempset.Count);
                tempset[index].setBackWall(false);
                currentID = row[i].GetCellID();
                tempset.Clear();
                tempset.Add(row[i]);
                
            }
            else
            {
                bool value = UnityEngine.Random.Range(0.0f, 1.0f) > 0.5f;
                row[i].setBackWall(value);
                tempset.Add(row[i]);
            }
        }
    }

    private void RowPreProcessing(EllerCell[] row)
    {
        for(int i = 0; i< row.Length ;i++)
        {
            if(i < row.Length - 1)
            {
                row[i].setRightWall(false);
                row[i + 1].setLeftWall(false);
            }
            if (row[i].GetBackWallActive() == true)
            {
                row[i].SetCellID(_ID);
                _ID++;

                row[i].setBackWall(false);
                row[i].setFrontWall(true);
            }
        }
    }

}
