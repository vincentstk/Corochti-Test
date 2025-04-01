using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHudSystem : MonoBehaviour
{
    #region Component Configs

    [SerializeField]
    private Image heartPrefab;

    private List<Image>  hearts = new List<Image>();
    #endregion

    public void CreateHeart(int heartCount)
    {
        for (int i = 0; i < heartCount; i++)
        {
            Image heartObj = Instantiate(heartPrefab);
            hearts.Add(heartObj);
            MainGameHUD.Instance.AddHeartToView(heartObj, i);
        }
    }

    public void DecreaseHeart(int heartCount)
    {
        hearts[heartCount].enabled = false;
    }
    public void IncreaseHeart(int heartCount)
    {
        hearts[heartCount - 1].enabled = true;
    }

    public void ResetHearts()
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            hearts[i].enabled = true;
        }
    }

    public void IncreaseToken(int tokenCount)
    {
        MainGameHUD.Instance.UpdateToken(tokenCount);
    }
}
