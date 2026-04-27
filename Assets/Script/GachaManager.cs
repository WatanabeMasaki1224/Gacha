using UnityEngine;

public class GachaManager : MonoBehaviour
{
    [System.Serializable]
    public class GachaEntry
    {
        public MagicDataSO data;
        public int rate;
    }

    [SerializeField] GachaEntry[] table;
    
    public MagicDataSO GetRangdomMagic()
    {
        int total = 0;
        foreach(var t in table) 
            total += t.rate;

        int rand = Random.Range(0, total);
        int current = 0;
        foreach (var t in table)
        {
            current += t.rate;
            if(rand <= current)
            {
                return t.data;
            }
        }
        return table[0].data;
    }
}

