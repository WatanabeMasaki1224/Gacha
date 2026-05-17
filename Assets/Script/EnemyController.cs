using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float _hp = 10f;
   
    public void TakeDamage(float damage, bool isCritical)
    {
        _hp -= damage;

        if(isCritical )
        {
            Debug.Log("criticalHIt");
            //会心時のエフェクトなど   
        }
        Debug.Log($"EnemyHP+{_hp}");

        if( _hp <= 0 )
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
