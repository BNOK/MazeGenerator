using UnityEngine;

public class EllerCell : MonoBehaviour
{
    #region walls
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
    #endregion

    private int[] cellPosition = new int[2];

    [SerializeField]
    private int cellID = -1;


    #region walls
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

    public void setVisitedWall(bool ison)
    {
        _visitedWall.SetActive(ison);
    }

    // wall getters
    public bool GetLeftWallActive()
    {
        return _leftWall.activeSelf;
    }
    public bool GetRightWallActive()
    {
        return _rightWall.activeSelf;
    }
    public bool GetFrontWallActive()
    {
        return _frontWall.activeSelf;
    }
    public bool GetBackWallActive()
    {
        return _backWall.activeSelf;
    }

    #endregion

    #region HelperFunctions
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

    #endregion
    public void SetCellPosition(int[] posref)
    {
        if (posref.Length == 2)
        {
            cellPosition = posref;
        }
    }

    public int[] GetCellPosition()
    {
        return cellPosition;
    }

    public int GetCellID()
    {
        return cellID;
    }

    public void SetCellID(int ID)
    {
        cellID = ID;
    }
}
