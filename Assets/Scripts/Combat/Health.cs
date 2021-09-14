using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float healthPoints = 100f;

    bool isDead = false;

    public bool IsDead()
    {
        return isDead;
    }

    public void TakeDamage(float damage)
    {
        healthPoints = Mathf.Max(healthPoints - damage, 0);
        print("Health = " + healthPoints);
        if(healthPoints == 0 && !isDead)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        GetComponent<Animator>().SetTrigger("die");
    }
}
