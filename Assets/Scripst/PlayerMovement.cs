using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed;
    public float rotationSpeed;
    public float gravity = -9.81f;

    private float currentRatation;

    private InputController inputController;
    private CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        inputController = GetComponent<InputController>();
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Rotation();
    }

    private void Rotation()
    {
        float rotationInput = inputController.moveVector.x * rotationSpeed * Time.deltaTime;
        currentRatation += rotationInput;
        transform.localRotation = Quaternion.AngleAxis(currentRatation, transform.up);
    }

    public void Movement()
    {
        Vector3 inputVector = new Vector3(0,0,inputController.moveVector.y);
        inputVector = transform.TransformDirection(inputVector);

        Vector3 finalMovement = (inputVector * speed) + (Vector3.up * gravity);
    
        characterController.Move(finalMovement * Time.deltaTime);
    
    }


}
