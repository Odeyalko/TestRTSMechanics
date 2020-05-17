using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Builder : MonoBehaviour
{
    private Ray ray;
    private RaycastHit hit;
    private Camera camera;
    
    private bool isBuild = false;
    private bool isRotate = false;

    
    private GameObject selectedMask;

    private int selectedIndex = 0;

    //набор масок
    public GameObject[] BuildMasks;


    //набор объектов-не масок
    public GameObject[] Buildings;

    private void Start()
    {
        camera = Camera.main;
    }


    void Update()
    {
        ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Input.GetKeyDown("1"))
        {
            selectedIndex = 0;
            selectedMask = BuildMasks[selectedIndex];
            isBuild = true;
        }

        if (Input.GetMouseButtonUp(0) && isBuild && isRotate && BuildMasks[selectedIndex].GetComponent<BuildChecker>().placeIsFree)
        {
            Instantiate(Buildings[selectedIndex], selectedMask.transform.position, selectedMask.transform.rotation);
            selectedMask.transform.position = Vector3.zero;
            selectedMask = null;
            isBuild = false;
            isRotate = false;
        }

        if(Input.GetMouseButtonUp(0)&& isBuild || isRotate && isBuild)
        {
            isRotate = true;
            Vector3 startRotation = selectedMask.transform.position;
            selectedMask.transform.position = startRotation;
            Physics.Raycast(ray, out hit);

            selectedMask.transform.LookAt(hit.point, -Vector3.up);
            selectedMask.transform.localEulerAngles = new Vector3(0, selectedMask.transform.localEulerAngles.y, 0);
            
        }
        
        
        

        
        if (isBuild&&!isRotate)
        {
            
            if (Physics.Raycast(ray, out hit))
            {
                selectedMask.transform.position = hit.point;
            }
        }
    }
}

 
