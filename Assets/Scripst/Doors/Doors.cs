using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    private Animator animator;
    [Tooltip("0 generica, 1 roja, 2 azul, 3 amarilla")]
    public int keyID;
    private bool hasKey;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        hasKey = false;
    }

    //Verifica si algo entro al Trigger
    private void OnTriggerEnter(Collider other)
    {
        GoKey(); 
        //Activa la animacion si entro el jugador y tiene la llave correcta
        if (other.CompareTag("Player") && hasKey)
        {
            animator.SetBool("IsOpen", true);
        }
    }

    //Verifica si algo salio del Trigger
    private void OnTriggerExit(Collider other)
    {
        //Si salio el player realiza la animacion
        if (other.CompareTag("Player"))
        {
            animator.SetBool("IsOpen", false);
        }
    }

    //Revisa si el jugador tiene la llave
    private void GoKey()
    {
        switch (keyID)
        {
            case 0:
                hasKey = true;
                break;
            case 1:
                hasKey = Stats.hasRedKey;
                break;
            case 2:
                hasKey = Stats.hasBlueKey;
                break;
            case 3:
                hasKey = Stats.hasYellowKey;
                break;
            default:
                break;

        }
    }

}
