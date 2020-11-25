using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;
using UnityEngine.Experimental.TerrainAPI;
using UnityEngine.XR;
using RPG.Saving;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction, ISaveable
    {
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] Transform rightHandTransform = null;
        [SerializeField] Transform leftHandTransform = null;
        [SerializeField] Weapon defaultWeapon = null;

        Weapon currentWeapon;
        Health target;
        float timeSinceLastAttack = Mathf.Infinity;

        private void Start()
        {
            if (currentWeapon == null) EquipWeapon(defaultWeapon);
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null) return;
            if (!CanAttack(target.gameObject)) return;

            if (!GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(target.transform.position, 1f);
            }
            if (GetIsInRange())
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null || combatTarget.GetComponent<Health>().IsDead()) return false;
            return true;
        }

        public void Cancel()
        {
            StopAttack();
            target = null;
            GetComponent<Mover>().Cancel();
        }

        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
            // Animation will trigger the Hit() event.
        }
        public void EquipWeapon(Weapon weapon)
        {
            currentWeapon = weapon;
            Animator animator = GetComponent<Animator>();
            weapon.Spawn(rightHandTransform, leftHandTransform, animator);
        }
        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) <= currentWeapon.GetRange();
        }

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            if (timeSinceLastAttack < timeBetweenAttacks) return;
            timeSinceLastAttack = 0f;
            TriggerAtack();
        }

        private void TriggerAtack()
        {
            GetComponent<Animator>().ResetTrigger("stopAttack");
            GetComponent<Animator>().SetTrigger("attack");
        }


        private void StopAttack()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
        }


        // Animation Event
        void Hit()
        {
            if (target == null) return;
            if (currentWeapon.HasProjectile()) currentWeapon.LaunchProjectile(rightHandTransform, leftHandTransform, target);
            if (!currentWeapon.HasProjectile()) target.TakeDamage(currentWeapon.GetDamage());
        }

        void Shoot()
        {
            Hit();
        }

        public object CaptureState()
        {
            return currentWeapon.name;
        }

        public void RestoreState(object state)
        {
            string weaponName = (string)state;
            Weapon weapon = Resources.Load<Weapon>(weaponName);
            EquipWeapon(weapon);
        }
    }
}
