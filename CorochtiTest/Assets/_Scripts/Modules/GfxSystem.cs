using UnityEngine;

public class GfxSystem : MonoBehaviour
{
    #region Component Configs

    [SerializeField] private SpriteRenderer gfx;
    [SerializeField] private Color onGroundColor;
    [SerializeField] private Color onAirColor;

    #endregion

    public void Init()
    {
        gfx.color = onGroundColor;
    }

    public void ChangeColor(bool isOnAir)
    {
        gfx.color = isOnAir ? onAirColor : onGroundColor;
    }

    public void FlipSprite(bool isLeft)
    {
        gfx.flipX = isLeft ? true : false;
    }
}
