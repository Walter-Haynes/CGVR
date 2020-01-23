#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

public class ExecutionOrderManager : EditorWindow
{
	public Object monoBehaviour;
	public int desiredOrder;

	[MenuItem("Tools/ExecutionOrderManager")]
	public static void ShowWindow()
	{
		GetWindow(typeof(ExecutionOrderManager));
	}

	public void OnGUI()
	{
		monoBehaviour = EditorGUILayout.ObjectField("MonoBehaviour: ", monoBehaviour, typeof(Object), false);
		desiredOrder = EditorGUILayout.IntSlider("Desired Order: ", desiredOrder, -32000, 32000);

		if (GUI.Button(new Rect(20.0f, 50.0f, 150.0f, 20.0f), "Set Execution Order"))
		{
			if (monoBehaviour == null) return;

			SetScriptOrder(monoBehaviour.name, desiredOrder);
		}
	}

	public static void SetScriptOrder(string scriptName, int desiredOrder)
	{
		if (scriptName == null || scriptName == "") return;

		foreach (MonoScript monoScript in MonoImporter.GetAllRuntimeMonoScripts())
		{
			if (monoScript.name == scriptName)
			{
				if (MonoImporter.GetExecutionOrder(monoScript) != desiredOrder) MonoImporter.SetExecutionOrder(monoScript, desiredOrder);
				break;
			}
		}
	}
}
#endif
