using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;
public class TileMapInstantiator : EditorWindow
{
    [MenuItem("Window/HyperGnosys/Tile Map Instantiator")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<TileMapInstantiator>("Tile Map Instantiator");
    }
    Editor editor;
    [SerializeField] private GameObject mapParent;
    [SerializeField] private string seed = "";
    [SerializeField] List<GameObject> prefabList = new List<GameObject>();
    [SerializeField] float prefabXScale = 20;
    [SerializeField] float prefabZScale = 20;
    [SerializeField] float mapRowAmount = 20;
    [SerializeField] float mapColumnAmount = 20;

    void OnGUI()
    {
        if (GUILayout.Button("Clear MapParent Children"))
        {
            ClearMap();
        }
        if (!editor) { editor = Editor.CreateEditor(this); }
        if (editor) { editor.OnInspectorGUI(); }
        GUILayout.Label("Instancia prefabs al azar de la lista en el MapParent", EditorStyles.boldLabel);
        GUILayout.Label("Se considera que los prefabs tienen el origen al centro", EditorStyles.boldLabel);
        if (GUILayout.Button("Create Map"))
        {
            CreateMap();
        }

    }

    void OnInspectorUpdate() { Repaint(); }

    private enum Directions
    {
        East = 0,
        North = 90,
        West = 180,
        South = 270
    }
    private void ClearMap()
    {
        foreach (Transform child in mapParent.transform)
        {
            DestroyImmediate(child.gameObject);
        }
    }
    private void CreateMap()
    {
        if (!String.IsNullOrEmpty(seed))
        {
            Random.InitState(seed.GetHashCode());
        }
        Array directions = Enum.GetValues(typeof(Directions));
        float originX = mapParent.transform.position.x - (mapColumnAmount / 2) * prefabXScale;
        float originZ = mapParent.transform.position.z - (mapRowAmount / 2) * prefabZScale;
        for (int x = 0; x < mapColumnAmount; x++)
        {
            ///   Origen centrando mapParent  +  Centro del Prefab  +  Distancia de Prefabs Instanciados
            float instanceX = originX + (prefabXScale / 2) + (x * prefabXScale);
            for (int z = 0; z < mapRowAmount; z++)
            {
                ///   Origen centrando mapParent  +  Centro del Prefab  +  Distancia de Prefabs Instanciados
                float instanceZ = originZ + (prefabZScale / 2) + (z * prefabZScale);
                Vector3 instancePosition = new Vector3(instanceX, 0, instanceZ);
                int prefabIndex = Random.Range(0, prefabList.Count - 1);
                int yRot = (Int32)directions.GetValue(Random.Range(0, directions.Length));
                Quaternion rotation = Quaternion.Euler(0, yRot, 0);
                Instantiate(prefabList[prefabIndex], instancePosition, rotation, mapParent.transform);
            }
        }
    }
    public GameObject MapParent { get => mapParent; set => mapParent = value; }
}