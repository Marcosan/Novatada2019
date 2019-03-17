using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static AudioSource BackgroundMusic;      // Audio Source para referenciar a la musica de fondo.
    public static AudioSource efxSound;             // Audio Source para referenciar el sonido de fondo.

    private static SoundManager instance = null;    // Allows other scripts to call functions from SoundManager.     
    
    public AudioClip InteractSound;                 // Audio para interaccion
    public AudioClip ActionSound;                   // Audio para Accion
    
    public AudioClip[] musicList;                   // Array de AudioClips para agregar musica de fondo, se llaman desde AudioCanvas
    public string[] Escenas;                        // Array que almacena los nombres de todas las escenas existentes

    // Start is called before the first frame update
    void Start()
    {
        CargarEscenas();                            // Carga el nombre de todas las escenas existentes, el mismo orden esta en File > Build Settings (Se ingresa desde el editor de unity)

        BackgroundMusic = GetComponent<AudioSource>();
        efxSound = GetComponent<AudioSource>();

        BackgroundMusic.volume = 0.70F;             // Volumen de 0.0 a 1.0 para la musica de fondo
        BackgroundMusic.loop = true;                // Para que se repita el audio
        BackgroundMusic.enabled = true;             // Para que se active de ser necesario
        SetBackground();                            // Para asignarle un audio dependiendo de la escena en la que se encuentra
        BackgroundMusic.mute = true;                // Para que no se note el pequeño margen de error cuando se ajusta el audio
    }

    private void Update()
    {
        // Si la musica no se esta reproduciendo desde aqui se verifica, asigna un audio, y reproduce nuevamente
        if (!BackgroundMusic.isPlaying)
        {
            SetBackground();
            if (BackgroundMusic.clip != null)
            {
                BackgroundMusic.Play();
            }
        }
        else {
        }
    }

    void Awake()
    {
        // Check if there is already an instance of SoundManager
        if (instance == null)
            // if not, set it to this.
            instance = this;
        // If instance already exists:
        else if (instance != this)
            // Destroy this, this enforces our singleton pattern so there can only be one instance of SoundManager.
            Destroy(gameObject);

        // Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
        DontDestroyOnLoad(gameObject);
    }

    //Used to play single sound clips.
    public static void PlaySingle(AudioClip clip)
    {
        /** // Asignas un clip al Audio Source de efectos.
        efxSound.clip = clip;

        // Reemplaza y reproduce el sonido de fondo.
        efxSound.Play(); **/

        // Reproducir una vez.
        efxSound.PlayOneShot(clip);

        // Opcional para cambiar la intensidad con la que suena
        //efxSound.PlayOneShot(clip, 0.7F);

    }
    
    // Asignar un audio para los efectos dependiendo de la accion
    public static void SetClip(string str) {
        // "I" para Interactuar
        if (str.Equals("I"))
        {
            PlaySingle(SoundManager.instance.InteractSound);
        }
        // "A" para Accion
        else if (str.Equals("A")) {
            PlaySingle(SoundManager.instance.ActionSound);
        }
    }

    // Asignar un audio de fondo dependiendo del escenario.
    public void SetBackground() {
        // Orden de las escenas en el array se ven desde File>Build Settings
        if (SceneManager.GetActiveScene().name == Escenas[0])
        {
            BackgroundMusic.clip = musicList[0];
        }
        else if (SceneManager.GetActiveScene().name == Escenas[2])
        {
            BackgroundMusic.clip = musicList[1];
        }
        else {
            BackgroundMusic.clip = musicList[2];
        }
    }

    // Para cuando se cambie de escena: se para la musica de fondo, asigna nulo al clip de fondo,
    // y se quita el modo de mute al fondo.
    // Automaticamente el update() reconocera que no hay musica reproduciendose y se encargara de
    // asignarle un audio y de reproducirlo.
    public static void ChangeMusic()
    {
        if (BackgroundMusic.clip != null || BackgroundMusic.isPlaying ) { 
            BackgroundMusic.Stop();
            BackgroundMusic.clip = null;
            BackgroundMusic.mute = false;
        }
    }

    // Para que se cargue una lista con todas las escenas que se encuentran agregadas en Build Settings
    // Deben agregarse ahi siempre que se crean nuevas escenas, ahi esta el orden de como se cargaran.
    public void CargarEscenas() {
        int sceneCount = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings;
        Escenas = new string[sceneCount];
        for (int i = 0; i < sceneCount; i++)
        {
            Escenas[i] = System.IO.Path.GetFileNameWithoutExtension(UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex(i));
        }
    }

}
