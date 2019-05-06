using UnityEngine;

namespace LowPop
{
    public class PoppableScript : MonoBehaviour
    {
        public float Value;
        private GameObject _gameController;

        // Start is called before the first frame update
        void Start()
        {
            _gameController = GameObject.Find("GameController");
        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnMouseDown()
        {
            var successful = _gameController.GetComponent<GameController>().OnPopped(Value);
            if (successful)
            {
                var renderers = gameObject.GetComponentsInChildren<Renderer>();
                foreach(var renderer in renderers)
                {
                    renderer.enabled = false;
                }
            }
        }
    }
}