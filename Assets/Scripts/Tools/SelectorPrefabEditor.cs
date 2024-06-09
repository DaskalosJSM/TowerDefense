using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(selectorPrefab))] // Asegúrate de que 'selectorPrefab' sea el nombre correcto de tu script
public class SelectorPrefabEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        selectorPrefab script = (selectorPrefab)target;

        // Agrega un botón al Inspector de Unity
        if (GUILayout.Button("ActualizarVisualizacion"))
        {
            script.ActualizarVisualizacion();
        }
    }
}