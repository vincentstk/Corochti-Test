using UnityEngine;

public class ConfigSystem
{
    #region Defines

    private const string CONFIGS = "Configs/Configs";

    #endregion

    public ConfigData Config { get; }

    public ConfigSystem()
    {
        TextAsset jsonText = Resources.Load<TextAsset>(CONFIGS);

        if (jsonText == null)
        {
            Debug.LogError("Failed to load JSON resource: " + CONFIGS);
        }

        try
        {
            Config = JsonUtility.FromJson<ConfigData>(jsonText.text);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to parse JSON from resource " + CONFIGS + ": " + e.Message);
        }
    }
}
