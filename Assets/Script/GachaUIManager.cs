using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

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
    [SerializeField] Image _resultIcon;
    [SerializeField] TextMeshProUGUI _resultText;
    MagicDataSO _newMagic;
    [SerializeField] PlayerController _player;
    [SerializeField] GachaManager _gachaManager;
    [SerializeField] Transform _gacha;
    [SerializeField] Transform _flash;

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
        _gacha.rotation = Quaternion.Euler(0, 0, -10f);

        yield return _gacha
            .DORotate(new Vector3(0, 0, 15f), 0.15f)
            .SetLoops(6, LoopType.Yoyo)
            .SetEase(Ease.InOutSine)
            .WaitForCompletion();

        _gacha.rotation = Quaternion.identity;
        //光をガチャの中心から広げる
        _flash.localScale = Vector3.zero;
        _flash.gameObject.SetActive(true);

        yield return _flash
            .DOScale(200f, 2f)
            .SetEase(Ease.OutQuad)
            .WaitForCompletion();

        _flash.gameObject.SetActive(false);
        //ガチャの結果を所得
        _newMagic =  _gachaManager.GetRangdomMagic();
        _resultIcon.sprite = _newMagic.icon;
        _resultText.text = $"獲得！{_newMagic.magictype}：レア度{_newMagic.rarity}";
        Debug.Log($"獲得: {_newMagic.name} / {_newMagic.rarity}");
        //UI表示
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
