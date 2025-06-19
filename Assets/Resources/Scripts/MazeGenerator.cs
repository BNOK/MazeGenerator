using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    [SerializeField]
    private int _mazeWidth;
    [SerializeField]
    private int _mazeHeight;

    [SerializeField]
    private EllerCell _cellPrefab;

    private EllerCell[] _mazeArray;

    private Coroutine currentCoroutine;

    private void Start()
    {
        _mazeArray = new EllerCell[_mazeWidth * _mazeHeight];
        // maze initialization from flat array
        int currentrowindex = 0;
        EllerCell[] previousrow = CreateRow(currentrowindex);

        currentCoroutine = StartCoroutine(ProcessRowSideWalls(currentrowindex, previousrow));

        EllerCell[] currentrow = previousrow;
        //currentCoroutine = StartCoroutine(ProcessRowBranches(currentrowindex, previousrow));
        

    }

    private EllerCell[] CreateRow(int rowindex)
    {
        EllerCell[] result = new EllerCell[_mazeWidth];
        for (int i = 0; i < _mazeWidth; i++)
        {
            GameObject cellgameobject = Instantiate(_cellPrefab.gameObject, new Vector3(i, 0, rowindex), Quaternion.identity, transform);
            EllerCell tempcomp = cellgameobject.GetComponent<EllerCell>();

            tempcomp.setCellIndex(new int[] { rowindex, i }, _mazeWidth, _mazeHeight);
            result[i] = tempcomp;
        }

        return result;
    }

    private IEnumerator ProcessRowSideWalls(int currentrowindex, EllerCell[] previousrow)
    {
        
        for (int index = 0; index < previousrow.Length; index++)
        {
            if (previousrow[index].getID() == null)
            {
                previousrow[index].SetID(index + currentrowindex * _mazeWidth);
            }
        }

        System.Random rnd = new System.Random();
        for (int index = 0; index < previousrow.Length - 1; index++)
        {
            if (previousrow[index].getID() != previousrow[index + 1].getID() && rnd.Next(0, 2) == 1)
            {
                previousrow[index].setRightWall(true);
                previousrow[index + 1].setLeftWall(true);
            }
            else
            {
                previousrow[index].setRightWall(false);
                previousrow[index + 1].setLeftWall(false);
                previousrow[index + 1].SetID((int)previousrow[index].getID());
            }
            //previousrow[index].setVisitedWall(false);
            //previousrow[index + 1].setVisitedWall(false);

            yield return new WaitForSeconds(0.01f);
        }

        yield return ProcessRowBranches(currentrowindex, previousrow);
    }

    private IEnumerator ProcessRowBranches(int currentrowindex, EllerCell[] previousrow)
    {
        
        int? currentID = previousrow[0].getID();

        List<EllerCell> tempset = new List<EllerCell>();
        System.Random rnd = new System.Random();

        for(int index = 0; index < previousrow.Length; index++)
        {
            previousrow[index].ChangeVisitedWallColor(true);
            if (previousrow[index].getID().Equals(currentID))
            {
                Debug.Log("same set X !");
                //add to a set collection
                tempset.Add(previousrow[index]);
                if(rnd.Next(0,11) >= 5)
                {
                    previousrow[index].setBackWall(true);
                }
            }
            else
            {
                Debug.Log("NOT same set !! ");
                currentID = previousrow[index].getID();
                tempset[rnd.Next(0, tempset.Count)].setBackWall(false);

                //resetting the set
                tempset.Clear();
                tempset.Add(previousrow[index]);
            }

            
            yield return new WaitForSeconds(0.1f);
            previousrow[index].ChangeVisitedWallColor(false);
        }

        yield break;
    }

    

   
    

  


}
