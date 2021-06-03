using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseHandler : MonoBehaviour
{
    private List<Action> _onMouseDownEvents = new List<Action>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            // Clicked on UI element that is on top of this object
            return;
        }

        foreach (var action in _onMouseDownEvents)
        {
            action();
        }
    }

    public void AddOnMouseDownEvent(Action onMouseDown)
    {
        _onMouseDownEvents.Add(onMouseDown);
    }
}
