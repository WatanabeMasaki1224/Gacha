using System.Collections;
using UnityEngine;

public class GachaUIManager : MonoBehaviour
{
    enum GachaState
    {
        None,
        Confirm,
        Rolling,
        Result
    }

    GachaState state = GachaState.None;
    [SerializeField] GameObject _confirmPanel;
    [SerializeField] GameObject _resultPanel;
    MagicDataSO _newMagic;
    [SerializeField] PlayerController _player;
    [SerializeField] GachaManager _gachaManager;

    public void OpenConfirm()
    {
        state = GachaState.Confirm;
        _confirmPanel.SetActive(true);
    }
    public void OnCancelConfirm()
    {
        _confirmPanel.SetActive(false);
        state = GachaState.None;
    }

    public void OnClickDrraw()
    {
        if(state != GachaState.Confirm)  return;
        _confirmPanel.SetActive(false);
        state = GachaState.Rolling;
        StartCoroutine(GachaFlow());
    }

    IEnumerator GachaFlow()
    {
        yield return new WaitForSeconds(0.5f);
        _newMagic =  _gachaManager.GetRangdomMagic();
        Debug.Log($"獲得: {_newMagic.name} / {_newMagic.rarity}");
        _resultPanel.SetActive(true);
        state = GachaState.Result;
    }

    public void SelectNewMagic()
    {
        if( state != GachaState.Result) return;
        Debug.Log($"変更: {_player.GetCurrentMagicName()} → {_newMagic.name}");
        _player.SetMagic(_newMagic);
        CloseGacha();
    }

    public void OnKeep()
    {
        if (state != GachaState.Result) return;
        Debug.Log("キープ");
        CloseGacha();
    }

    void CloseGacha()
    {
        _resultPanel.SetActive(false);
        state = GachaState.None;
    }
}
