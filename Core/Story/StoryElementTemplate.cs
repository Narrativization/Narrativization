using System;
using Godot;
using Godot.Collections;

public class StoryElementTemplate : Control
{
    [Export] // Prof of Concept - Template Creation
    private Dictionary<Godot.Object, Godot.Object> assets; // Create Enum Asset Types (include variables, Texture etc.)

    public override void _Ready()
    {
        
    }

    public Dictionary<string, object> GetSaveData()
    {
        Dictionary<string, object> saveData = new Dictionary<string, object>(); 
        Godot.Collections.Array saveNodes = GetTree().GetNodesInGroup("Persist");

        for (int i = 0; i < saveNodes.Count; i++)
        {
            if (saveNodes[i] is LineEdit || saveNodes[i] is TextEdit)
            {
                Node saveNode = saveNodes[i] as Node;
                saveData.Add(saveNode.Name, saveNode.Get("text"));
            }
        }
        return saveData;
    }

}
