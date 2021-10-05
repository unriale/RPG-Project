using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed = 1f;
        [SerializeField] bool isHoming = false;
        [SerializeField] GameObject hitEffect = null;
        [SerializeField] float maxLifeTime = 15f;

        float damage = 0f;
        Health target = null;

        private void Start()
        {
            transform.LookAt(GetAimLocation());
        }

        private void Update()
        {
            if (target == null) return;
            if(isHoming && !target.IsDead())
                transform.LookAt(GetAimLocation());
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        public void SetTarget(Health target, float damage)
        {
            this.target = target;
            this.damage = damage;

            Destroy(gameObject, maxLifeTime);
        }

        public float GetDamage() => damage;
        private Vector3 GetAimLocation()
        {
            CapsuleCollider capsuleCollider = target.GetComponent<CapsuleCollider>();
            return target.transform.position + Vector3.up * capsuleCollider.height / 2;
        }

        private void OnTriggerEnter(Collider other)
        {
            Health character = other.GetComponent<Health>();
            if (!character) return;
            if (character != target) return;
            if (character.IsDead()) return;
            target.TakeDamage(damage);
            PlayHitEffect();
            Destroy(gameObject);
        }

        private void PlayHitEffect()
        {
            if (!hitEffect) return;
            Instantiate(hitEffect, GetAimLocation(), target.transform.rotation);
        }
    }
}