using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{


    [Header("Sounds")]
    public AudioClip sandFootsteps;
    public List<AudioClip> hurtSound;

    [SerializeField] AudioSource footstepSource;
    [SerializeField] AudioSource hurtSource;

    private AudioClip clip;

    // Start is called before the first frame update
    void Start()
    {
        footstepSource = GetComponents<AudioSource>()[0];
        hurtSource = GetComponents<AudioSource>()[1];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayHurt()
    {
        clip = hurtSound[Random.Range(0, hurtSound.Count)];
        footstepSource.clip = clip;
        footstepSource.volume = Random.Range(0.1f, 5f);
        footstepSource.pitch = Random.Range(0.75f, 2f);
        footstepSource.Play();
    }
    public void PlayFootstep()
    {
        footstepSource.clip = sandFootsteps;
        footstepSource.volume = Random.Range(0.1f, 5f);
        footstepSource.pitch = Random.Range(0.75f, 2f);
        footstepSource.Play();
    }
}
