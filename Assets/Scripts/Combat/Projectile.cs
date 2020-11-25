using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using RPG.Core;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed = 1f;
        [SerializeField] bool isHoming = false;
        [SerializeField] GameObject hitEffect = null;

        Health target = null;
        float damage = 0;
        void Start()
        {
            transform.LookAt(GetAimLocation());
            Destroy(gameObject, 6f);
        }

        void Update()
        {
            if (target == null) return;

            if (isHoming) transform.LookAt(GetAimLocation());
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        public void SetTarget(Health target, float damage)
        {
            this.target = target;
            this.damage = damage;
        }

        private Vector3 GetAimLocation()
        {
            CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
            if (targetCapsule == null) return target.transform.position;
            return target.transform.position + Vector3.up * targetCapsule.height / 2;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<Health>() != target) return;
            if (other.gameObject.GetComponent<Health>().IsDead())
            {
                isHoming = false;
                return;
            }
            if (hitEffect != null)
            {
                var effect = Instantiate(hitEffect, GetAimLocation(), transform.rotation);
                Destroy(effect, 5f);
            }
            target.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
