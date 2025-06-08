using TMPro;
using UnityEngine;
using TMPro;

public class TextManager : MonoBehaviour
{
    public GameObject text;
    public GameObject camera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        text.transform.LookAt(camera.transform);
    }
}
