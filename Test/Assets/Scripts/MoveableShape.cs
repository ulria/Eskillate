using UnityEngine;

public class MoveableShape : MonoBehaviour
{
    private Vector3 _screenPoint;
    private Vector3 _offset;

    public bool IsDraggable;

    private Vector3 _startPos;

    // Constants
    private float SCREEN_BORDERS_SAFETY_DIST = 50.0f;

    // Start is called before the first frame update
    void Start()
    {
        var xRangeMin = -Screen.width/2 + SCREEN_BORDERS_SAFETY_DIST;
        var xRangeMax = -SCREEN_BORDERS_SAFETY_DIST;

        var yRangeMin = -Screen.height/2 + SCREEN_BORDERS_SAFETY_DIST;
        var yRangeMax = -SCREEN_BORDERS_SAFETY_DIST;

        var x = Random.Range(xRangeMin, xRangeMax);
        var y = Random.Range(yRangeMin, yRangeMax);

        _startPos = new Vector3(x, y, 0.0f);

        GetComponent<RectTransform>().anchoredPosition = _startPos;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        if (!IsDraggable)
            return;

        _screenPoint = Camera.main.WorldToScreenPoint(transform.position);

        _offset = transform.position - Camera.main.ScreenToWorldPoint(
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z));
    }

    void OnMouseDrag()
    {
        if (!IsDraggable)
            return;

        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z);


        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + _offset;
        transform.position = curPosition;
    }

    public void MoveToStartPos()
    {
        transform.position = _startPos;
    }
}
