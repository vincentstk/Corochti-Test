using System;
using DG.Tweening;
using Hiraishin.ObserverPattern;
using Hiraishin.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainGameHUD : BaseSingleton<MainGameHUD>
{
    #region Component Configs

    [SerializeField] private Transform heartParent;
    [SerializeField] private float spacingOffset;
    [SerializeField] private GameObject screenHit;

    private Action<object> OnPlayerHit;
    #endregion

    #region UI Elements

    public Image imgToken;
    public TextMeshProUGUI txtToken;

    #endregion

    protected override void OnAwake()
    {
        OnPlayerHit = (param) => ScreenHit();
        this.RegisterListener(EventID.GetHit, OnPlayerHit);
    }

    public void AddHeartToView(Image heart, int index)
    {
        heart.transform.SetParent(heartParent);
        heart.rectTransform.anchoredPosition = Vector2.zero;
        Vector2 pos = heart.rectTransform.anchoredPosition;
        pos.x += index * spacingOffset;
        heart.rectTransform.anchoredPosition = pos;
    }

    public void UpdateToken(int token)
    {
        imgToken.transform.DOPunchScale(Vector3.one * 0.2f, 0.2f).OnComplete(() =>
        {
            txtToken.SetText(token.ToString());
            imgToken.transform.localScale = Vector3.one;
        });
        
    }
    public void ScreenHit()
    {
        screenHit.gameObject.SetActive(false);
        screenHit.gameObject.SetActive(true);
    }
}
