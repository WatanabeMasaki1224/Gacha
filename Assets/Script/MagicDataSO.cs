using UnityEngine;

[CreateAssetMenu(fileName = "MagicDataSO", menuName = "Scriptable Objects/MagicDataSO")]
public class MagicDataSO : ScriptableObject
{
    public Rarity rarity;
    public MagicType magictype;
    public float damage;
    public float speed;
    public float size;
    public bool pierce;
}
