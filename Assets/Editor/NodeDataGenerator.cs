using UnityEngine;
using UnityEditor;
using System.IO;

public class NodeDataGenerator : EditorWindow
{
    [MenuItem("BumpU/Generate Node Data")]
    public static void ShowWindow()
    {
        GetWindow<NodeDataGenerator>("Node Data Gen");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Generate 5x5 Grid Data"))
        {
            GenerateGridData();
        }
    }

    private static void GenerateGridData()
    {
        string path = "Assets/Resources/NodeData";
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        // 5x5 Grid Pattern
        // Row 1: 1 2 3 4 5
        // Row 2: 2 3 4 5 1
        // Row 3: 3 4 5 1 2
        // Row 4: 4 5 1 2 3
        // Row 5: 5 1 2 3 4

        int gridSize = 5;
        for (int row = 0; row < gridSize; row++)
        {
            for (int col = 0; col < gridSize; col++)
            {
                // Calculate number based on shifting pattern
                // Base sequence 1,2,3,4,5. Shift by row index.
                // (col + row) % 5 + 1
                int number = (col + row) % 5 + 1;

                NodeData data = ScriptableObject.CreateInstance<NodeData>();
                data.Row = row;
                data.Column = col;
                data.Number = number;

                string fileName = $"Node_{row}_{col}.asset";
                string assetPath = Path.Combine(path, fileName);
                
                AssetDatabase.CreateAsset(data, assetPath);
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("Generated 25 NodeData assets in Assets/Resources/NodeData");
    }
}
