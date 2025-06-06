using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MahhorSound : MonoBehaviour
{
    public BackMusic BackMusic; // Referência ao controlador de música de fundo
    [Header("Sounds")]
    public AudioClip sandFootsteps;
    public AudioClip TrasnformationSound;
    public AudioClip DeathSound;
    public List<AudioClip> hurtSound;

    [SerializeField] AudioSource footstepSource;
    [SerializeField] AudioSource hurtSource;
    [SerializeField] AudioSource TrasnformationSource;
    [SerializeField] AudioSource DeathSource;

    private AudioClip clip;

    // Start is called before the first frame update
    void Start()
    {
        footstepSource = GetComponents<AudioSource>()[0];
        hurtSource = GetComponents<AudioSource>()[1];
        TrasnformationSource = GetComponents<AudioSource>()[2];
        DeathSource = GetComponents<AudioSource>()[3];
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChanceMusic()
    {
        BackMusic.PlayBackGroundMusic();
    }
    public void PlayHurt()
    {
        clip = hurtSound[Random.Range(0, hurtSound.Count)];
        hurtSource.clip = clip;
        hurtSource.volume = Random.Range(0.1f, 5f);
        hurtSource.pitch = Random.Range(0.75f, 2f);
        hurtSource.Play();
    }
    public void PlayFootstep()
    {
        footstepSource.clip = sandFootsteps;
        footstepSource.volume = Random.Range(0.1f, 5f);
        footstepSource.pitch = Random.Range(0.75f, 2f);
        footstepSource.Play();
    }
    public void PlayDeath()
    {
        footstepSource.clip = DeathSound;
        footstepSource.volume = Random.Range(0.1f, 5f);
        footstepSource.pitch = Random.Range(0.75f, 2f);
        footstepSource.Play();
    }

    public void PlayTransformation()
    {
        TrasnformationSource.clip = TrasnformationSound;
        TrasnformationSource.pitch = Random.Range(0.65f, 1f);
        TrasnformationSource.Play();
    }
}
