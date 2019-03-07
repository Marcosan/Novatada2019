using UnityEngine;

public class SoundManagement : MonoBehaviour
{
    public static AudioSource musicPlayer;

    // Start is called before the first frame update
    void Start()
    {
        musicPlayer = GetComponent<AudioSource>();
        musicPlayer.Play();
    }
    
    // Update is called once per frame
    void Update()
    {

    }

}
