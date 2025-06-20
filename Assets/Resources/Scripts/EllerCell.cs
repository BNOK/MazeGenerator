using UnityEngine;

public class EllerCell : MonoBehaviour
{
    //for backtracker
    public int[] cellIndex = new int[] { -1, -1 };

    //for eller
    [SerializeField]
    private int cellPublic;

    private int? cellID = null;

    [SerializeField]
    private GameObject _leftWall;

    [SerializeField]
    private GameObject _rightWall;

    [SerializeField]
    private GameObject _frontWall;

    [SerializeField]
    private GameObject _backWall;

    [SerializeField]
    private GameObject _visitedWall;

    [SerializeField]
    private Renderer _renderer;

    #region walls
    public void setLeftWall(bool ison)
    {
        //Debug.Log("left wall called !");
        _leftWall.SetActive(ison);
    }

    public void setRightWall(bool ison)
    {
        //Debug.Log("right wall called !");
        _rightWall.SetActive(ison);
    }

    public void setFrontWall(bool ison)
    {
        //Debug.Log("front wall called !");
        _frontWall.SetActive(ison);
    }

    public void setBackWall(bool ison)
    {
        //Debug.Log("back wall called !");
        _backWall.SetActive(ison);
    }

    public void setVisitedWall(bool ison)
    {
        _visitedWall.SetActive(ison);
    }

    public bool isBackWallActive()
    {
        return _backWall.activeSelf;
    }

    #endregion

    public void SetID(int? id)
    {
        cellID = id;
        cellPublic = -1;
    }

    public int? getID()
    {
        return cellID;
    }

    public void setCellIndex(int[] index, int width, int height)
    {
        cellIndex = index;
        //check column index
        if (cellIndex[1] == 0)
        {
            setLeftWall(true);
        }
        else if (cellIndex[1] == width-1 )
        {
            setRightWall(true);
        }

        //check row index 
        if (cellIndex[0] == 0)
        {
            setFrontWall(true);
        }
        else if (cellIndex[0] == height-1 )
        {
            setBackWall(true);
        }
    }

    public void ChangeVisitedWallColor(bool selected)
    {
        if (selected)
        {
            _renderer.material.color = Color.red;
        }
        else
        {
            _renderer.material.color = Color.green;
        }
    }
}
