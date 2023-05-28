using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public Camera camera;
    public Image frameImage;
    private Vector2 _startPoint;
    private Vector2 _endPoint;
    public Unit unit;
    public SelectableObject howered;
    public List<SelectableObject> listOfSelected = new List<SelectableObject>();

    private Unit[] _units;
    private void Start()
    {
        frameImage.enabled = false;
    }
    void Update()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 10f, Color.white);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) 
        {
            if (hit.collider.GetComponent<SelectableCollider>())
            {
//                howered = hit.collider.GetComponent<SelectableCollider>().SelectableObject;
//                howered.OnHover(1.1f);
            }
            else
            {
                if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
                {
                    {
                        UnSelectAll();
                    }
                }
                UnhoverCurrentObject(1f);
            }
        }
        else 
        { 
            UnhoverCurrentObject(1f);
        }

        if(howered != null)
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (listOfSelected.Contains(howered) == false)
                {
                    if (Input.GetKey(KeyCode.LeftControl) == false)
                    {
                        UnSelectAll();
                    }
                    listOfSelected.Add(howered);
                    howered.Select();
                }
            }
        }
        else
        {
            if (Input.GetMouseButtonUp(1))
            {
                if (hit.collider.CompareTag("Ground"))
                {
                    int rowNumber = Mathf.CeilToInt(Mathf.Sqrt(listOfSelected.Count));
                    for (int i = 0; i < listOfSelected.Count; i++)
                    {
                        int row = i / rowNumber;
                        int column = i % rowNumber;

                        Vector3 point = hit.point + new Vector3(row, 0, column);
                        listOfSelected[i].SetDestination(point);
                    }
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            _startPoint = Input.mousePosition;
            
        }
        if (Input.GetMouseButton(0)) // создание рамки
        {
            _endPoint = Input.mousePosition;
            frameImage.enabled = true;
            Vector2 min = Vector2.Min(_startPoint, _endPoint);
            Vector2 max = Vector2.Max(_startPoint, _endPoint);
            Vector2 size = max - min;
            frameImage.rectTransform.anchoredPosition = min;
            frameImage.rectTransform.sizeDelta = size;

            Rect rect = new Rect(min, size);
            _units = FindObjectsOfType<Unit>();
            for (int i = 0; i < _units.Length; i++)
            {
                Vector2 screen = camera.WorldToScreenPoint(_units[i].transform.position);
                if (rect.Contains(screen))
                {
                    if (listOfSelected.Contains(_units[i]))
                    {
                        
                    }
                    else
                    {
                        listOfSelected.Add(_units[i]);
                        _units[i].Select();
                    }
                }
                else
                {
                    _units[i].Unselect();
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            frameImage.enabled = false;
        }
        if(Physics.Raycast(ray, out hit))
        {
            if (hit.collider.GetComponent<Barracks>())
            {
                howered = hit.collider.GetComponent<Barracks>();
                howered.OnHover(1.1f);
                if (Input.GetMouseButtonUp(0))
                {
                    howered.Select();
                    if (listOfSelected.Contains(howered))
                    {
                        return;
                    }
                    listOfSelected.Add(howered);
                }
            }
            else
            {
                UnhoverCurrentObject(1f);
            }
        }
    }
    public void UnhoverCurrentObject(float modifier)
    {
        if (howered != null)
        {
            howered.UnHover(modifier);
            howered = null;
        }
    }
    public void UnSelectAll()
    {
        for (int i = 0; i < listOfSelected.Count; i++)
        {
            if(listOfSelected[i] != null)
            {
                listOfSelected[i].Unselect();
            }
        }
        listOfSelected.Clear();
    }
}