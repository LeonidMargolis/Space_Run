using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    private bool isPlaying = false;
    private ParticleSystem particleObject;

    void Start()
    {
        particleObject = GetComponent<ParticleSystem>();
        particleObject.Stop();
    }

    void Update()
    {
        if (isPlaying)
        {
            particleObject.Play();
            isPlaying = false;
        }
    }

    public void ParticlePlay()
    {
        isPlaying = true;
    }
}
