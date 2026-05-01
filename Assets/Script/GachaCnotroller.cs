using UnityEngine;

public class GachaCnotroller : MonoBehaviour
{
    [SerializeField] GachaUIManager _gachaUI;
    bool _canInteract = false;

    void Update()
    {
        if (_canInteract && Input.GetKeyDown(KeyCode.E))
        {
            _gachaUI.OpenConfirm();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _canInteract = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _canInteract = false;
        }
    }
}
