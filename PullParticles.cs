using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullParticles : MonoBehaviour
{
    public static PullParticles instance = null;

    [SerializeField]
    private ParticleSystem ps = null;

    [SerializeField]
    private Transform destination = null;

    private ParticleSystem.Particle[] particlesArray = null;

    private float delay = 0;

    public void setParticlesToPull(ParticleSystem particleSystem)
    {
        ps = particleSystem;
        delay = ps.main.duration - 2;
    }

    private void Awake()
    {
        instance = this;
        if (ps != null)
        {

            delay = ps.main.duration - 2;
        }
    }

    private void LateUpdate()
    {
        
        if (delay > 0)
        {
            delay -= Time.deltaTime;
        }
        else
        {
            if(destination == null)
            {
                destination = Player.instance.transform;
            }

            if(ps != null)
            {
                if (particlesArray == null)
                {
                    particlesArray = new ParticleSystem.Particle[ps.main.maxParticles];
                }
                else
                {
                    int particlesCount = ps.GetParticles(particlesArray);

                    for (int i = 0; i < particlesCount; i++)
                    {

                        particlesArray[i].velocity = particlesArray[i].velocity * 0.5f;
                        if (Vector3.Distance(particlesArray[i].position, destination.position) > 0.1f)
                        {

                            particlesArray[i].position = Vector3.Lerp(particlesArray[i].position, destination.position, 0.1f);


                        }
                        else
                        {
                            particlesArray[i].remainingLifetime = -1;

                        }
                    }
                }

                ps.SetParticles(particlesArray);

            }
        }
    }
}
