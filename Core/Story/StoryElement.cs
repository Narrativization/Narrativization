using Godot;
using System;
using Godot.Collections;

// Story Elements / Narrative Elements
public class StoryElement : Resource
{
    // Custom Story Element Data
    public Dictionary<string, string> StoryElementData { get; set; } = new Dictionary<string, string>();

    // Story Element Data 
    public string StoryElementId { get { return StoryElementData["Id"]; } set { StoryElementData["Id"] = value; } } // Example: /Entity/Character/MyChar | Relative path to project(<ProjectName>.storier)
    public string StoryElementName { get { return StoryElementData["Name"]; } set { ResourceName = StoryElementData["Name"] = value; } } // Example: MyChar | Uses ResourceName 
    public string StoryElementDescription { get { return StoryElementData["Description"]; } set { StoryElementData["Description"] = value; } }  // Example: Character Example
    public string StoryElementTag { get { return StoryElementData["Tag"]; } set { StoryElementData["Tag"] = value; } }  // Example: Awesome Person
    
    // Story Element Informations
    public StoryElementType StoryElementType { get; set; } = StoryElementType.Asset; 
    public string StoryElementPath { get{ return ResourcePath; } set{ TakeOverPath(value); } } // StoryElementPath = StoryBase[.../<StoryName>] + StoryElementId[/Flow/Chapter1] + "." + StoryElementExtension[flow]
    public string StoryElementExtension { get { return StoryElementPath.Extension(); } } // Example ext: "ext" 
    
    // public string Template { get { return StoryElementData["Template"]; } set { StoryElementData["Template"] = value; } }  // Template reference
    // public Dictionary<string, object> TemplateData { get; set; } = new Dictionary<string, object>();

    protected StoryElement() {}
    // Load Story Element
    // public StoryElement(string id)
    // {
        
    // }
    // Create Story Element
    public StoryElement(string path, string name = "", string description = "") // path: fullPath
    {
        if (!path.StartsWith(StoryManager.CurrentStory.StoryBase)) { GD.PushWarning("Story Element isn't in Workspace!\n" + path); return; }
        StoryElementId = path.Replace(StoryManager.CurrentStory.StoryBase, "").Replace("." + path.Extension(), "");
        StoryElementName = name; 
        StoryElementDescription = description; 
        StoryElementPath = path;
    }

    // public StoryElement(string path)
    // {
    //     if (!path.StartsWith(StoryManager.CurrentStory.StoryBase)) { GD.PushWarning("Story Element isn't in Workspace!\n" + path); return; }
    //     StoryElementId = path.Replace(StoryManager.CurrentStory.StoryBase, "").Replace("." + path.Extension(), "");
    //     StoryElementName = path.GetFile().Replace("." + path.Extension(), "");
    //     StoryElementPath = path;
        
    // }
    // public StoryElement(string name, string description, string path)
    // {
    //     if (!path.StartsWith(StoryManager.CurrentStory.StoryBase)) { GD.PushWarning("Story Element isn't in Workspace!\n" + path); return; }
    //     StoryElementId = path.Replace(StoryManager.CurrentStory.StoryBase, "").Replace("." + path.Extension(), "");
    //     StoryElementName = name;
    //     StoryElementDescription = description;
    //     StoryElementPath = path;
    // }

    // Replace/Move Story Element | Not ready for use -> Move to File Manager?
    private Error Move(string destination)
    {
        Directory dir = new Directory();
        Error errCopy = dir.Copy(StoryElementPath, destination);
        if (errCopy != Error.Ok) { return errCopy; }
        Error errDelete = dir.Remove(StoryElementPath);
        if (errDelete != Error.Ok) { return errDelete; }
        return Error.Ok;
    }

    // Change Story Element Id(Relative Path) 
    public Error ReId(string id)
    {
        string _destination = StoryManager.CurrentStory.StoryBase + StoryElementId + StoryElementExtension;
        StoryElementId = StoryElementId.Replace(StoryElementName, id);
        Error err = Move(_destination);
        StoryElementPath = _destination;
        return err; // Error.Ok;
    }   

    // Saving Story Element | ~Maybe create Resource Format Saver/Loader
    public virtual bool Save() { return FileManager.SaveFile(StoryElementPath, JSON.Print(StoryElementData)); } // ~Save text
    // Reloading Story Element
    public virtual bool Reload() { return false; }
    // Import Story Element
    public virtual void Import() { }
    // Export Story Element
    public virtual void Export() { } // ~List of Character(csv), Single File(json)

    // Loading Story Element
    public static StoryElement Load(string path) 
    { 
        StoryElement loaded = new StoryElement(path);
        JSONParseResult jsonObject = JSON.Parse(FileManager.LoadFile(path));
        if(jsonObject.Error != 0) { GD.Print("Error:", jsonObject.ErrorLine); return null; } 
        else
        {
            Dictionary elementDictionary = jsonObject.Result as Dictionary;  // Godot Problem: "as Dictionary<string, string>" return null
            loaded.StoryElementData = new Dictionary<string, string>(elementDictionary);
            // GD.Print("Story Element Data:\n" + JSON.Print(loaded.StoryElementData, "\t"));
        }
        return loaded; 
    }
}


/*
Story Element/Entity Informations
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
Fragments: {
    Fragment0: {
        Content: "..."
        Reference: "/Flows/Chapter2"
    }
}
Nodes: {
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
}

++ 

~Fragment Stored Data
~Nodes Displayed Positions/Offsets

~Combine Node and Fragment

*/