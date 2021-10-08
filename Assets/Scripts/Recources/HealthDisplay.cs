using UnityEngine;
using UnityEngine.UI;
using System;

namespace RPG.Resources
{
    public class HealthDisplay : MonoBehaviour
    {
        Health health;

        private void Awake()
        {
            health = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        }

        private void Update()
        {
            GetComponent<Text>().text = String.Format("{0:0.0}%", health.GetPercentage());
        }
    }
}