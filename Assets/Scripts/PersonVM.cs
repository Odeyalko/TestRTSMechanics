using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonVM : MonoBehaviour
{
    private bool isSelected;

    private delegate void Select();
    Select delegateSelect;
    
    [SerializeField]
    private MeshRenderer renderer;

    private Color tempColor;
    private Color selectionColor;

    private void Start()
    {
        delegateSelect += ChangeMaterial;
        selectionColor = new Color(1, 1, 1, 0);
        tempColor = renderer.material.color;
    }

    public void Selected()
    {
        isSelected = true;
        delegateSelect.Invoke();
    }

    public void Deselected()
    {
        isSelected = false;
        delegateSelect.Invoke();
    }

    private void ChangeMaterial()
    {
        if (isSelected)
        {
            renderer.material.color = tempColor + selectionColor;
        }
        else if (!isSelected)
        {
            renderer.material.color = tempColor;
        }
    }
}
