using UnityEngine;

public class BuildChecker : MonoBehaviour
{
    public bool placeIsFree;
    private MeshRenderer objectMaterial;

    private Color cantBuild = Color.red;
    private Color canBuild = Color.green;

    private void Start()
    {
        objectMaterial = this.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>();
        placeIsFree = true;
    }

    private void ChangeMaterial()
    {
        if(placeIsFree)
        {
            objectMaterial.material.color = canBuild;
        }
        else
        {
            objectMaterial.material.color = cantBuild;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        placeIsFree = false;
        ChangeMaterial();
    }

    private void OnTriggerExit(Collider other)
    {
        placeIsFree = true;
        ChangeMaterial();
    }
}
