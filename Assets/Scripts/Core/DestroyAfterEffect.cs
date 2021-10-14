using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class DestroyAfterEffect : MonoBehaviour
    {
        [SerializeField] GameObject targetToDestroy = null;
        private ParticleSystem particleSystem = null;
        private void Start()
        {
            particleSystem = GetComponent<ParticleSystem>();
        }
        private void Update()
        {
            if (!particleSystem.IsAlive())
            {
                if (targetToDestroy != null)
                {
                    Destroy(targetToDestroy);
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
