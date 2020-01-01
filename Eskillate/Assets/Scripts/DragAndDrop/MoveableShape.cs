using UnityEngine;

namespace DragAndDrop
{
    public class MoveableShape : MonoBehaviour
    {
        private Vector3 _screenPoint;
        private Vector3 _offset;

        // Indicates if user is currently dragging the shape
        private bool _isDragging;
        // Indicates if user can drag the shape
        private bool _isDraggable = true;

        private Vector3 _startPos;

        private RectTransform _rectTransform;

        public GameObject GameController;

        // Start is called before the first frame update
        void Start()
        {
            _rectTransform = GetComponent<RectTransform>();

            var screenBorderSafetyDistX = GetComponent<RectTransform>().localScale.x / 2.0f;
            var screenBorderSafetyDistY = GetComponent<RectTransform>().localScale.y / 2.0f;

            var xRangeMin = -Screen.width / 2 + screenBorderSafetyDistX;
            var xRangeMax = -screenBorderSafetyDistX;

            var yRangeMin = -Screen.height / 2 + screenBorderSafetyDistY;
            var yRangeMax = Screen.height / 2 - screenBorderSafetyDistY;

            var x = Random.Range(xRangeMin, xRangeMax);
            var y = Random.Range(yRangeMin, yRangeMax);

            var originalZ = transform.position.z;
            _startPos = new Vector3(x, y, originalZ);

            _rectTransform.anchoredPosition = _startPos;

            var firstR = Random.Range(0.0f, 1.0f);
            var firstG = Random.Range(0.0f, 1.0f);
            var firstB = Random.Range(0.0f, 1.0f);
            var secondR = Random.Range(0.0f, 1.0f);
            var secondG = Random.Range(0.0f, 1.0f);
            var secondB = Random.Range(0.0f, 1.0f);

            var renderer = GetComponent<Renderer>();
            renderer.material.SetColor("_FirstColor", new Color(firstR, firstG, firstB));
            renderer.material.SetColor("_SecondColor", new Color(secondR, secondG, secondB));

            GameController = GameObject.Find("GameController");
        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnMouseDown()
        {
            if (!_isDraggable)
                return;

            // Indicate that we are dragging the shape from now on
            _isDragging = true;

            _screenPoint = Camera.main.WorldToScreenPoint(transform.position);

            _offset = transform.position - Camera.main.ScreenToWorldPoint(
                new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z));
        }

        void OnMouseDrag()
        {
            if (!_isDragging)
                return;

            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z);


            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + _offset;
            transform.position = curPosition;
        }

        private void OnMouseUp()
        {
            // Indicate that we are not dragging anymore
            _isDragging = false;
        }

        private void MoveToStartPos()
        {
            _rectTransform.position = _startPos;
        }

        public void OnPlacedCorrectly()
        {
            OnMouseUp();
            _isDraggable = false;
            GameController.GetComponent<GameController>().OnPlacedCorrectly();
        }

        public void OnPlacedIncorrectly()
        {
            OnMouseUp();
            MoveToStartPos();
        }

        public void Restart()
        {
			MoveToStartPos();
			_isDraggable = true;
			_isDragging = false;
        }
    }
}