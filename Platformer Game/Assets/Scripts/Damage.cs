using UnityEngine;

#region Damage
public class Damage : MonoBehaviour
{
    #region Fields
    [SerializeField] private float damage = -1f;

    [SerializeField] private string damageTag = "Player";
    #endregion

    #region OnCollisionEnter2D
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
    #endregion
}
#endregion
