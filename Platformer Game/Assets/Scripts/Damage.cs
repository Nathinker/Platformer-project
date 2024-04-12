using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] private float damage = -1f;
    [SerializeField] private string damageTag = "Player";

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(damageTag))
        {
            Health health = collision.gameObject.GetComponent<Health>();
            if (health != null)
            {
                health.AddHealth(damage);
            }
        }
    }
}