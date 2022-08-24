using Godot;
using System;
using Godot.Collections;
using Array = Godot.Collections.Array;

// Story Elements / Narrative Elements
public class StoryElement : Resource
{
    // Story Element Datas
    public int Id { get; set; } = -1; // '-1' Unassigned
    public string Name  { get; set; } = "New Story Element";
    public string Description { get; set; } = "";
    public Array Tags { get; set; } = new Array();

    // Story Element Type Datas
    public Dictionary<string, object> Fragments { get; set; } = new Dictionary<string, object>();
    public Dictionary<string, object> Nodes { get; set; } = new Dictionary<string, object>();
    public Array Connections { get; set; } = new Array();

    // ~Use StoryElement Prefix?

    // Remove Unnessary Datas 

    // Story Element Data
    public Dictionary<string, string> StoryElementData { get; set; } = new Dictionary<string, string>();
    public Dictionary<string, object> StoryElementTypeData { get; set; } = new Dictionary<string, object>();

    // Story Element Information 
    public string StoryElementName { get { return StoryElementData["Name"]; } set { StoryElementData["Name"] = ResourceName = value; } } // Example: MyChar | Uses ResourceName
    public string StoryElementDescription { get { return StoryElementData["Description"]; } set { StoryElementData["Description"] = value; } }  // Example: Character Example
    public StoryElementType StoryElementType { get { return (StoryElementType)Enum.Parse(typeof(StoryElementType), StoryElementData["StoryElementType"]);  } set { StoryElementData["StoryElementType"] = value.ToString(); } }  // Example: Character Example
    
    // Story Element Type Information
    // Fragments, Nodes, Connections etc.

    // Story Element File
    public string StoryElementPath { get { return ResourcePath; } set { TakeOverPath(value); } } // StoryElementPath = StoryBase[.../<StoryName>] + StoryElementId[/Flow/Chapter1] + "." + StoryElementExtension[storyel]
    public string StoryElementId { get { return StoryElementPath.Replace(StoryManager.CurrentStory.StoryBase, "").Replace("." + StoryElementPath.Extension(), ""); } } // Example: /Entity/Character/MyChar | Relative path to project(<ProjectName>.storier) | StoryElementId(Seid)
    public string StoryElementExtension { get { return StoryElementPath.Extension(); } } // Example ext: "ext" 

    protected StoryElement() {}
    public StoryElement(string name, string desc) 
    { 
        Godot.Object asd = new Godot.Object();
        Name = name; Description = desc;
        Fragments.Add("Fragment-1", new StoryFragment().StoryFragmentData);
        Fragments.Add("Fragment-2", new StoryFragment().StoryFragmentData);
        Nodes.Add("Node-1", new StoryNode().NodeData);
        Nodes.Add("Node-2", new StoryNode().NodeData);
        Tags.Add("Hero");
        Error err = Error.Ok;
        ConfigFile configFile = new ConfigFile();
        configFile.SetValue("StoryElement", "Id", Id);
        configFile.SetValue("StoryElement", "Name", Name);
        configFile.SetValue("StoryElement", "Description", Description);
        configFile.SetValue("StoryElement", "Tags", Tags);
        configFile.SetValue("StoryElementType", "Fragments", Fragments);
        configFile.SetValue("StoryElementType", "Nodes", Nodes);
        configFile.SetValue("StoryElementType", "Connections", Connections);
        err = configFile.Save("C:/Users/Atakan/Documents/Storier/Test.storyel");
        if (err != Error.Ok) { GD.Print("Story Element Save Failed:" + err); }
    }

    // Load Story Element
    public StoryElement(string path)
    {
        if (!path.StartsWith(StoryManager.CurrentStory.StoryBase)) { GD.PushWarning("Story Element isn't in Workspace!\n" + path); return; }
        StoryElementPath = path;
        StoryElementName = ""; 
        StoryElementDescription = ""; 
        StoryElementType = StoryElementType.StoryAsset; 
        
        if ((new Directory()).FileExists(StoryElementPath))
        {
            Load();
            StoryManager.CurrentStoryManager.EmitSignal("OnStoryElementCreated", this);
        }
    } 

