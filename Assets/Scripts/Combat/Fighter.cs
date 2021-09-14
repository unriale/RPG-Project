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
        float timeSinceLastAttack = 0;

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
                GetComponent<Mover>().MoveTo(target.transform.position);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            if (timeSinceLastAttack >= timeBetweenAttacks)
            {
                //This will trigger the Hit() event
                animator.SetTrigger("attack");
                timeSinceLastAttack = 0;
            }     
        }

        /// <summary>
        /// Check if a player is too close to the target.
        /// </summary>
        /// <returns>True if a player is too close to the target, false otherwise.</returns>
        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) <= weaponRange;
        }

        public void Cancel()
        {
            target = null;
            animator.SetTrigger("stopAttack");
        }

        public void Attack(CombatTarget combatTarget)
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
