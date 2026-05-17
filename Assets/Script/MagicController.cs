using UnityEngine;

public class MagicController : MonoBehaviour
{
    MagicDataSO _data;
    Rigidbody2D _rb;

    public void Init(MagicDataSO magic)
    {
        _data = magic;
        _rb = GetComponent<Rigidbody2D>();
        //サイズ反映
        transform.localScale = Vector3.one * _data.size;
        //速度反映
        _rb.linearVelocity = transform.right * _data.speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            float damage = _data.damage;
            bool isCritical = Random.Range(0,100) < _data.criticalRate;
            if(isCritical)
            {
                damage *= _data.criticalDamage;
                Debug.Log($"クリティカルダメージ：{damage}");
            }
            else
            {
                Debug.Log($"クリティカルなし:{damage}");
            }

            EnemyController enemy = other.GetComponent<EnemyController>();
            
            if (enemy != null)
            {
                enemy.TakeDamage(damage, isCritical);
                Debug.Log("a");

            }
        }

        if(!other.CompareTag("Player") && !_data.pierce)
        {
            Destroy(gameObject);
        }
    }
}
