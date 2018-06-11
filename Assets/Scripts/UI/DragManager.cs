using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragManager : MonoBehaviour {
    [SerializeField]
    private static bool Drag;
    private void Awake()
    {
        Drag = false;
    }
    public bool IsDragging()
    {
        return Drag;
    }

    public void SetDragAction(bool value)
    {
        Drag = value;
    }


}
