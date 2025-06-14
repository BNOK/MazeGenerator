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
        StartCoroutine( ProcessRow(row));
    }

    private EllerCell[] CreateRow(int width, int rowindex)
    {
        EllerCell[] result = new EllerCell[width];

        for(int i = 0; i < width; i++)
        {
            GameObject cellGO = Instantiate(_cellPrefab.gameObject, new Vector3(i, 0, rowindex), Quaternion.identity, this.transform);
            EllerCell cell = cellGO.GetComponent<EllerCell>();

            cell.cellIndex = new int[] { i, rowindex };
            result[i] = cell;
        }

        return result;
    }

    private IEnumerator ProcessRow(EllerCell[] currentrow)
    {
        // initializing row 
        for(int i = 0;i < currentrow.Length;i++)
        {
            if (currentrow[i].getID() == -1)
            {
                currentrow[i].SetID();
            }

            if (i == 0)
            {
                currentrow[i].setLeftWall(true);
            }
            if(i == currentrow.Length - 1)
            {
                currentrow[i].setRightWall(true);
            }

            yield return new WaitForSeconds(0.2f);
        }

        // merging sets
        EllerCell tempcurrent = currentrow[0];
        System.Random random = new System.Random();

        for(int i = 1; i< currentrow.Length; i++)
        {
            if(random.Next(0,2) == 1 && tempcurrent.getID() != currentrow[i].getID())
            {
                currentrow[i].SetID(tempcurrent.getID());
                tempcurrent.setRightWall(true);
                currentrow[i].setLeftWall(true);
            }
            tempcurrent = currentrow[i];

            yield return new WaitForSeconds(0.2f);
        }
    }
    

    
}
