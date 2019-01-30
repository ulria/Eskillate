using UnityEngine;

public class MoveableShape : MonoBehaviour
{
    private Vector3 _screenPoint;
    private Vector3 _offset;

    public bool IsDraggable;

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
