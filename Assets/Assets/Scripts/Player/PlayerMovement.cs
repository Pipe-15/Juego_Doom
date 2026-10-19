using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private OldInput _OldInput;
    private CharacterController _CC;
    public float Speed;
    public float rotationSpeed;
    public float gravity = -9.81f;
    public float currentLookingPos;
    public Animator _anim;
    // Start is called before the first frame update
    void Start()
    {
        _OldInput = GetComponent<OldInput>();
        _CC = GetComponent<CharacterController>();
     
    }

    // Update is called once per frame
    void Update()
    {
      Movement();
      Rotation();
      CamAnim();
    }
    public void Movement()
    { 
     Vector3 inputVector = new Vector3(0,0,_OldInput.vertical);
     inputVector = transform.TransformDirection(inputVector);
     Vector3 movementVector = ((inputVector * Speed) + Vector3.up * gravity);

        _CC.Move(movementVector * Time.deltaTime);
    }

    public void Rotation()
    {  
        float rotationInput = _OldInput.horizontal * rotationSpeed * Time.deltaTime;
        currentLookingPos += rotationInput;
        transform.localRotation = Quaternion.AngleAxis(currentLookingPos,transform.up);
    }
    public void CamAnim()
    { 
    //_anim.SetBool("isMoving",_OldInput.vertical != 0);
    }

}
