using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeCollision : MonoBehaviour
{
    public string Tag;
    public GameObject AudioObject;
    public Animator Animator;

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
        col.gameObject.GetComponent<MoveableShape>().IsDraggable = false;
        AudioObject.GetComponent<AudioScript>().PlaySuccessful();
        Animator.SetTrigger("OnPlacedCorrectly");
    }

    void OnPlacedIncorrectly(Collision2D col)
    {
        col.gameObject.GetComponent<MoveableShape>().IsDraggable = false;
        col.gameObject.GetComponent<MoveableShape>().MoveToStartPos();
        //col.gameObject.GetComponent<MoveableShape>().IsDraggable = true;
        AudioObject.GetComponent<AudioScript>().PlayFail();
        Animator.SetTrigger("OnPlacedIncorrectly");
    }
}
