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
    public string StoryElementId { get; set; } // Example: /Entity/Character/MyChar | Relative path to project(<ProjectName>.storier)
    public string StoryElementPath { get{ return ResourcePath; } set{ TakeOverPath(value); } } // StoryElementPath = StoryBase[.../<StoryName>] + StoryElementId[/Flow/Chapter1] + "." + StoryElementExtension[flow]
    public string StoryElementExtension { get { return StoryElementPath.Extension(); } } // Example ext: "ext" 

    // Reference Data
    // public string StoryElementTag { get { return StoryElementData["Tag"]; } set { StoryElementData["Tag"] = value; } }  // Example: Awesome Person

    // private StoryElementType _StoryElementType { get; set; }
    // public StoryElementType StoryElementType 
    // { 
    //     get
    //     {
    //         if (StoryElementId.StartsWith("/Flows/")) { _StoryElementType = StoryElementType.Flow; }
    //         else if (StoryElementId.StartsWith("/Entities/")) { _StoryElementType = StoryElementType.Entity; }
    //         else if (StoryElementId.StartsWith("/Assets/")) { _StoryElementType = StoryElementType.Asset; }
    //         else if (StoryElementId.StartsWith("/References/")) { _StoryElementType = StoryElementType.Reference; }
    //         else { _StoryElementType = StoryElementType.Asset; }
    //         return _StoryElementType;
    //     }
    // }

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

    // public static string StoryElementType(StoryElement storyElement)
    // {
    //     string storyElementId = storyElement.StoryElementId;
    //     string storyElementType = "";
    //     if (storyElementId.StartsWith("/Flows/")) { storyElementType = "Flow"; }
    //     else if (storyElementId.StartsWith("/Entities/")) { storyElementType = "Entity"; }
    //     else if (storyElementId.StartsWith("/Assets/")) { storyElementType = "Asset"; }
    //     else if (storyElementId.StartsWith("/References/")) { storyElementType = "Reference"; }
    //     else { storyElementType = "Asset"; }
    //     return storyElementType;
    // }

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

    // // Loading Story Element
    // public static StoryElement Load(string path) 
    // { 
    //     StoryElement loaded = new StoryElement(path);
    //     JSONParseResult jsonObject = JSON.Parse(FileManager.LoadFile(path));
    //     if(jsonObject.Error != 0) { GD.Print("Error:", jsonObject.ErrorLine); return null; } 
    //     else
    //     {
    //         Dictionary elementDictionary = jsonObject.Result as Dictionary;  // Godot Problem: "as Dictionary<string, string>" return null
    //         loaded.StoryElementData = new Dictionary<string, string>(elementDictionary);
    //         // GD.Print("Story Element Data:\n" + JSON.Print(loaded.StoryElementData, "\t"));
    //     }
    //     return loaded; 
    // }

    // ----------------------Flow---------------------------
    

    // public override bool Save()
    // {
    //     ConfigFile configFile = new ConfigFile();
    //     configFile.SetValue("StoryElement", "Data", StoryElementData);
    //     configFile.SetValue("Flow", "Data", StoryFlowData);
    //     Error err = configFile.Save(ResourcePath);
    //     if (err != Error.Ok) { GD.Print(err); return false; }
    //     return true;
    // }

    // // Loading Story Element
    // public static new StoryFlow Load(string path) 
    // { 
    //     StoryFlow loaded = new StoryFlow(path);
    //     ConfigFile configFile = new ConfigFile();
    //     Error err = configFile.Load(path);
    //     if (err != Error.Ok) { GD.Print(err); return null; }
    //     Dictionary elementDictionary = configFile.GetValue("StoryElement", "Data") as Dictionary;  // Godot Problem: "as Dictionary<string, string>" return null
    //     loaded.StoryElementData = new Dictionary<string, string>(elementDictionary);
    //     Dictionary flowDictionary = configFile.GetValue("Flow", "Data") as Dictionary;  // Godot Problem: "as Dictionary<string, string>" return null
    //     loaded.StoryFlowData = new Dictionary<string, string>(flowDictionary);
    //     return loaded; 
    // }



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
[StoryElement]
{
    Id: "", 
    Name: "", 
    Description: ""
}

[StoryEntity]
~Template: "",
~Content: {
    ...
}

++

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

++ 

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

Story Element Types
Flow        // Story Flow: Plot and Scenes and more...
Entity      // Story Entity:  Character, Settings and more...
Asset       // Story Asset: Images, Notes and more...
Reference   // Story Reference: Mention, Tag and more...  
~Template   // Story Template: User Generated Nodes and more...


*/