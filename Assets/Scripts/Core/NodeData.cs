using UnityEngine;

[CreateAssetMenu(menuName = "BumpU/NodeData", fileName = "NodeData")]
public class NodeData : ScriptableObject
{
    public int Row; // 0‑4
    public int Column; // 0‑4
    public int Number; // 1‑5 according to shifting pattern
}
