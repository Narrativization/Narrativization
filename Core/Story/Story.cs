using Godot;
using System;
using Godot.Collections;
using Array = Godot.Collections.Array;

namespace Storier
{
    // Story(Project) 
    public class Story : Node
    {
        // Custom Story Data
        public Dictionary<string, string> StoryData { get; set; } = new Dictionary<string, string>(); 
        
        // Story Data
        public string StoryName { get { return StoryData["Name"]; } set { Name = StoryData["Name"] = value; } } // Current Story Name
        public string StoryDescription { get { return StoryData["Description"]; } set { StoryData["Description"] = value; } } // Current Story Description
        public string StoryDirectory { get; set; } // Current Story Directory

        // Story Informations
        // Id of Opened File/Directory
        public string FlowTitle { get; set; } 
        // Full Path of Flow
        public string FlowFileName { get {return StoryDirectory.GetBaseDir() + FlowTitle; } } // FlowTitle Start with "/" Character 


        public StoryData storyData = new StoryData();
        public static string flowTitle {get { return StoryManager.CurrentStory.Name + flowTarget; } }  // ~Folder View and Flow View 
        public static string flowTarget = ""; // ~Folder View and Flow View 

        private Story() 
        {
            StoryName = "Untitled Story";
            StoryDescription = "";
            StoryDirectory = "";
        }
        public Story(string _name, string _description, string _directory) 
        {
            StoryName = _name;
            StoryDescription = _description;
            StoryDirectory = _directory;
        }        
        public Story(string _name, string _description, string _directory, string _author, string _copyright) 
        {
            StoryName = _name;
            StoryDescription = _description;
            StoryDirectory = _directory;
            StoryData.Add("Author", _author);
            StoryData.Add("Copyright", _copyright);
        }
        private Story(StoryData _storyData) // Create New Story Old 
        {
            storyData = _storyData;
            Name = storyData.name;
            flowTarget = "/" + "Flows" + "/";

            CreateHierarchy();
            StoryName = "Untitled Story";
            StoryDescription = "";
            StoryDirectory = "";    
        }
        

        public override void _Ready()
        {
            AddToGroup("Story");
        }

    //  public override void _Process(float delta)
    //  {
    //      
    //  }

        public override void _Input(InputEvent inputEvent)
        {
            if (inputEvent.IsActionPressed("Help"))
            {
                GD.Print(StoryData);
                GD.Print("Done!");
            }
        }
        
        // Create Folder/File Structure of project
        public void CreateHierarchy()
        {
            if (StoryDirectory  == "") { GD.Print("Story Directory can't found!"); return; }
            Godot.Collections.Array<string> defaultDirs = new Godot.Collections.Array<string>()
            {
                StoryDirectory + "Flows/",
                StoryDirectory + "Entities/",
                StoryDirectory + "Entities/Characters/",
                StoryDirectory + "Entities/Locations/",
                // StoryDirectory + "Entities/Items/",
                StoryDirectory + "Assets/",
                // StoryDirectory + "Journeys/"
                StoryDirectory + "Templates/",
                StoryDirectory + "References/"
            };
            Directory dir = new Directory();
            for (int i = 0; i < defaultDirs.Count; i++)
            {
                dir.MakeDirRecursive(defaultDirs[i]);
            }
        }

        public Dictionary LookHierarchy()
        {
            GD.Print(FileManager.DirectoryTree(StoryDirectory));
            GD.Print((StoryDirectory + flowTarget));
            GD.Print(FileManager.DirectoryContents(StoryDirectory + flowTarget));
            return FileManager.DirectoryTree(StoryDirectory);
        }

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

/*
    Create a project file structure | .storier
    {
        Story: {
            Name: "Storier",  // Title
            Description: "Story Managing Software for Game Creators.",
            Author: "Atakan Ateş",
            Copyright: "Copyright © Atakan Ateş, All Rights Reserved!"
        },
        Storier: {
            Version: "v<Major>.<Minor>"
            // 
        }
    }
    Additional Story Informations
    + Directory
*/