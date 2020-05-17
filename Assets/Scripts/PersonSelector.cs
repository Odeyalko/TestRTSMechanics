using System.Collections.Generic;
using UnityEngine;

public class PersonSelector : MonoBehaviour
{
    private PersonsDB personsDB;
    private List<GameObject> tmpPersons;

    private Camera camera;

    private Ray ray;
    private RaycastHit hit;

    public LayerMask selectable;

    private List<GameObject> selected;
    CharacterMotor characterMotor;

    private PersonVM personVM;
    private GameObject tmpGameObject;

    private Rect rect;

    private Vector2 startPosition;
    private Vector2 ongoingPosition;
    private Vector2 PersonScreenPosition;

    private bool draw;

    void Start()
    {
        camera = Camera.main;
        selected = new List<GameObject>();
        characterMotor = CharacterMotor.characterMotorInstance;
        personsDB = GetComponent<PersonsDB>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ClickSelection();
        }
    }
       
    private void ClickSelection()
    {
        ray = camera.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit, 1000f))
        {
            tmpGameObject = hit.transform.gameObject;

            //click on empty field
            if (!hit.transform.gameObject.GetComponent<PersonVM>())
            {
                ClearListOfPersons();
                return;
            }

            personVM = hit.transform.gameObject.GetComponent<PersonVM>();

            if ((!selected.Contains(tmpGameObject))&&(Input.GetKey(KeyCode.LeftControl)))
            {
                Select();
            }
            else if(!selected.Contains(tmpGameObject))
            {
                ClearListOfPersons();
                Select();
            }
            else
            {
                Deselect();
            }
        }
    }

    private void Select()
    {
        personVM.Selected();
        selected.Add(tmpGameObject);
        characterMotor.AddAgentsToList(tmpGameObject);
    }

    private void Deselect()
    {
        personVM.Deselected();
        selected.Remove(tmpGameObject);
        characterMotor.RemoveFromAgents(tmpGameObject);
    }

    private void ClearListOfPersons()
    {
        foreach(GameObject person in selected)
        {
            person.GetComponent<PersonVM>().Deselected();
            characterMotor.ClearAgents();
        }
        selected.Clear();
    }

    private void OnGUI()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            if (selected.Count != 0)
            {
                Deselect();
            }
            startPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            draw = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            draw = false;
            if (startPosition != ongoingPosition)
            {
                RectSelection();
            }
        }

        if (draw)
        {
            ongoingPosition = Input.mousePosition;
            rect = new Rect(Mathf.Min(ongoingPosition.x, startPosition.x),
                            Screen.height - Mathf.Max(ongoingPosition.y, startPosition.y),
                            Mathf.Max(ongoingPosition.x, startPosition.x) - Mathf.Min(ongoingPosition.x, startPosition.x),
                            Mathf.Max(ongoingPosition.y, startPosition.y) - Mathf.Min(ongoingPosition.y, startPosition.y)
                            );
            GUI.Box(rect, "");
        }
    }

    private void RectSelection()
    {
        tmpPersons = personsDB.Persons;

        foreach(GameObject temp in tmpPersons)
        {
            PersonScreenPosition = new Vector2(camera.WorldToScreenPoint(temp.transform.position).x, Screen.height - camera.WorldToScreenPoint(temp.transform.position).y);
            if (rect.Contains(PersonScreenPosition))
            {
                tmpGameObject = temp;
                personVM = temp.GetComponent<PersonVM>();
                Select();
            }
        }
        rect = Rect.zero;
    }
}