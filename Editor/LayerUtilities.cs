using UnityEditor;
using UnityEngine;

namespace HyperGnosys.EditorUtilities
{
    public static class LayerUtilities
    {
        public static void CreateAndAssignLayer(GameObject target, string layerName)
        {
            target.layer = GetLayerAndCreateItIfItDoesntExist(layerName);
        }
        public static int GetLayerAndCreateItIfItDoesntExist(string layerName)
        {
            int layerIndex = LayerMask.NameToLayer(layerName);
            if (layerIndex == -1)
            {
                Debug.LogWarning($"{layerName} Layer doesn't exist yet. Creating it...");
                CreateLayer(layerName);
                layerIndex = LayerMask.NameToLayer(layerName);
            }
            return layerIndex;
        }
        public static void CreateLayer(string newLayerName)
        {
            if (string.IsNullOrEmpty(newLayerName))
                throw new System.ArgumentNullException($"{newLayerName}", "New layer name string is either null or empty.");
            var tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
            var tagManagerLayerProperty = tagManager.FindProperty("layers");
            var layersPropertyCount = tagManagerLayerProperty.arraySize;
            SerializedProperty firstEmptyProperty = null;

            for (var i = 0; i < layersPropertyCount; i++)
            {
                var layerProperty = tagManagerLayerProperty.GetArrayElementAtIndex(i);
                var layerName = layerProperty.stringValue;
                if (layerName == newLayerName)
                {
                    Debug.LogError($"{newLayerName} layer already exists");
                    return;
                }
                //Los primeros 8 espacios son para default layers.
                //Si la layer ya tiene nombre entonces el espacio esta utilizado.
                if (i < 8 || layerName != string.Empty) continue;
                if (firstEmptyProperty == null)
                    firstEmptyProperty = layerProperty;
            }
            if (firstEmptyProperty == null)
            {
                Debug.LogError($"Maximum limit of {layersPropertyCount} layers exceeded. Layer \" {newLayerName}\" could not created.");
                return;
            }
            firstEmptyProperty.stringValue = newLayerName;
            tagManager.ApplyModifiedProperties();
        }
    }
}