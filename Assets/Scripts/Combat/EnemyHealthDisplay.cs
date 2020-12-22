using RPG.Resources;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Combat
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        Health health;

        private void Update()
        {

            health = GameObject.FindWithTag("Player").GetComponent<Fighter>().GetTarget();
            if (health == null)
            {
                GetComponent<Text>().text = "";
                return;
            }
            GetComponent<Text>().text = string.Format("{0:0}%", health.GetHealthPercentage());
        }
    }
}
