using UnityEngine;

public class EllerCell : MonoBehaviour
{
    //for eller
    [SerializeField]
    public int cellPublic;

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

    public int[] indexes = new int[2];
    

    public EllerCell(bool left, bool right, bool front, bool back)
    {
        setLeftWall( left );    
        setRightWall( right );
        setFrontWall( front );
        setBackWall( back );
        setVisitedWall(true);
    }


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

    public bool[] GetWallsState()
    {
        bool[] result = new bool[4];
        result[0] = _leftWall.activeSelf;
        result[1] = _rightWall.activeSelf;
        result[2] = _frontWall.activeSelf;
        result[3] = _backWall.activeSelf;

        return result;
    }

    #endregion

    public void setCellIndex(int[] index)
    {
        indexes = index;
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
