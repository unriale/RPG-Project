using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class DestroyAfterEffect : MonoBehaviour
    {
        private ParticleSystem particleSystem = null;
        private void Start()
        {
            particleSystem = GetComponent<ParticleSystem>();
        }
        private void Update()
        {
            if (!particleSystem.IsAlive())
                Destroy(gameObject);
        }
    }
}
