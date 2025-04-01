using UnityEngine;

public class CollectSystem : MonoBehaviour
{
    #region Component Configs

    private int currentToken;
    private PlayerHudSystem _hudSystem;

    #endregion

    public void Init(PlayerHudSystem hudSystem)
    {
        _hudSystem = hudSystem;
    }

    public void IncreaseToken()
    {
        currentToken++;
        _hudSystem.IncreaseToken(currentToken);
    }
}
