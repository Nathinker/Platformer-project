using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] private float damage = -1f;
    [SerializeField] private string damageTag = "Player";

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(damageTag))
        {
            if (collision.gameObject.TryGetComponent<Health>(out var hl))
            {
                hl.AddHealth(damage);
            }
        }
    }
}