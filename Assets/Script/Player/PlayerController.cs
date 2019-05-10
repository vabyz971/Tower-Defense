using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private float movementSpeed = 7;

    void Start()
    {

    }

    void Update()
    {
        HandleMovementInput();
        HandleRotationInput();
    }

    void HandleMovementInput()
    {
        float _horizontale = Input.GetAxis("Horizontal");
        float _verticale = Input.GetAxis("Vertical");

        Vector3 _movement = new Vector3(_horizontale, 0, _verticale);
        transform.Translate(_movement * movementSpeed * Time.deltaTime, Space.World);
    }

    void HandleRotationInput()
    {
        RaycastHit _hit;
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(_ray, out _hit))
        {
            transform.LookAt(new Vector3(_hit.point.x, transform.position.y, _hit.point.z));
        }
    }

}
