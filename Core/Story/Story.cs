using Godot;
using System;
using Godot.Collections;
using Array = Godot.Collections.Array;

// Story(Project) 
public class Story : Node
{
    // Change Parent Class to Resource


    // Custom Story Data
    public Dictionary<string, string> StoryData { get; set; } = new Dictionary<string, string>(); 
    
    // Story Data
    public string StoryName { get { return StoryData["Name"]; } set { Name = StoryData["Name"] = value; } } // Current Story Name
    public string StoryDescription { get { return StoryData["Description"]; } set { StoryData["Description"] = value; } } // Current Story Description

    // Story Informations
    public string StoryPath { get { return StoryDirectory + StoryName + ".storier"; } } // Current Story Path
    public string StoryDirectory { get; set; } // Current Story Directory
    public string StoryBase { get { return StoryDirectory.GetBaseDir(); } } // Story Base Directory

    public Story() 
    {
        StoryName = "Untitled Story";
        StoryDescription = "";
        StoryDirectory = "";
    }
    public Story(string name, string description, string directory, string author, string copyright) 
    {
        StoryName = name;
        StoryDescription = description;
        StoryDirectory = directory;
        StoryData.Add("Author", author);
        StoryData.Add("Copyright", copyright);
        CreateHierarchy(); 
    }

    public override void _Ready()
    {
        AddToGroup("Story");
    }

//  public override void _Process(float delta)
//  {
//      
//  }
    
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

    public Array GetDirectoryTree()
    {
        GD.Print(FileManager.DirectoryTreeAsArray(StoryDirectory));
        return FileManager.DirectoryTreeAsArray(StoryDirectory);
    }
}


/*
Story Directory Structure - Project File/Folder Structure
- Story(.storier)       -> json
    - Flows             -> config (storyel)
    - Entities          -> config (storyel)
        - Characters
        - Locations
        - ~Items
    - Assets            -> config, png, jpg, txt, csv... 
    - Journeys          -> config
    - Templates         -> config, json, ~xml, tscn
    - References        -> config
*/