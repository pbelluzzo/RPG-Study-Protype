using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using System;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float weaponDamage = 5f;
        [SerializeField] GameObject equippedPrefab = null;
        [SerializeField] AnimatorOverrideController animatorOverride;
        [SerializeField] bool isRightHanded = true;
        [SerializeField] Projectile projectile = null;

        const string weaponName = "Weapon";

        public void Spawn(Transform rHandTransform, Transform lHandTransform, Animator animator)
        {
            DestroyOldWeapon(rHandTransform, lHandTransform);

            Transform handTransform = GetHandTransform(rHandTransform, lHandTransform);
            if (equippedPrefab != null)
            {
                GameObject weapon = Instantiate(equippedPrefab, handTransform);
                weapon.name = weaponName;
            }

            var  overrideController = animator.runtimeAnimatorController as AnimatorOverrideController;
            if (animatorOverride != null) animator.runtimeAnimatorController = animatorOverride;
            else if(overrideController != null)
            {
                animator.runtimeAnimatorController = overrideController.runtimeAnimatorController;
            }

            
        }

        // TODO :: refatorar
        private void DestroyOldWeapon(Transform rHandTransform, Transform lHandTransform)
        {
            Transform oldWeapon = rHandTransform.Find(weaponName);
            if (oldWeapon == null)
            {
                oldWeapon = lHandTransform.Find(weaponName);
            }
            if (oldWeapon == null) return;

            oldWeapon.name = "Destroying";
            Destroy(oldWeapon.gameObject);
        }

        public bool HasProjectile()
        {
            return projectile != null;
        }

        public void LaunchProjectile(Transform rHandTransform, Transform lHandTransform, Health target)
        {
            Projectile projectileInstance = Instantiate(projectile, GetHandTransform(rHandTransform, lHandTransform).position, Quaternion.identity);
            projectileInstance.SetTarget(target, weaponDamage);
        }

        public float GetRange()
        {
            return weaponRange;
        }

        public float GetDamage()
        {
            return weaponDamage;
        }
        private Transform GetHandTransform(Transform rHandTransform, Transform lHandTransform)
        {
            return isRightHanded ? rHandTransform : lHandTransform;
        }
    }

}