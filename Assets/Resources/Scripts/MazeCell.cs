using UnityEngine;

public class MazeCell : MonoBehaviour
{
    //for backtracker
    public int[] cellIndex = new int[] { -1, -1};

    //for eller
    public int? cellID = null;

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

    private bool _visited = false;

    public void ClearLeftWall()
    {
        _leftWall.SetActive(false);
    }

    public void ClearRightWall()
    {
        _rightWall.SetActive(false);
    }

    public void ClearFrontWall()
    {
        _frontWall.SetActive(false);
    }

    public void ClearBackWall()
    {
        _backWall.SetActive(false);
    }

    public void Visit()
    {
        _visitedWall.SetActive(false);
        _visited = true;
    }

    public bool getVisited()
    {
        return _visited;
    }
}
