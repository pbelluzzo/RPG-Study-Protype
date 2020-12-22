using RPG.Saving;
using RPG.Stats;
using RPG.Core;
using UnityEngine;
using System;

namespace RPG.Resources
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float healthPoints = 100f;
        bool isDead = false;

        private void Start()
        {
            healthPoints = GetComponent<BaseStats>().GetHealth();
        }

        public bool IsDead()
        {
            return isDead;
        }

        public object CaptureState()
        {
            return healthPoints;
        }

        public void RestoreState(object state)
        {
            healthPoints = (float)state;

            if (healthPoints == 0) Die();
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            if (healthPoints == 0 && !isDead) 
            {
                GiveExperience(instigator);
                Die(); 
            }
        }

        private void GiveExperience(GameObject instigator)
        {
            Experience experience = GetComponent<Experience>();
            if (experience == null) return;
            instigator.GetComponent<Experience>().GainExperience(GetComponent<BaseStats>().GetExperienceReward());
        }

        public float GetHealthPercentage()
        {
            return 100 * (healthPoints / GetComponent<BaseStats>().GetHealth());
        }

        private void Die()
        {
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
            isDead = true;
        }
    }
}
