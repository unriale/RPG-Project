using UnityEngine;
using RPG.Resources;
using UnityEngine.UI;

namespace RPG.Combat
{
    public class EnemyHealthDisplay: MonoBehaviour
    {
        Fighter fighter;

        private void Awake()
        {
            fighter = GameObject.FindGameObjectWithTag("Player").GetComponent<Fighter>();
        }

        private void Update()
        {
            Health health = fighter.GetTarget();
            if (!health)
            {
                GetComponent<Text>().text = "N/A";
                return;
            }
            else
                GetComponent<Text>().text = string.Format("{0:0}/{1:0}", health.GetHealthPoints(), health.GetMaxHealthPoints());
        }
    }
}