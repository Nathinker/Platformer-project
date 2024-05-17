using UnityEngine;

#region Damage

public class Damage : MonoBehaviour
{
    #region Fields
    [SerializeField] private float damageAmount = 1f;
    [SerializeField] private string damageTag = "Player";
    #endregion

    #region OnCollisionEnter2D
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(damageTag))
        {
            ApplyDamage(collision.gameObject);
        }
    }
    #endregion

    #region ApplyDamage
    private void ApplyDamage(GameObject target)
    {
        var hl = target.GetComponent<Health>();
        if (hl != null)
        {
            hl.AddHealth(-damageAmount);
        }
    }
    #endregion
}
#endregion
