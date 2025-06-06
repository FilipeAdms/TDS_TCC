using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackMusic : MonoBehaviour
{
    [SerializeField] private MahhorController mahhorController;
    [Header("Sounds")]
    public AudioClip MahhorDefaultMusic;
    public AudioClip MahhorMadnessMusic;


    [SerializeField] AudioSource backgroundSource;


    // Start is called before the first frame update
    void Start()
    {
        backgroundSource = GetComponent<AudioSource>();
        PlayBackGroundMusic();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayBackGroundMusic()
    {
        switch (mahhorController.currentTransformation)
        {
            case MahhorTransformation.Default:
                backgroundSource.clip = MahhorDefaultMusic;
                break;
            case MahhorTransformation.Madness:
                backgroundSource.clip = MahhorMadnessMusic;
                break;
            default:
                backgroundSource.clip = MahhorDefaultMusic;
                break;
        }
        backgroundSource.Play();
    }
}
