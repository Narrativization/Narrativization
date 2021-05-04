using Godot;
using System;
using Godot.Collections;
using Array = Godot.Collections.Array;

// Story(Project) 
public class Story : Node
{
    // Custom Story Data
    public Dictionary<string, string> StoryData { get; set; } = new Dictionary<string, string>(); 
    
    // Story Data
    public string StoryName { get { return StoryData["Name"]; } set { Name = StoryData["Name"] = value; } } // Current Story Name
    public string StoryDescription { get { return StoryData["Description"]; } set { StoryData["Description"] = value; } } // Current Story Description

    // Story Informations
    public string StoryPath { get { return StoryDirectory + StoryName + ".storier"; } } // Current Story Path
    public string StoryDirectory { get; set; } // Current Story Directory
    public string StoryBase { get { return StoryDirectory.GetBaseDir(); } } // Story Base Directory

    private Story() 
    {
        StoryName = "Untitled Story";
        StoryDescription = "";
        StoryDirectory = "";
    }
    public Story(string name, string description, string directory) 
    {
        StoryName = name;
        StoryDescription = description;
        StoryDirectory = directory;
        ValidateHierarchy(directory);
    }        
    public Story(string name, string description, string directory, string author, string copyright) 
    {
        StoryName = name;
        StoryDescription = description;
        StoryDirectory = directory;
        StoryData.Add("Author", author);
        StoryData.Add("Copyright", copyright);
        ValidateHierarchy(directory);
    }

    public override void _Ready()
    {
        AddToGroup("Story");
    }

//  public override void _Process(float delta)
//  {
//      
//  }

    // Validate Story Directory
    private void ValidateHierarchy(string directory)
    {
        if (!(new Directory()).DirExists(directory)) 
        { 
            StoryManager.CurrentStoryManager.EmitSignal("StoryCreated", this);
            CreateHierarchy(); 
        } 
    }
    
    // Create Folder/File Structure of project
    public void CreateHierarchy()
    {
        if (StoryDirectory  == "") { GD.Print("Story Directory can't found!"); return; }
        Godot.Collections.Array<string> defaultDirs = new Godot.Collections.Array<string>()
        {
            StoryBase + "/Flows/",
            StoryBase + "/Entities/",
            StoryBase + "/Entities/Characters/",
            StoryBase + "/Entities/Locations/",
            // StoryBase + "/Entities/Items/",
            StoryBase + "/Assets/",
            // StoryBase + "/Journeys/",
            // StoryBase + "/Templates/",
            // StoryBase + "/References/"
        };
        Directory dir = new Directory();
        for (int i = 0; i < defaultDirs.Count; i++)
        {
            dir.MakeDirRecursive(defaultDirs[i]);
        }
    }

    // ~Open Default/Recent Flow
    public void CreateDefaultFlow()
    {
        StoryElement defaultStory = new StoryElement(StoryBase + "/Flows/Starter.flow");
        defaultStory.Save();
        StorierManager.StorierData.Add("Starter", "/Flows/Starter.flow");
        StoryManager.CurrentStoryElement = defaultStory;
    }

    public Dictionary LookHierarchy()
    {
        GD.Print(JSON.Print(FileManager.DirectoryTree(StoryDirectory), "\t"));
        return FileManager.DirectoryTree(StoryDirectory);
    }

}


/*
Story Elements(Hierarchy) - File/Folder Structure
- Story(.storier) -> json
    - Flows(.flow) -> config
    - Entities(.entity) -> json
        - Characters
        - Locations
        - ~Items
    - Assets -> png, jpg, txt, csv... 
    - Journeys() -> config
    - Templates(.template) -> config, json, ~xml, tscn
    - References(.reference) -> config
*/