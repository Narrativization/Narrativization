using Godot;
using System;
using Godot.Collections;

public class StoryElementTemplate : Node
{
    
    public override void _Ready()
    {
        
    }

    public Dictionary<string, object> GetSaveData()
    {
        Dictionary<string, object> saveData = new Dictionary<string, object>(); 
        var saveNodes = GetTree().GetNodesInGroup("Persist");
        foreach (Node saveNode in saveNodes)
        {
            saveData.Add(saveNode.Name, saveNode.Get("text"));
        }
        return saveData;
    }

}
