using UnityEngine;

public class MagicController : MonoBehaviour
{
    MagicDataSO data;
    Rigidbody2D rb;

    public void Init(MagicDataSO magic)
    {
        data = magic;
        rb = GetComponent<Rigidbody2D>();
        //ÉTÉCÉYîΩâf
        transform.localScale = Vector3.one * data.size;
        //ë¨ìxîΩâf
        rb.linearVelocity = transform.right * data.speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Damage:" + data.damage);
        }

        if(!data.pierce)
        {
            Destroy(gameObject);
        }
    }
}
