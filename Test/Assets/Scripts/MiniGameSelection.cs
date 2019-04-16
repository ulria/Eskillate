using UnityEngine;
using UnityEngine.UI;

public class MiniGameSelection : MonoBehaviour
{
    private bool isDragging = false;
    public float SelectedElementX = 0.0f;
    public float SelectedElementY = 0.0f;
    private float _elementWidthPlusSpacing = 0.0f;
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
        var elementWidth = ScrollListContent.transform.GetChild(0).GetComponent<RectTransform>().rect.width;
        var spacing = ScrollListContent.GetComponent<HorizontalLayoutGroup>().spacing;
        _elementWidthPlusSpacing = elementWidth + spacing;

        _scrollRange = (Elements.Length-1) * _elementWidthPlusSpacing;
        _scrollMin = ScrollListContent.GetComponent<RectTransform>().anchoredPosition.x - _scrollRange;

        ScrollListContent.GetComponent<HorizontalLayoutGroup>().padding.right = (int) (ScrollListViewport.GetComponent<RectTransform>().rect.width - (elementWidth + spacing));

        Elements[0].GetComponent<MiniGame>().OnSelected();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isDragging)
        {
            var currentScrollX = ScrollListContent.GetComponent<RectTransform>().anchoredPosition.x;
            var scrollOffset = currentScrollX - _scrollMin;
            var scrollNbElementOffset = Mathf.RoundToInt(scrollOffset / _elementWidthPlusSpacing);
            var newOffset = scrollNbElementOffset * _elementWidthPlusSpacing;
            var newPosition = _scrollMin + newOffset;
            newPosition = Mathf.Clamp(newPosition, _scrollMin, _scrollMin + _scrollRange);
            LerpToElement(newPosition);

            var elementIndexReversed = (Elements.Length - 1) - scrollNbElementOffset;
            if (elementIndexReversed != _previouslySelectedMiniGameIndex)
            {
                Elements[elementIndexReversed].GetComponent<MiniGame>().OnSelected();
                _previouslySelectedMiniGameIndex = elementIndexReversed;
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
    
    public void OnLevelSelectionClicked()
    {
        Elements[_previouslySelectedMiniGameIndex].GetComponent<MiniGame>().OnLevelSelectionClicked();
    }
}