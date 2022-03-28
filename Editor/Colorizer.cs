using UnityEngine;
using UnityEditor;
public class Colorizer : EditorWindow
{
    [MenuItem("Window/HyperGnosys/Colorizer")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<Colorizer>("Colorizer");
    }

    Color color;

    private void OnGUI()
    {
        GUILayout.Label("Cambia el color del material de los GameObjects seleccionados", EditorStyles.boldLabel);
        color = EditorGUILayout.ColorField("Nuevo Color", color);
        if (GUILayout.Button("Colorize"))
        {
            Colorize(color);
        }
    }
    private void Colorize(Color color)
    {
        foreach(GameObject obj in Selection.gameObjects)
        {
            Renderer renderer = obj.GetComponent<Renderer>();
            if(renderer != null)
            {
                Undo.RecordObject(renderer.sharedMaterial, "Change Color");
                renderer.sharedMaterial.color = color;
            }
        }
    }
}