using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class GachaManager : MonoBehaviour
{
    [System.Serializable]
    public class GachaEntry
    {
        public MagicDataSO data;
    }

    [SerializeField] GachaEntry[] _table;
    [SerializeField] int _SSR = 5;
    [SerializeField] int _SR = 10;
    [SerializeField] int _R = 35;
    [SerializeField] int _gachaCeiling = 5;
    [SerializeField] int _currentCeiling = 0;

    public MagicDataSO GetRangdomMagic()
    {
        int rand = Random.Range(0,100);
        Rarity targetRarity;
        _currentCeiling++;
        //レア度の抽選
        if(_currentCeiling >= _gachaCeiling)
        {
            targetRarity = Rarity.SSR;
            _currentCeiling = 0;
        }
        else if(rand < _SSR )
        {
            targetRarity = Rarity.SSR;
        }
        else if(rand < _SSR + _SR)
        {
            targetRarity = Rarity.SR;
        }
        else if(rand < _SSR + _SR + _R)
        {
            targetRarity = Rarity.R;
        }
        else
        {
            targetRarity = Rarity.N;
        }

        //該当レア度の魔法を数える
        int count = 0;
        foreach (var t in _table)
        {
            if (t.data.rarity == targetRarity)
            {
                count++;
            }
        }
        //該当レア度が存在しないときの保険
        if(count == 0)
        {
            return _table[0].data;
        }

        // レア度内ランダム
        int pick = Random.Range(0, count);
        int current = 0;

        foreach (var t in _table)
        {
            if (t.data.rarity == targetRarity)
            {
                if (current == pick)
                {
                    return t.data;
                }

                current++;
            }
        }

        return _table[0].data;
    }
}

