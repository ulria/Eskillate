using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MiniGameSelection : MonoBehaviour
{
    private bool isDragging = false;
    private float _elementWidthPlusSpacing = 0.0f;
    private float _scrollRange = 0.0f;
    private float _scrollMin = 0.0f;
    private int _previouslySelectedMiniGameIndex = -1;

    private GameObject ScrollListContent;
    private GameObject ScrollListViewport;
    public GameObject[] Elements;

    // Start is called before the first frame update
    void Start()
    {
        // Initializin game objects
        ScrollListContent = GameObject.Find("ScrollListContent");
        ScrollListViewport = GameObject.Find("ScrollListViewport");

        // Initializing UI components
        var elementWidth = ScrollListContent.transform.GetChild(0).GetComponent<RectTransform>().rect.width;
        var spacing = ScrollListContent.GetComponent<HorizontalLayoutGroup>().spacing;
        _elementWidthPlusSpacing = elementWidth + spacing;

        _scrollRange = (Elements.Length-1) * _elementWidthPlusSpacing;
        _scrollMin = ScrollListContent.GetComponent<RectTransform>().anchoredPosition.x - _scrollRange;
        _scrollMin /= 2;

        ScrollListContent.GetComponent<HorizontalLayoutGroup>().padding.right = (int) (ScrollListViewport.GetComponent<RectTransform>().rect.width - (elementWidth + spacing));

        var newContentPos = new Vector2(_scrollMin + _scrollRange, 0);
        ScrollListContent.GetComponent<RectTransform>().anchoredPosition = newContentPos;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isDragging)
        {
            // Find out which MiniGame is in the Selection spot
            var currentScrollX = ScrollListContent.GetComponent<RectTransform>().anchoredPosition.x;
            var scrollOffset = currentScrollX - _scrollMin;
            var scrollNbElementOffset = Mathf.RoundToInt(scrollOffset / _elementWidthPlusSpacing);
            scrollNbElementOffset = Mathf.Clamp(scrollNbElementOffset, 0, Elements.Length-1);
            var newOffset = scrollNbElementOffset * _elementWidthPlusSpacing;
            var newPosition = _scrollMin + newOffset;
            // Snap to this MiniGame
            LerpToElement(newPosition);

            // Invert index, because, for some reason index 0 represents the left most element
            var elementIndexReversed = (Elements.Length - 1) - scrollNbElementOffset;
            // Call the OnSelected method of the MiniGame in the selection spot
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
        Vector2 newPos = new Vector2(newX, 0);
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

    public void OnBackToMainMenuClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }
}