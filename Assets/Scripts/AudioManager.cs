
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Singleton para llamar al manager desde otros scripts
    public static AudioManager instance;

    // Almacena el AudioSource de los efectos de sonido
    private AudioSource SFXSource;

    // Guarda el sonido del enemigo al morir
    public AudioClip enemyDamage;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }

    private void Start()
    {
        SFXSource = GetComponent<AudioSource>();
    }

    // Se crea el metodo para el dano del enemigo
    public void PlayEnemyDamage() { PlayAudio(enemyDamage); }

    // Metodo base para reproducir en el AudioSource de los efectos, el clip que esta entre parentesis
    private void PlayAudio(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
