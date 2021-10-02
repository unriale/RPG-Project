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
        [SerializeField] bool isRightHanded = true;

        public float GetWeaponRange() => range;

        public float GetDamage() => damage;
        public void Spawn(Transform rightHand, Transform leftHand, Animator animator)
        {
            if(equippedWeapon != null)
            {
                Transform handTransform = isRightHanded ? rightHand : leftHand;
                Instantiate(equippedWeapon, handTransform);
            }
            if(animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;
            }
        }
    }
}