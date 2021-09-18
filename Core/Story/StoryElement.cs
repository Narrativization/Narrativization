using Godot;
using System;
using Godot.Collections;

// Story Elements / Narrative Elements
public class StoryElement : Resource
{
    // Custom Story Element Data
    public Dictionary<string, string> StoryElementData { get; set; } = new Dictionary<string, string>();

    // Story Element Data 
    public string StoryElementName { get { return StoryElementData["Name"]; } set { StoryElementData["Name"] = ResourceName = value; } } // Example: MyChar | Uses ResourceName 
    public string StoryElementDescription { get { return StoryElementData["Description"]; } set { StoryElementData["Description"] = value; } }  // Example: Character Example
    
    // Story Element Informations
    public string StoryElementId { get; set; } // Example: /Entity/Character/MyChar | Relative path to project(<ProjectName>.storier) | StoryElementId(Seid)
    public string StoryElementPath { get{ return ResourcePath; } set{ TakeOverPath(value); } } // StoryElementPath = StoryBase[.../<StoryName>] + StoryElementId[/Flow/Chapter1] + "." + StoryElementExtension[flow]
    public string StoryElementExtension { get { return StoryElementPath.Extension(); } } // Example ext: "ext" 

    protected StoryElement() {}
    // Create Story Element
    public StoryElement(string path, string name = "", string description = "") // path: fullPath
    {
        if (!path.StartsWith(StoryManager.CurrentStory.StoryBase)) { GD.PushWarning("Story Element isn't in Workspace!\n" + path); return; }
        StoryElementId = path.Replace(StoryManager.CurrentStory.StoryBase, "").Replace("." + path.Extension(), "");
        StoryElementName = name; 
        StoryElementDescription = description; 
        StoryElementPath = path;
        ValidateStoryElement(path);
    }

    // Validate Story Element Directory
    private void ValidateStoryElement(string path)
    {
        if (!(new Directory()).FileExists(path)) 
        { 
            StoryManager.CurrentStoryManager.EmitSignal("StoryElementCreated", this);
            Save();
        } 
        else
        {
            Load();
        }
        StoryManager.CurrentStoryManager.EmitSignal("StoryElementChanged", this);
    }

    // Saving Story Element | ~Maybe create Resource Format Saver/Loader
    public virtual bool Save() 
    { 
        bool isSuccess = false;
        ConfigFile configFile = new ConfigFile();
        configFile.SetValue("StoryElement", "Data", StoryElementData);
        Error err = configFile.Save(ResourcePath);
        if (err != Error.Ok) { GD.Print(err); return false; }
        return isSuccess; 
    }
    
    protected virtual bool Load()
    {
        bool isSuccess = false;
        ConfigFile configFile = new ConfigFile();
        Error err = configFile.Load(StoryElementPath);
        if (err != Error.Ok) { GD.Print(err); return false; }
        Dictionary elementDictionary = configFile.GetValue("StoryElement", "Data") as Dictionary;  // Godot Problem: "as Dictionary<string, string>" return null
        StoryElementData = new Dictionary<string, string>(elementDictionary);
        return isSuccess;
    } 
    
    // Reloading Story Element
    public bool Reload() { return Load(); }
    // Import Story Element
    public void Import() { }
    // Export Story Element
    public void Export() { } // ~List of Character(csv), Single File(json)

    // ----------------------Old---------------------------

    // // Replace/Move Story Element | Not ready for use -> Move to File Manager?
    // private Error Move(string destination)
    // {
    //     Directory dir = new Directory();
    //     Error errCopy = dir.Copy(StoryElementPath, destination);
    //     if (errCopy != Error.Ok) { return errCopy; }
    //     Error errDelete = dir.Remove(StoryElementPath);
    //     if (errDelete != Error.Ok) { return errDelete; }
    //     return Error.Ok;
    // }

    // // Change Story Element Id(Relative Path) 
    // public Error ReId(string id)
    // {
    //     string _destination = StoryManager.CurrentStory.StoryBase + StoryElementId + StoryElementExtension;
    //     StoryElementId = StoryElementId.Replace(StoryElementName, id);
    //     Error err = Move(_destination);
    //     StoryElementPath = _destination;
    //     return err; // Error.Ok;
    // }   

}

/*
Story Element/Entity Informations
Seid: <PathOfFile>
---
[StoryElement]
Id: <Unique id>, 
Name: "", 
Description: ""
StoryElementType: <Type>

[StoryElementType]
<TypeInfo1>
<TypeInfo2>
<TypeInfo3>

---

Storier: Fix References -> ReMap(Validate) UniqueId and Seid  
Unique: StoryElement-0, StoryElement-1, StoryElement-10, StoryElement-1000, ...

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