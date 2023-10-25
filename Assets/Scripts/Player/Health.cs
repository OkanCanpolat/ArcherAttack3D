using System;
using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action OnTakeDamage;
    public event Action OnDie;
    [SerializeField] private float maxHelth;
    private float currentHealth;
    private Animator animator;
    private bool isDead;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHelth;
    }

    public void TakeDamage()
    {
        if (isDead) return;

        currentHealth--;
        OnTakeDamage?.Invoke();

        if(currentHealth <= 0)
        {
            isDead = true;
            OnDie?.Invoke();
            animator.SetTrigger("Die");
        }
    }
    public void RestoreFullHealth()
    {
        currentHealth = maxHelth;
    }
    public bool IsDead()
    {
        return isDead;
    }
}
