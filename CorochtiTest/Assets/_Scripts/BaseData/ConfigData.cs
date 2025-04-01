using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class ConfigData
{
    public int maxHp;
    [FormerlySerializedAs("flySpeed")] public float takeOffSpeed;
    public float flySpeedIncreaseStep;
}
