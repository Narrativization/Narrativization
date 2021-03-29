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
        private static Dictionary<string, string> StoryData { get; set; } = new Dictionary<string, string>(); 
        
        // Story Data
        public static string StoryName { get { return StoryData["Name"]; } set { StoryData["Name"] = value; } } // Current Story Name
        public static string StoryDescription { get { return StoryData["Description"]; } set { StoryData["Description"] = value; } } // Current Story Description
        public static string StoryDirectory { get { return StoryData["Directory"]; } set { StoryData["Directory"] = value; } } // Current Story Directory

        // Story Informations
        // Id of Opened File/Directory
        public static string FlowTitle { get; set; } 
        // Full Path of Flow
        public static string FlowFileName { get {return StoryDirectory + FlowTitle; } } 


        public StoryData storyData = new StoryData();
        public static string flowTitle {get { return StoryManager.currentStory.Name + flowTarget; } }  // ~Folder View and Flow View 
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
            CreateHierarchy();
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

        public static string SaveData()
        {
            return StoryData.ToString();
        }

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
            if (storyData.directory == "") { GD.Print("Story Directory can't found!"); return; }
            Godot.Collections.Array<string> defaultDirs = new Godot.Collections.Array<string>()
            {
                storyData.directory + "Flows/",
                storyData.directory + "Entities/",
                storyData.directory + "Entities/Characters/",
                storyData.directory + "Entities/Locations/",
                // storyData.directory + "Entities/Items/",
                storyData.directory + "Assets/",
                // storyData.directory + "Journeys/"
                storyData.directory + "Templates/",
                storyData.directory + "References/"
            };
            Directory dir = new Directory();
            for (int i = 0; i < defaultDirs.Count; i++)
            {
                dir.MakeDirRecursive(defaultDirs[i]);
            }
        }

        public Dictionary LookHierarchy()
        {
            GD.Print(FileManager.DirectoryTree(storyData.directory));
            GD.Print((storyData.directory + flowTarget));
            GD.Print(FileManager.DirectoryContents(storyData.directory + flowTarget));
            return FileManager.DirectoryTree(storyData.directory);
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