using UnityEngine;

public class MoveableShape : MonoBehaviour
{
    private Vector3 _screenPoint;
    private Vector3 _offset;

    public bool IsDragging;

    private Vector3 _startPos;

    // Start is called before the first frame update
    void Start()
    {
        var x = Random.Range(-40.0f, 0.0f);
        var y = Random.Range(-10.0f, 20.0f);

        _startPos = new Vector3(x, y, 0.0f);

        transform.position = _startPos;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        // Indicate that we are dragging the shape from now on
        IsDragging = true;

        _screenPoint = Camera.main.WorldToScreenPoint(transform.position);

        _offset = transform.position - Camera.main.ScreenToWorldPoint(
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z));
    }

    void OnMouseDrag()
    {
        // We dont want to drag anymore we are not dragging
        if (!IsDragging)
            return;

        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z);


        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + _offset;
        transform.position = curPosition;
    }

    public void OnMouseUp()
    {
        // Indicate that we are not dragging anymore
        IsDragging = false;
    }

    public void MoveToStartPos()
    {
        transform.position = _startPos;
    }
}
