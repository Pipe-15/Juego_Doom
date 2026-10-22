using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    public static KeyManager instance;

    public GameObject redKeyImage;
    public GameObject blueKeyImage;
    public GameObject yellowKeyImage;

    // Hace que solo exista una instancia
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    //Inicia las variables de las llaves e imagenes en falso
    void Start()
    {
        Stats.hasBlueKey = false;
        Stats.hasRedKey = false;
        Stats.hasYellowKey = false;

        redKeyImage.SetActive(false);
        blueKeyImage.SetActive(false);
        yellowKeyImage.SetActive(false);
    }

}
