using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
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
        StartCoroutine( ProcessRow(row, null));
    }

    private EllerCell[] CreateRow(int width, int rowindex)
    {
        EllerCell[] result = new EllerCell[width];

        for(int i = 0; i < width; i++)
        {
            GameObject cellGO = Instantiate(_cellPrefab.gameObject, new Vector3(i, 0, rowindex), Quaternion.identity, this.transform);
            EllerCell cell = cellGO.GetComponent<EllerCell>();
            cell.SetID(-1);
            cell.cellIndex = new int[] { i, rowindex };
            result[i] = cell;
        }

        return result;
    }

    private IEnumerator ProcessRow(EllerCell[] currentrow, EllerCell[] previousrow)
    {
        // initializing row (TESTED)
        for(int i = 0;i < currentrow.Length;i++)
        {
            if (currentrow[i].getID() == -1)
            {
                currentrow[i].SetID();
            }

            // right and left sides of the maze
            if (i == 0)
            {
                currentrow[i].setLeftWall(true);
            }
            if(i == currentrow.Length - 1)
            {
                currentrow[i].setRightWall(true);
            }

            yield return new WaitForSeconds(0.1f);
        }

        // creating sets of the existing row cells , merging cells from different sets randomly (TESTED)
        EllerCell tempcurrent = currentrow[0];
        System.Random random = new System.Random();

        for(int i = 1; i< currentrow.Length; i++)
        {
            if(random.Next(0,2) == 1 && tempcurrent.getID() != currentrow[i].getID())
            {
                currentrow[i].SetID(tempcurrent.getID());
            }
            else
            {
                tempcurrent.setRightWall(true);
                currentrow[i].setLeftWall(true);
            }
            tempcurrent = currentrow[i];

            yield return new WaitForSeconds(0.1f);
        }

        //creating vertical branches randomly from cells in the same sets (minimum 1)
        List<EllerCell> set = new List<EllerCell> { currentrow[0] };
        int currentID = currentrow[0].getID();

        System.Random rand = new System.Random();

        for (int i = 1; i< currentrow.Length; i++)
        {
            if (currentrow[i].getID() == currentID)
            {
                currentrow[i].setBackWall(rand.Next(0,2) == 1);
                set.Add(currentrow[i]);
            }
            else
            {
                int randomcell = rand.Next(0, set.Count);
                set[randomcell].setBackWall(false);

                // reset set
                set = new List<EllerCell> { currentrow[i] };
                currentID = currentrow[i].getID();
            }

            yield return new WaitForSeconds(0.1f);
        }
    }
    

    
}
