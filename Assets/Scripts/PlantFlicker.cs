using System;
using System.Collections.Generic;
using UnityEngine.Serialization;
using UnityEngine.Rendering;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlantFlicker : MonoBehaviour
{
    private Light2D plantLight;
    public float lightMultiplier = 2;
    public float amplitude;
    private float frequency, angularFrequency, elapsedTime, period, phase;


    void Start()
    {
        plantLight = GetComponent<Light2D>();
        period = Random.Range(5, 10);
        phase = Random.Range(0, 360);
    }


    void Update()
    {
        plantLight.intensity = SmoothSineWave() * lightMultiplier + 1;
    }

    float SmoothSineWave()
    {
        // y(t) = A * sin(ωt + θ) [Basic Sine Wave Equation]
        // [A = amplitude | ω = AngularFrequency ((2*PI)f) | f = 1/T | T = [period (s)] | θ = phase | t = elapsedTime]
        // Public/Serialized Variables: amplitude, period, phase
        // Private/Non-serialized Variables: frequency, angularFrequency, elapsedTime
        // Local Variables: omegaProduct, y

        // If the value of period has altered last known frequency...
        if (1 / (period) != frequency)
        {
            // Recalculate frequency & omega.
            frequency = 1 / (period);
            angularFrequency = (2 * Mathf.PI) * frequency;
        }
        // Update elapsed time.
        elapsedTime += Time.deltaTime;
        // Calculate new omega-time product.
        float omegaProduct = (angularFrequency * elapsedTime);
        // Plug in all calculated variables into the complete Sine wave equation.
        float y = (amplitude * Mathf.Sin(omegaProduct + phase));
        // 
        return y;
    }
}


