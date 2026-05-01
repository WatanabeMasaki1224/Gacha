using UnityEngine;

public class MagicController : MonoBehaviour
{
    MagicDataSO _data;
    Rigidbody2D _rb;

    public void Init(MagicDataSO magic)
    {
        _data = magic;
        _rb = GetComponent<Rigidbody2D>();
        //ÉTÉCÉYîΩâf
        transform.localScale = Vector3.one * _data.size;
        //ë¨ìxîΩâf
        _rb.linearVelocity = transform.right * _data.speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Damage:" + _data.damage);
        }

        if(!_data.pierce)
        {
            Destroy(gameObject);
        }
    }
}
