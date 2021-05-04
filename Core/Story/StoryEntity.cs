using Godot;
using System;
using Godot.Collections;
using Array = Godot.Collections.Array;

// Entity Definations
public class StoryEntity : StoryElement
{
    // Custom Story Element Data
    public Dictionary<string, string> StoryEntityData { get; set; } = new Dictionary<string, string>();

    // public string Template { get { return StoryElementData["Template"]; } set { StoryElementData["Template"] = value; } }  // Template reference
    // public Dictionary<string, object> TemplateData { get; set; } = new Dictionary<string, object>();

    protected StoryEntity() {} 
    public StoryEntity(string path, string name = "", string description = "") : base(path, name, description) 
    { 
        
    }

    public override bool Save()
    {
        bool isSuccess = base.Save();
        ConfigFile configFile = new ConfigFile();
        configFile.SetValue("StoryElement", "Data", StoryElementData);
        configFile.SetValue("StoryEntity", "Data", StoryEntityData);
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
        Dictionary entityDictionary = configFile.GetValue("StoryEntity", "Data") as Dictionary;  // Godot Problem: "as Dictionary<string, string>" return null
        StoryEntityData = new Dictionary<string, string>(entityDictionary);
        return isSuccess;
    }



    
}