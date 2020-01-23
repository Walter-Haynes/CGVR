#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

public static class ExportPackage
{
	[MenuItem("/SimpleVR/Tools/Export with tags and layers, Input settings")]
	public static void Export()
	{
		string[] projectContent = new string[] { "Assets/SimpleVR", "Assets/Resources/DataVR.asset", "ProjectSettings/TagManager.asset", "ProjectSettings/InputManager.asset", "ProjectSettings/ProjectSettings.asset" };
		string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "\\SimpleVR.unitypackage";
		AssetDatabase.ExportPackage(projectContent, path, ExportPackageOptions.Interactive | ExportPackageOptions.Recurse);
		Debug.Log("Project Exported");
	}
}
#endif