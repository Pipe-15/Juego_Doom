using UnityEngine;

public class OldInput : MonoBehaviour
{
    // Se declaran las variables
    [HideInInspector] public float horizontal;
    [HideInInspector] public float vertical;

    // Se llama cada frame
    void Update()
    {
        // Se llaman los métodos para que funcionen
        GetInputFloat();
    }

    // Método para visibilizar el vector 2
    public void GetInputFloat()
    {
        // Se almacena el eje raw (-1, 0- 1) del eje horizontal y vertical (ver en Unity "Axis"), en las variables.
        horizontal = Input.GetAxisRaw("Horizontal");

        vertical = Input.GetAxisRaw("Vertical");
    }
}

