using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TrapPicker : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    public List<GameObject> trapsIcons;
    [SerializeField]
    public int offset;
    [SerializeField]
    public int rotationSpeedModifier;

    private bool isPointerDown;
    private List<GameObject> menuItemList = new List<GameObject>();
    private Vector3 circleCenterPosition;
    private Vector3 circelLeftUpperCorner;
    private Queue<GameObject> disabledQueue = new Queue<GameObject>();

    private void Start()
    {
        var rectTransform = GetComponent<RectTransform>();
        circleCenterPosition = new Vector3(Screen.width, 0);
        circelLeftUpperCorner = circleCenterPosition - new Vector3(rectTransform.rect.width, -rectTransform.rect.height);

        if (trapsIcons.Count != 0)
            InitializeMenu(trapsIcons);

    }

    private void Update()
    {
        if (isPointerDown)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                if (Input.GetTouch(i).position.x > circelLeftUpperCorner.x && Input.GetTouch(i).position.y < circelLeftUpperCorner.y)
                {
                    if (Input.GetTouch(i).phase == TouchPhase.Moved)
                    {
                        Vector3 touchMoveDirection = Input.GetTouch(i).deltaPosition;
                        if (touchMoveDirection.x > 0 || touchMoveDirection.y > 0)
                        {
                            MoveMenuItemsClockwise(Vector3.one);
                        }
                        else
                        {
                            MoveMenuItemsCounterClockwise(Vector3.one);
                        }
                        break;
                    }
                }
            }
        }
    }

    private void MoveMenuItemsClockwise(Vector3 translation)
    {
        foreach (var go in menuItemList)
        {
            go.transform.RotateAround(circleCenterPosition, Vector3.back, translation.magnitude * rotationSpeedModifier);
            if (IsNotInBoundries(go))
            {
                go.SetActive(false);
                disabledQueue.Enqueue(go);
                if(disabledQueue.Count != 0)
                {
                    go.SetActive(true);
                    go.transform.eulerAngles = Vector3.zero;
                    go.GetComponent<RectTransform>().position = new Vector3(circelLeftUpperCorner.x + offset, 0, 0);
                }
            }

        }

    }

    private void MoveMenuItemsCounterClockwise(Vector3 translation)
    {
        foreach (var go in menuItemList)
        {
            go.transform.RotateAround(circleCenterPosition, Vector3.forward, translation.magnitude * rotationSpeedModifier);
            if (IsNotInBoundries(go))
            {
                go.SetActive(false);
                disabledQueue.Enqueue(go);
                if (disabledQueue.Count != 0)
                {
                    go.SetActive(true);
                    go.transform.eulerAngles = Vector3.zero;
                    go.GetComponent<RectTransform>().position = new Vector3(circleCenterPosition.x, circelLeftUpperCorner.y - offset, 0);
                }
            }
        }

    }

    private bool IsNotInBoundries(GameObject trapObject)
    {
        var trapRect = trapObject.GetComponent<RectTransform>();
        return trapRect.position.x - trapRect.rect.width/2 > circleCenterPosition.x ||
            trapRect.position.y + trapRect.rect.height/2 < 0;
    }

    public void InitializeMenu(List<GameObject> menuItems)
    {

        for (int i = 0; i < menuItems.Count; i++)
        {
            var trap = Instantiate(menuItems[i], transform);
            trap.GetComponent<RectTransform>().position = new Vector3(circelLeftUpperCorner.x + offset, 0, 0);
            menuItemList.Add(trap);

            if (i < 4)
            {
                foreach (var go in menuItemList)
                {
                    float radiusDivider = i == menuItems.Count - 1 ? menuItems.Count * 2 : menuItems.Count;
                    go.transform.RotateAround(circleCenterPosition, Vector3.back, menuItems.Count > 4 ? 22.5f : (90f / radiusDivider));

                }
            }
            else
            {
                trap.SetActive(false);
            }
        }
    }

    public void AddItemToMenu(GameObject menuItem)
    {
        menuItemList.Add(menuItem);
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        isPointerDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPointerDown = false;
    }
}