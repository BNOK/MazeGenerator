using NUnit.Framework;
using System;
using System.Collections;
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
    private EllerCell _cellPrefab;

    private List<(EllerCell, int?)> _mazeArray;

    private int _currentSetId = 1;

    private void Start()
    {
        _mazeArray = new List<(EllerCell, int?)>();
        int currentIndex = 0;

        // MAZE INITIALIZATION
        List<(EllerCell, int?)> previousrow = CreateRow(null, currentIndex);
        ProcessRowSideWalls(currentIndex, previousrow);

        currentIndex++;
        List<(EllerCell, int?)> currentrow = CreateRow(previousrow, currentIndex);
        SetupRowForProcessing(currentrow);
    }

    private List<(EllerCell, int?)> CreateRow(List<(EllerCell, int?)>? existingrow, int rowindex)
    {

        //EllerCell[] result = new EllerCell[_mazeWidth];
        List<(EllerCell, int?)> result = new List<(EllerCell, int?)>();

        if (existingrow != null)
        {
            result = existingrow;
            for (int i = 0; i < _mazeWidth; i++)
            {
                GameObject cellObject = Instantiate(existingrow[i].Item1.gameObject, new Vector3(i, 0, -rowindex), Quaternion.identity, transform);
                EllerCell tempcomp = cellObject.GetComponent<EllerCell>();
                tempcomp.setCellIndex(new int[] { rowindex, i }, _mazeWidth, _mazeHeight);
                result.Add((tempcomp, existingrow[i].Item2));

            }
        }
        else
        {
            for (int i = 0; i < _mazeWidth; i++)
            {
                Tuple<GameObject, int> cell;
                GameObject cellObject = Instantiate(_cellPrefab.gameObject, new Vector3(i, 0, -rowindex), Quaternion.identity, transform);
                EllerCell tempcomp = cellObject.GetComponent<EllerCell>();

                tempcomp.setCellIndex(new int[] { rowindex, i }, _mazeWidth, _mazeHeight);
                result.Add((tempcomp, _currentSetId));
                _currentSetId++;
            }
        }
        return result;
    }

    private void ProcessRowSideWalls(int currentrowindex, List<(EllerCell, int?)> previousrow)
    {
        System.Random rnd = new System.Random();
        for (int index = 0; index < previousrow.Count - 1; index++)
        {
            if (previousrow[index].Item2 == previousrow[index + 1].Item2)
            {
                previousrow[index].Item1.setRightWall(true);
                previousrow[index + 1].Item1.setLeftWall(true);
            }
            else
            {
                if (rnd.Next(0, 2) == 1)
                {
                    previousrow[index].Item1.setRightWall(true);
                    previousrow[index + 1].Item1.setLeftWall(true);
                }
                else
                {
                    previousrow[index].Item1.setRightWall(false);
                    previousrow[index + 1].Item1.setLeftWall(false);

                    previousrow[index + 1] = (previousrow[index + 1].Item1, previousrow[index].Item2);
                }
            }
            //yield return new WaitForSeconds(0.01f);
        }
        //yield return ProcessRowBranches(currentrowindex, previousrow);
        //yield break;
    }

    private void ProcessRowBranches(int currentrowindex, List<(EllerCell, int?)> row)
    {

        int? currentID = row[0].Item2;

        List<EllerCell> tempset = new List<EllerCell>();
        System.Random rnd = new System.Random();

        for (int index = 0; index < row.Count; index++)
        {
            row[index].Item1.ChangeVisitedWallColor(true);
            if (row[index].Item2.Equals(currentID))
            {
                Debug.Log("same set X !");
                //add to a set collection
                tempset.Add(row[index].Item1);
                if (rnd.Next(0, 11) >= 5)
                {
                    row[index].Item1.setBackWall(true);
                }
            }
            else
            {
                Debug.Log("NOT same set !! ");
                currentID = row[index].Item2;
                tempset[rnd.Next(0, tempset.Count)].setBackWall(false);

                //resetting the set
                tempset.Clear();
                tempset.Add(row[index].Item1);
            }


            //yield return new WaitForSeconds(0.1f);
            row[index].Item1.ChangeVisitedWallColor(false);
        }

        //yield break;
    }

    private void SetupRowForProcessing(List<(EllerCell, int?)> row)
    {
        for (int i = 0; i < row.Count - 1; i++)
        {
            row[i].Item1.setRightWall(false);
            row[i + 1].Item1.setLeftWall(false);
        }

        for (int i = 0; i < row.Count; i++)
        {
            if (row[i].Item1.isBackWallActive())
            {
                row[i].Item1.setFrontWall(true);
                row[i].Item1.setBackWall(false);
                row[i] = (row[i].Item1, null);
            }
        }

        //yield break;
    }

    //private IEnumerator MazeGenerator(List<(EllerCell, int?)> prevrow, List<(EllerCell, int?)> currrow, int index)
    //{
    //    do
    //    {
    //        currrow = CreateRow(prevrow, index);
    //        SetupRowForProcessing(currrow);
    //        yield return ProcessRowSideWalls(index, prevrow);

    //        index++;
    //        yield return new WaitForSeconds(0.25f);

    //    } while (index < 10);

    //    yield break;
    //}
}
