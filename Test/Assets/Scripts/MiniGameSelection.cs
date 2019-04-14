using UnityEngine;
using UnityEngine.UI;

public class MiniGameSelection : MonoBehaviour
{
    private bool isDragging = false;
    public float SelectedElementX = 0.0f;
    public float SelectedElementY = 0.0f;
    private float _elementWidth = 0.0f;
    private float _scrollRange = 0.0f;
    private float _scrollMin = 0.0f;
    private int _previouslySelectedMiniGameIndex = 0;

    public GameObject ScrollList;
    public GameObject ScrollListContent;
    public GameObject ScrollListViewport;
    public GameObject[] Elements;

    // Start is called before the first frame update
    void Start()
    {
        _elementWidth = ScrollListContent.transform.GetChild(0).GetComponent<RectTransform>().rect.width;
        _elementWidth += ScrollListContent.GetComponent<HorizontalLayoutGroup>().spacing;

        _scrollRange = (Elements.Length-1) * _elementWidth;
        _scrollMin = ScrollListContent.GetComponent<RectTransform>().anchoredPosition.x - _scrollRange;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isDragging)
        {
            var currentScrollX = ScrollListContent.GetComponent<RectTransform>().anchoredPosition.x;
            var scrollOffset = currentScrollX - _scrollMin;
            var scrollNbElementOffset = Mathf.RoundToInt(scrollOffset / _elementWidth);
            var newOffset = scrollNbElementOffset * _elementWidth;
            var newPosition = _scrollMin + newOffset;
            newPosition = Mathf.Clamp(newPosition, _scrollMin, _scrollMin + _scrollRange);
            LerpToElement(newPosition);

            if(scrollNbElementOffset != _previouslySelectedMiniGameIndex)
            {
                var elementIndexReversed = (Elements.Length - 1) - scrollNbElementOffset;
                Elements[elementIndexReversed].GetComponent<MiniGame>().OnSelected();
                _previouslySelectedMiniGameIndex = scrollNbElementOffset;
            }
        }
    }

    private void LerpToElement(float position)
    {
        var newX = Mathf.Lerp(ScrollListContent.GetComponent<RectTransform>().anchoredPosition.x, position, Time.deltaTime * 10f);
        Vector2 newPos = new Vector2(newX, SelectedElementY);
        ScrollListContent.GetComponent<RectTransform>().anchoredPosition = newPos;
    }

    public void OnBeginDrag()
    {
        isDragging = true;
    }

    public void OnEndDrag()
    {
        isDragging = false;
    }

    // Anchored position of the content : [-420;420]
    // Find width of an element + spacing
    // When not dragging : take current anchored pos x and see range.min + 

    // var test = current anchored pos x - range.min
    // var test2 = test / (width+spacing)
    // var index  = round to nearest int (test2)
}
