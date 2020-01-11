using Core;
using UnityEngine;

namespace DragAndDrop
{
    public class ShapeCollision : MonoBehaviour
    {
        public string Tag;
        public GameObject AudioObject;

        // Start is called before the first frame update
        void Start()
        {
            AudioObject = GameObject.Find("AudioObject");
        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnCollisionEnter2D(Collision2D col)
        {
            if (col.collider.GetType() == typeof(CircleCollider2D))
            {
                if (col.gameObject.tag == Tag)
                {
                    OnPlacedCorrectly(col);
                }
                else
                {
                    OnPlacedIncorrectly(col);
                }
            }
        }

        void OnPlacedCorrectly(Collision2D col)
        {
            // Stop dragging the shape!
            col.gameObject.GetComponent<MoveableShape>().OnPlacedCorrectly();
            // Play sound
            AudioObject.GetComponent<AudioScript>().PlaySuccessful();
        }

        void OnPlacedIncorrectly(Collision2D col)
        {
            // Stop dragging the shape!
            col.gameObject.GetComponent<MoveableShape>().OnPlacedIncorrectly();
            // Play sound
            AudioObject.GetComponent<AudioScript>().PlayFail();
        }
    }
}