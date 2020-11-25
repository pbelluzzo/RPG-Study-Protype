using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour
    {
        [SerializeField] Weapon weapon;
        [SerializeField] float respawnTime;
        public void OnTriggerEnter(Collider other)
        {
            print("trigger");
            if (other.gameObject.CompareTag("Player"))
            {
                other.GetComponent<Fighter>().EquipWeapon(weapon);
                StartCoroutine(HideForSeconds(respawnTime));
            }
        }

        private IEnumerator HideForSeconds(float seconds)
        {
            ShowPickup(false);
            yield return new WaitForSeconds(seconds);
            ShowPickup(true);
        }

        private void ShowPickup(bool show)
        {
                var collider = GetComponent<Collider>();
                collider.enabled = show;
                foreach (Transform child in transform)
                {
                    child.gameObject.SetActive(show);
                }
        }

    }
}
