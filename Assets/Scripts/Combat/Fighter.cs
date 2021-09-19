using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;
using System;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks = 1f;

        Health target;
        Animator animator;
        float timeSinceLastAttack = Mathf.Infinity;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }


        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null) return;

            if (target.IsDead()) return;

            if (!GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(target.transform.position, 1f);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            if (timeSinceLastAttack >= timeBetweenAttacks)
            {
                //This will trigger the Hit() event
                AttackTrigger();
                timeSinceLastAttack = 0;
            }     
        }

        private void AttackTrigger()
        {
            animator.ResetTrigger("stopAttack");
            animator.SetTrigger("attack");
        }

        /// <summary>
        /// Check if a player is too close to the target.
        /// </summary>
        /// <returns>True if a player is too close to the target, false otherwise.</returns>
        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) <= weaponRange;
        }

        private void StopAttack()
        {
            animator.ResetTrigger("attack");
            animator.SetTrigger("stopAttack");
        }

        public void Cancel()
        {
            StopAttack();
            target = null;
        }


        /// <summary>
        /// If a gameObject we want to attack is not dead
        /// </summary>
        /// <param name="combatTarget">A game object to attack</param>
        /// <returns>True if a GO is alive, false otherwise</returns>
        public bool CanAttack(GameObject combatTarget)
        {
            return !combatTarget.GetComponent<Health>().IsDead();
        }

        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        //Animation Event
        void Hit()
        {
            if(target != null)
                target.TakeDamage(10);
        }
    }
}