    // Create Story Element
    public StoryElement(string path, string name = "", string description = "", StoryElementType type = StoryElementType.StoryAsset) // path: fullPath
    {
        if (!path.StartsWith(StoryManager.CurrentStory.StoryBase)) { GD.PushWarning("Story Element isn't in Workspace!\n" + path); return; }
        StoryElementName = name; 
        StoryElementDescription = description; 
        StoryElementType = type; 
        StoryElementPath = path;

        Save();
        StoryManager.CurrentStoryManager.EmitSignal("OnStoryElementCreated", this);
    }

    // Saving Story Element to Path | ~Maybe create Resource Format Saver/Loader
    public Error Save(string path) 
    { 
        Error err = Error.Ok;
        ConfigFile configFile = new ConfigFile();
        configFile.SetValue("StoryElement", "Name", StoryElementName);
        configFile.SetValue("StoryElement", "Description", StoryElementDescription);
        configFile.SetValue("StoryElement", "Type", StoryElementType);
        configFile.SetValue("StoryElementType", "Data", StoryElementTypeData);
        err = configFile.Save(path);
        if (err != Error.Ok) { GD.Print("Story Element Save Failed:" + err); }
        return err; 
    }

    // Loading Story Element from Path
    public Error Load(string path)
    {
        Error err = Error.Ok;
        ConfigFile configFile = new ConfigFile();
        err = configFile.Load(path);
        if (err != Error.Ok) { GD.Print("Story Element Load Failed:" + err); return err; }
        StoryElementName = configFile.GetValue("StoryElement", "Name") as string;
        StoryElementDescription = configFile.GetValue("StoryElement", "Description") as string;
        StoryElementType = (StoryElementType)Enum.Parse(typeof(StoryElementType), configFile.GetValue("StoryElement", "Type") as string);
        Dictionary TypeDataDictionary = configFile.GetValue("StoryElementType", "Data") as Dictionary;  // Godot Problem: "as Dictionary<string, string>" return null
        StoryElementTypeData = new Dictionary<string, object>(TypeDataDictionary);
        return err;
    } 

    // Save Story Element
    public Error Save() { return Save(StoryElementPath); }
    // Load Story Element
    public Error Load() { return Load(StoryElementPath); }
    // Reload Story Element
    public Error Reload() { return Load(); }
    // Import Story Element
    public Error Import() { return Error.Ok; }
    // Export Story Element | ~List of Character(csv), Single File(json)
    public Error Export() { return Error.Ok; } 
    // Move Story Element
    public Error Move(string path) { Error err = FileManager.MoveFile(StoryElementPath, path); StoryElementPath = path; return err; }

}

/*
Story Element/Entity Informations
Seid: <PathOfFile>
---
[StoryElement]
Name: "", 
Description: ""
StoryElementType: <Type>

[StoryElementType]
<TypeInfo1>
<TypeInfo2>
<TypeInfo3>

*/

/*
StoryElement General Informations (All time included)
Id, Name, Description, ~Template 
StoryElement Template Informations (After selected template)
~Content

--------------

Story Flow Informations
[Story Element]
{
    Id: "", 
    Name: "", 
    Description: "",
    Tag: "",
    ~Template: "",
    ~Content: {
        ...
    }
}

[Story Flow]
Fragments: 
[
    Fragment0: {
        Content: "..."
        Reference: "/Flows/Chapter2"
    }
]

Nodes: 
[
    Node0: {
        Offset: (0,0),
        Slot: "",
        Previous: "",
        Next: "Node1",
        Fragment: Fragment0
    }
    Node1: {
        Offset: (0,0),
        Slot: "",
        Previous: "Node0",
        Next: "",
        Fragment: Fragment0
    }
]

Connections: 
[
    Connection0: {
        From: "",
        FromSlot: "",
        To: "",
        ToSlot: ""
    }
    Connection1: {
        ...
    }
]

-- 

~Fragment Stored Data
~Nodes Displayed Positions/Offsets

~Combine Node and Fragment

-----------------

Reference Informations

Tag Informations

[Hero]  
ids: 
{
    "/.../...",
    "/.../...",
    "/.../..."
}

[Enemy]  
ids: 
{
    "/.../...",
    "/.../...",
    "/.../..."
}
*/