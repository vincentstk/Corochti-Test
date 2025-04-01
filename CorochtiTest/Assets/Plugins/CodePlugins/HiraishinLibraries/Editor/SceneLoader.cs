using UnityEditor;
namespace Hiraishin.EditorUtilities
{
	public partial class SceneManagerHelper
	{
		#if UNITY_EDITOR
		[MenuItem("Tools/Scene Manager Helper/Open/DevelopScene")]
		public static void LoadDevelopScene()
		{
			OpenScene("Assets/Scenes/DevelopScene.unity");
		}
		[MenuItem("Tools/Scene Manager Helper/Open/GroundPlayMap")]
		public static void LoadGroundPlayMap()
		{
			OpenScene("Assets/Scenes/GroundPlayMap.unity");
		}
		[MenuItem("Tools/Scene Manager Helper/Open/MainScene")]
		public static void LoadMainScene()
		{
			OpenScene("Assets/Scenes/MainScene.unity");
		}
		#endif
	}
}