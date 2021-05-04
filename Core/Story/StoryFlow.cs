using Godot;
using System;
using Godot.Collections;
using Array = Godot.Collections.Array;

// Flow Definations
public class StoryFlow : StoryElement
{
    // Custom Story Element Data
    public Array StoryFlowFragments { get; set; } = new Array();
    public Array StoryFlowNodes { get; set; } = new Array();
    public Array StoryFlowConnections { get; set; } = new Array();

    protected StoryFlow() {} 
    public StoryFlow(string path, string name = "", string description = "") : base(path, name, description) 
    { 
        
    }

    public override bool Save()
    {
        bool isSuccess = base.Save();
        ConfigFile configFile = new ConfigFile();
        configFile.SetValue("StoryElement", "Data", StoryElementData);
        configFile.SetValue("StoryFlow", "Fragments", StoryFlowFragments);
        configFile.SetValue("StoryFlow", "Nodes", StoryFlowNodes);
        configFile.SetValue("StoryFlow", "Connections", StoryFlowConnections);
        Error err = configFile.Save(ResourcePath);
        if (err != Error.Ok) { GD.Print(err); return false; }
        return isSuccess;
    }

    protected override bool Load()
    {
        bool isSuccess = false;
        ConfigFile configFile = new ConfigFile();
        Error err = configFile.Load(StoryElementPath);
        if (err != Error.Ok) { GD.Print(err); return false; }
        Dictionary elementDictionary = configFile.GetValue("StoryElement", "Data") as Dictionary;  // Godot Problem: "as Dictionary<string, string>" return null
        StoryElementData = new Dictionary<string, string>(elementDictionary);
        Array fragmentDictionary = configFile.GetValue("StoryFlow", "Fragments") as Array;  // Godot Problem: "as Dictionary<string, string>" return null
        StoryFlowFragments = new Array(fragmentDictionary);
        Array nodeDictionary = configFile.GetValue("StoryFlow", "Nodes") as Array;  // Godot Problem: "as Dictionary<string, string>" return null
        StoryFlowNodes = new Array(nodeDictionary);
        Array connectionDictionary = configFile.GetValue("StoryFlow", "Connections") as Array;  // Godot Problem: "as Dictionary<string, string>" return null
        StoryFlowConnections = new Array(connectionDictionary);
        return isSuccess;
    }



    
}