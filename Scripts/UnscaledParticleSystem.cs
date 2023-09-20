using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnscaledParticleSystem : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale < 0.01f)
        {
            GetComponent<ParticleSystem>().Simulate(Time.unscaledDeltaTime, true, false);
        }
    }
}

