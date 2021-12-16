using UnityEngine;
using RPG.Stats;
using RPG.Core;
using GameDevTV.Utils;
using UnityEngine.Events;
using System;

namespace RPG.Attributes
{
    public class Health : MonoBehaviour, ISaveable
    {
        LazyValue<float> healthPoints;
        [SerializeField] float regenerationPercentage = 70;
        [SerializeField] TakeDamageEvent takeDamage;
        [SerializeField] UnityEvent onDie;

        [Serializable]
        public class TakeDamageEvent : UnityEvent<float>
        {
        }

        private void Awake()
        {
            healthPoints = new LazyValue<float>(GetInitialHealth);
        }

        private float GetInitialHealth()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        private void Start()
        {
            healthPoints.ForceInit();
        }

        private void OnEnable()
        {
            GetComponent<BaseStats>().OnLevelUp += RegenerateHealth;
        }

        private void OnDisable()
        {
            GetComponent<BaseStats>().OnLevelUp -= RegenerateHealth;
        }

        bool isDead = false;

        public object CaptureState()
        {
            return healthPoints.value;
        }

        public void RestoreState(object state)
        {
            //print("HEALTH RESTORE");
            healthPoints.value = (float)state;
            if (healthPoints.value <= 0)
            {
                print(gameObject.name + " is dead");
                Die();
            }
        }

        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            //print(gameObject.name + " took damage " + damage);
            healthPoints.value = Mathf.Max(healthPoints.value - damage, 0);
            if (healthPoints.value == 0 && !isDead)
            {
                onDie.Invoke();
                Die();
                AwardExperience(instigator);
            }
            else
            {
                takeDamage.Invoke(damage);
            }
        }

        public float GetPercentage()
        {
            return GetFraction() * 100;
        }

        public float GetFraction()
        {
            return (healthPoints.value / GetComponent<BaseStats>().GetStat(Stat.Health));
        }

        public float GetHealthPoints()
        {
            return healthPoints.value;
        }

        public void Heal(float healthToRestore)
        {
            healthPoints.value = Mathf.Min(healthPoints.value + healthToRestore, GetMaxHealthPoints());
        }

        public float GetMaxHealthPoints()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        private void Die()
        {
            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
            print("DIE TRIGGER IS SET");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void AwardExperience(GameObject instigator)
        {
            Experience experience = instigator.GetComponent<Experience>();
            if (!experience) return;
            experience.GainExperience(GetComponent<BaseStats>().GetStat(Stat.ExperienceReward));
        }

        private void RegenerateHealth()
        {
            float regenHealthPoints = GetComponent<BaseStats>().GetStat(Stat.Health) * (regenerationPercentage / 100);
            healthPoints.value = Mathf.Max(regenHealthPoints, healthPoints.value);
        }
    }
}