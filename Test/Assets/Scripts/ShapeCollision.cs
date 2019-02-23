using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeCollision : MonoBehaviour
{
    public string Tag;
    public GameObject AudioObject;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Im here.");
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
        col.gameObject.GetComponent<MoveableShape>().OnMouseUp();
        // Play sound
        AudioObject.GetComponent<AudioScript>().PlaySuccessful();
        // Play animation
        col.gameObject.GetComponent<Animator>().SetTrigger("OnPlacedCorrectly");
    }

    void OnPlacedIncorrectly(Collision2D col)
    {
        // Stop dragging the shape!
        col.gameObject.GetComponent<MoveableShape>().OnMouseUp();
        // Go to start position
        col.gameObject.GetComponent<MoveableShape>().MoveToStartPos();
        // Play sound
        AudioObject.GetComponent<AudioScript>().PlayFail();
        // Play animation
        col.gameObject.GetComponent<Animator>().SetTrigger("OnPlacedIncorrectly");
    }
}
