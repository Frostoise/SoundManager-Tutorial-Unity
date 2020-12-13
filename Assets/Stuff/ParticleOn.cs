using UnityEngine;
using UnityEngine.SceneManagement;

public class ParticleOn : MonoBehaviour
{

    AudioSource audioSource;
    public ParticleSystem particleS;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        var emission = particleS.emission;
        emission.enabled = audioSource.enabled && audioSource.isPlaying;
        
    }

}
