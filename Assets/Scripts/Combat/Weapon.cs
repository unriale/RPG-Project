using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] GameObject equippedWeapon = null;
        [SerializeField] AnimatorOverrideController animatorOverride = null;
        [SerializeField] private float range = 1f;
        [SerializeField] private float damage = 10f;

        public float GetWeaponRange() => range;

        public float GetDamage() => damage;
        public void Spawn(Transform handTransform, Animator animator)
        {
            if(equippedWeapon != null)
            {
                Instantiate(equippedWeapon, handTransform);
            }
            if(animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;
            }
        }
    }
}