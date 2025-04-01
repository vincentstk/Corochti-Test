using UnityEngine;

#region Enums
public enum SystemState : byte
{
    Disable,
    Enable
}
#endregion

#region Classes

public static class ConstantDefinitions
{

    public const string SFX_VOLUME = "sfx_volume";
    public const string MUSIC_VOLUME = "bgm_volume";
    public const string HURT_SFX = "hurt";
    public const string RESPAWN_SFX = "respawn";
    public const string JUMP_SFX = "jump";
    
}

#endregion
#region Observer Pattern Data Classes

public class VolumeMessage
{
    public float volume { get; set; }
}

#endregion