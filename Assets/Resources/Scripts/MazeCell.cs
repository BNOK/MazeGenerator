/* naming convention used on this project is from
 * https://www.c-sharpcorner.com/UploadFile/8a67c0/C-Sharp-coding-standards-and-naming-conventions/
 * thank you for your review
 */

using UnityEngine;

public class MazeCell : MonoBehaviour
{
    public int _cellHeight = 1;
    public int _cellWidth = 1;
    public bool _visited {  get; private set; }

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

    public int[] cellIndex; 


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
}
