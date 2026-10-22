using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public int keyType;

    //Valida que el jugador atraveso la llave
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switch (keyType)  //Segun la llave que recoge hace alguna acción
            {
                case 0:
                    KeyManager.instance.redKeyImage.SetActive(true);
                    Stats.hasRedKey = true;
                    break;
                case 1:
                    KeyManager.instance.blueKeyImage.SetActive(true);
                    Stats.hasBlueKey = true;
                    break;
                case 2:
                    KeyManager.instance.yellowKeyImage.SetActive(true);
                    Stats.hasYellowKey = true;
                    break;
                default:
                    break;
            }
            Destroy(gameObject);
        }
    }
}
