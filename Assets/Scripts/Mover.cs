using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public LayerMask movementMask;
    private Camera camera;
    private CharacterMotor motor;

    void Start()
    {
        camera = Camera.main;
        motor = CharacterMotor.characterMotorInstance;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, movementMask))
            {
                motor.MoveToPoint(hit.point);
            }
        }
    }
}
