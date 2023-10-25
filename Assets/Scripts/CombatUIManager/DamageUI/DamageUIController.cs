using System.Collections;
using UnityEngine;

public class DamageUIController : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private GameObject damageUI;
    [SerializeField] private float uILifeTime;
    private WaitForSeconds delay;
    private void Awake()
    {
        health.OnTakeDamage += OnDamage;
        delay = new WaitForSeconds(uILifeTime);
    }

    private void OnDamage()
    {
        damageUI.SetActive(true);
        StartCoroutine(DeactivateDamageUI());
    }

    private IEnumerator DeactivateDamageUI()
    {
        yield return delay;
        damageUI.SetActive(false);
    }
}
