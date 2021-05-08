using Godot;
using System;
using Godot.Collections;
using Array = Godot.Collections.Array;

// Reference Defination
public class StoryReference : StoryElement
{
    // Custom Story Element Data
    public Array StoryReferenceData { get; set; } = new Array();

    // Reference Data
    // public string StoryElementTag { get { return StoryElementData["Tag"]; } set { StoryElementData["Tag"] = value; } }  // Example: Awesome Person

    protected StoryReference() {} 
    public StoryReference(string path, string name = "", string description = "") : base(path, name, description) 
    { 
        
    }

    public override bool Save()
    {
        bool isSuccess = base.Save();
        ConfigFile configFile = new ConfigFile();
        configFile.SetValue("StoryReference", "", StoryReferenceData);
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
        Array connectionDictionary = configFile.GetValue("StoryReference", "Connections") as Array;  // Godot Problem: "as Dictionary<string, string>" return null
        StoryReferenceData = new Array(connectionDictionary);
        return isSuccess;
    }
}