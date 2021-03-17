using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public ParticleSystem particleSystem;

    public void SetParticleSystemState(bool state)
    {
        if (state)
            particleSystem.Play();
        else
            particleSystem.Stop();
    }
    
}
