using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioSource BackgroundMusic;      //Drag a reference to the audio source which will play the music.
    public static AudioSource efxSound;             //Drag a reference to the audio source which will play the sound effects.
    public static SoundManager instance = null;     //Allows other scripts to call functions from SoundManager.             
    public float lowPitchRange = .95f;              //The lowest a sound effect will be randomly pitched.
    public float highPitchRange = 1.05f;            //The highest a sound effect will be randomly pitched.

    public AudioClip InteractSound;
    public AudioClip ActionSound;

    // Start is called before the first frame update
    void Start()
    {
        BackgroundMusic = GetComponent<AudioSource>();
        efxSound = GetComponent<AudioSource>();
        BackgroundMusic.Play();
    }

    private void Update()
    {

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
        /** //Set the clip of our efxSource audio source to the clip passed in as a parameter.
        efxSound.clip = clip;

        //Reemplaza y reproduce el sonido de fondo.
        efxSound.Play(); **/

        //Reproducir una vez.
        efxSound.PlayOneShot(clip);

        // Opcional para cambiar la intensidad con la que suena
        //efxSound.PlayOneShot(clip, 0.7F);

    }

    /**
    //RandomizeSfx chooses randomly between various audio clips and slightly changes their pitch.
    public void RandomizeSfx(params AudioClip[] clips)
    {
        //Generate a random number between 0 and the length of our array of clips passed in.
        int randomIndex = Random.Range(0, clips.Length);

        //Choose a random pitch to play back our clip at between our high and low pitch ranges.
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        //Set the pitch of the audio source to the randomly chosen pitch.
        efxSound.pitch = randomPitch;

        //Set the clip to the clip at our randomly chosen index.
        efxSound.clip = clips[randomIndex];

        //Play the clip.
        efxSound.Play();
    } **/

    // Aqui agregar diferentes sonidos
    public static void SetClip(string str) {
        if (str.Equals("I"))
        {
            PlaySingle(SoundManager.instance.InteractSound);
        }
        else if (str.Equals("A")) {
            PlaySingle(SoundManager.instance.ActionSound);
        }
    }

}
