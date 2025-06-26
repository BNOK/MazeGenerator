using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Jobs;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
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
        _mazeList = new List<EllerCell>();
        StartCoroutine(GenerateMaze());

    }


    private IEnumerator CircularMazeGenerator()
    {
        int currentIndex = 0;
        EllerCell[] currentrow = new EllerCell[_mazeWidth];
        EllerCell[] previousrow = null;

        if (_mazeList.Count > 0)
        {
            do
            {
                if (currentIndex == _mazeHeight)
                {
                    currentIndex = 0;
                    LastRow(currentrow);
                }
                currentrow = GetRow(previousrow,currentIndex);
                RowPreProcessing(currentrow);
                yield return new WaitForSeconds(0.01f);
                JoinCells(currentrow);
                yield return new WaitForSeconds(0.01f);
                CreateBranches(currentrow);
                previousrow = currentrow;

                currentIndex++;
                if(_ID > _mazeHeight * _mazeWidth)
                {
                    _ID = 0;
                }
            } while (true);
        }
        yield break;
    }

    private EllerCell[] GetRow(EllerCell[] previous, int rowindex)
    {
        List<EllerCell> result = new List<EllerCell>();

        if (previous == null) 
        {
            // for the first row
            result = _mazeList.GetRange(rowindex * _mazeWidth, _mazeWidth);
            ResetRow(result, rowindex);
        }
        else
        {
            result = _mazeList.GetRange(rowindex * _mazeWidth, _mazeWidth);
            for(int i=0; i< result.Count; i++)
            {
                result[i].SetCellID(previous[i].GetCellID());

                result[i].setLeftWall(previous[i].GetLeftWallActive());
                result[i].setRightWall(previous[i].GetRightWallActive());
                result[i].setFrontWall(previous[i].GetBackWallActive());
                result[i].setBackWall(previous[i].GetBackWallActive());
            }
        }
        

        return result.ToArray();
    }
    
    private void ResetRow(List<EllerCell> row, int rowindex)
    {
        for(int i=0; i<row.Count; i++)
        {
            if(rowindex == 0)
            {
                row[i].setFrontWall(true);
            }
            if (i == 0)
            {
                row[i].setLeftWall(true);
            }
            if(i == _mazeWidth)
            {
                row[i].setRightWall(true);
            }

            row[i].SetCellID(-1);
        }
    }

    #region BaseGenerator
    private IEnumerator GenerateMaze()
    {
        // maze init
        int currentrowIndex = 0;
        EllerCell[] row = CreateRow(null, _mazeWidth, currentrowIndex, ref _ID);
        JoinCells(row);
        CreateBranches( row);
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
            yield return new WaitForSeconds(0.02f);
            JoinCells(currentrow);
            Debug.Log("finished joining cells");
            yield return new WaitForSeconds(0.02f);
            CreateBranches(currentrow);
            Debug.Log("finished creating branches");
            yield return new WaitForSeconds(0.02f);
            row = currentrow;
            Debug.Log("finished this row");
        } while (currentrowIndex < _mazeHeight);

        //LastRowProcessing
        //currentrow = CreateRow(row, _mazeWidth, currentrowIndex, ref _ID);
        LastRow(row);
        yield return CircularMazeGenerator();
    }

    private EllerCell[] CreateRow(EllerCell[] previous, int width, int row, ref int ID)
    {
        // Create a row , instantiate objects from row data, sets data for walls (for outer walls of the maze)
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
            /* DO A CUSTOM DEEP COPY USING THE INSTANTIATE
             * copy data from the previous row cell to the current cell
             */
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

        _mazeList.AddRange(result);
        return result.ToArray();
    }

    private void JoinCells(EllerCell[] row)
    {
        /* randomly selects if two adjacent cells will merge into the same set
         * if they will merge into the same set knock down the walls
         * if they are on the same set, create a wall to prevent loops
         */
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

    private void CreateBranches(EllerCell[] row)
    {
        /* creates a temporary set holding elements with the same ID
         * if the ID changes, we choose a random cell from the set to open its walls
         */
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

    private void LastRow(EllerCell[] row)
    {
        for(int i = 0;i< row.Length ;i++)
        {
            row[i].setBackWall(true);
        }
        System.Random random = new System.Random();
        
        for(int i = 0; i<row.Length - 1; i++)
        {
            if (row[i].GetCellID() != row[i + 1].GetCellID())
            {
                row[i].setRightWall(false);
                row[i + 1].setLeftWall(false);
                row[i + 1].SetCellID(row[i].GetCellID());
            }
        }
    }

    #endregion

    

}
