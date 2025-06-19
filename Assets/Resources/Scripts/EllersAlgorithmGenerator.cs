using NUnit.Framework;
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

    private EllerCell[,] _mazeMatrix;


    private void Start()
    {
        EllerCell[] row = CreateRow(20, 0);

    }

    private EllerCell[] CreateRow(int width, int rowindex)
    {
        EllerCell[] result = new EllerCell[width];

        for (int i = 0; i < width; i++)
        {
            GameObject cellGO = Instantiate(_cellPrefab.gameObject, new Vector3(i, 0, rowindex), Quaternion.identity, this.transform);
            EllerCell cell = cellGO.GetComponent<EllerCell>();
            cell.SetID(-1);
            cell.cellIndex = new int[] { i, rowindex };
            result[i] = cell;
        }

        return result;
    }

    private void GenerateMaze(EllerCell[] currentrow)
    {
        InitRow(currentrow);
        ProcessRow(currentrow);
        CreateVerticalBranches(currentrow);
    }

    private static IEnumerator InitRow(EllerCell[] currentrow)
    {
        // initializing row (TESTED)
        for (int i = 0; i < currentrow.Length; i++)
        {
            if (currentrow[i].getID() == -1)
            {
                currentrow[i].SetID(0);
            }

            // right and left sides of the maze
            if (i == 0)
            {
                currentrow[i].setLeftWall(true);
            }
            if (i == currentrow.Length - 1)
            {
                currentrow[i].setRightWall(true);
            }

            yield return new WaitForSeconds(0.1f);
        }

        yield return ProcessRow(currentrow);
    }
    private static IEnumerator ProcessRow(EllerCell[] currentrow)
    {
        // creating sets of the existing row cells , merging cells from different sets randomly (TESTED)
        EllerCell tempcurrent = currentrow[0];
        System.Random random = new System.Random();

        for (int i = 1; i < currentrow.Length; i++)
        {
            if (random.Next(0, 2) == 1 && tempcurrent.getID() != currentrow[i].getID())
            {
                //currentrow[i].SetID(tempcurrent.getID());
            }
            else
            {
                tempcurrent.setRightWall(true);
                currentrow[i].setLeftWall(true);
            }
            tempcurrent = currentrow[i];

            yield return new WaitForSeconds(0.1f);
        }

        yield return CreateVerticalBranches(currentrow);
    }
    private static IEnumerator CreateVerticalBranches(EllerCell[] currentrow)
    {
        //creating vertical branches randomly from cells in the same sets (minimum 1)
        List<EllerCell> set = new List<EllerCell> { currentrow[0] };
        int currentID = 0;

        System.Random rand = new System.Random();

        for (int i = 1; i < currentrow.Length; i++)
        {
            if (currentrow[i].getID() == currentID)
            {
                currentrow[i].setBackWall(rand.Next(0, 2) == 1);
                set.Add(currentrow[i]);
            }
            else
            {
                int randomcell = rand.Next(0, set.Count);
                set[randomcell].setBackWall(false);

                // reset set
                set = new List<EllerCell> { currentrow[i] };
                currentID = 0;
            }

            yield return new WaitForSeconds(0.1f);
        }
    }
}
