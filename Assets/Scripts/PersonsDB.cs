using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonsDB : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> persons;

    public  List<GameObject> Persons
    {
        get
        {
            return persons;
        }
    }

    public void AddToPersons(GameObject person)
    {
        persons.Add(person);
    }

    public void DeleteFromPersons(GameObject person)
    {
        persons.Remove(person);
    }
}
