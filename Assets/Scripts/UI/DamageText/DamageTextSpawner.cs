using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI.DamageText
{
    public class DamageTextSpawner : MonoBehaviour
    {
        [SerializeField] DamageText damageTextPrefab = null;

        private void Start()
        {
            Spawn(11);
        }
        public void Spawn(float damage)
        {
            DamageText instance = Instantiate<DamageText>(damageTextPrefab, transform);
        }
    }
}