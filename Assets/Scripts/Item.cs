using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Items/Resources")]
public class Item : ScriptableObject
{
    public string Name;
    public GameObject GObject;
    public byte Amount;

}
