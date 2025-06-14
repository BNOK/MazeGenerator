using UnityEngine;

public class EllerCell : MonoBehaviour
{
    //for backtracker
    public int[] cellIndex = new int[] { -1, -1 };

    //for eller
    [SerializeField]
    private int cellID = -1;

    [SerializeField]
    private GameObject _leftWall;

    [SerializeField]
    private GameObject _rightWall;

    [SerializeField]
    private GameObject _frontWall;

    [SerializeField]
    private GameObject _backWall;

    public void setLeftWall(bool ison)
    {
        _leftWall.SetActive(ison);
    }

    public void setRightWall(bool ison)
    {
        _rightWall.SetActive(ison);
    }

    public void setFrontWall(bool ison)
    {
        _frontWall.SetActive(ison);
    }

    public void setBackWall(bool ison)
    {
        _backWall.SetActive(ison);
    }

    public void SetID(int? id = null)
    {
        if(id == null)
        {
            cellID = cellIndex[0] + cellIndex[1];
            setFrontWall(true);
        }
        else
        {
            cellID = (int)id;
            if (cellIndex[1] != 0)
            {
                setFrontWall(false);
            }
        }
    }

    public int getID()
    {
        //if (cellID.HasValue)
        //{
        //    return (int)cellID;
        //}
        //else
        //{
        //    Debug.Log("CELL ID IS NOT VALID INT !!!");
        //    return -1;
        //}

        return cellID;
    }
}
