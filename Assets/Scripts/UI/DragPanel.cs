using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragPanel : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler {

    private Vector3 Offset;
    private Vector3 SnapPosition;
    private Transform ParentTransform;
    private DragManager dragManager;
    private bool dragInitiated;
    public bool isDraggable;


    private void Start()
    {
        dragManager = GameObject.Find("DragManager").GetComponent<DragManager>();
        isDraggable = true;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!isDraggable && dragInitiated) return;
        dragInitiated = true;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(eventData.position);
        Offset = new Vector3(transform.position.x - mousePosition.x, transform.position.y - mousePosition.y, 0);
        SnapPosition = transform.position;
        ParentTransform = transform.parent.gameObject.transform;
        transform.SetParent(GameObject.Find("DragHolder").transform);
        transform.position += new Vector3(0, 0, 10f);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isDraggable && dragInitiated) return;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(eventData.position);
        
        gameObject.transform.position = new Vector3(mousePosition.x + Offset.x, mousePosition.y + Offset.y, transform.position.z);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isDraggable && dragInitiated) return;
        List<RaycastResult> list = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, list);
        foreach (var obj in list)
        {
            if (obj.gameObject.GetComponent<HeroPanel>() && obj.gameObject != gameObject)
            {
                dragManager.SetDragAction(true);
                SwapObjects(obj.gameObject);
                return;
            }

        }
        dragInitiated = false;
        transform.SetParent(ParentTransform);
        transform.position = SnapPosition;
        transform.SetAsFirstSibling();
    }

    public void SwapObjects(GameObject obj)
    {
        transform.SetParent(obj.transform.parent);
        obj.transform.SetParent(ParentTransform);

        StartCoroutine(MoveObjectTo(this.gameObject, obj.transform.position));
        StartCoroutine(MoveObjectTo(obj, SnapPosition));
    }

    IEnumerator MoveObjectTo(GameObject objectToMove, Vector3 target)
    {
        //Transform dragHolder = GameObject.Find("DragHolder").transform;
        objectToMove.GetComponent<DragPanel>().isDraggable = false;
        Transform actualParent = objectToMove.transform.parent;
        objectToMove.transform.SetParent(GameObject.Find("DragHolder").transform);
        Vector3 startingPoint = objectToMove.transform.position;
        float speed = 35f;
        float step = speed / (startingPoint - target).magnitude * Time.deltaTime;
        float t = 0;
        while (t <= 1f)
        {
            t += step;
            objectToMove.transform.position = Vector3.Lerp(startingPoint, target, t);
            yield return new WaitForEndOfFrame();
        }
        dragManager.SetDragAction(false);
        dragInitiated = false;
        objectToMove.GetComponent<DragPanel>().isDraggable = true;
        objectToMove.transform.SetParent(actualParent);
        objectToMove.transform.SetAsFirstSibling();
    }
}
