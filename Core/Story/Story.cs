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
        public string StoryDirectory { get; set; } // Current Story Directory | isn't stored in ".storier"

        // Story and Element Informations
        public string StoryBase { get { return StoryDirectory.GetBaseDir(); } } // Story Base Directory
        public StoryElement CurrentStoryElement { get; set; } // Current Story Element

        // public string DisplayTitle { get; set; } // Current Title | For Displaying Folder in Flow Editor 

        // StoryElementPath = StoryBase[.../<StoryName>] + StoryElementId[/Flow/Chapter1] + StoryElementExtension
        public string StoryElementId { get { return IsInstanceValid(CurrentStoryElement) ? CurrentStoryElement.Id : ""; } } // Id of opened Relative File/Directory without extension [/Flow/Chapter]
        public string StoryElementExtension { get { return IsInstanceValid(CurrentStoryElement) ? CurrentStoryElement.StoryElementExtension : ""; } } // Extension of opened Relative File/Directory [.flow]
        public string StoryElementPath { get { return StoryBase + CurrentStoryElement.Id + CurrentStoryElement.StoryElementExtension; } } // Path of opened File ...[/Flow/Chapter]...

        // private StoryElement _currentStoryElement { get; set; } // Current Story Element
        // public StoryElement CurrentStoryElement { get { return _currentStoryElement; } set{ _currentStoryElement = value; EmitSignal("StoryElementUpdated", _currentStoryElement);  } } // Current Story Element
        // [Signal]
        // public delegate void StoryElementUpdated(StoryElement storyElement);

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
        }        
        public Story(string name, string description, string directory, string author, string copyright) 
        {
            StoryName = name;
            StoryDescription = description;
            StoryDirectory = directory;
            StoryData.Add("Author", author);
            StoryData.Add("Copyright", copyright);
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
                GD.Print("Help");
                // StoryEntity se = new StoryEntity(StoryBase + "/Entities/Characters/F1.entity");
                // se.Save();
                // GD.Print(StoryEntity.Load(StoryBase + "/Entities/Characters/F1.entity"));
            }
            else if (inputEvent.IsActionPressed("Fullscreen"))
            {
                GD.Print("Fullscreen");
                OS.WindowFullscreen = !OS.WindowFullscreen;
            }
            if (inputEvent.IsActionPressed("Save"))
            {
                GD.Print("Save");
                if (IsInstanceValid(CurrentStoryElement))
                {
                    Editors editors = GetTree().GetNodesInGroup("Editors")[0] as Editors;
                    CurrentStoryElement.StoryElementData["Flows"] = JSON.Print(editors.flowEditor.SaveData(), "\t");
                    CurrentStoryElement.Save();
                }
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
                // StoryBase + "/Journeys/"
                StoryBase + "/Templates/",
                StoryBase + "/References/"
            };
            Directory dir = new Directory();
            for (int i = 0; i < defaultDirs.Count; i++)
            {
                dir.MakeDirRecursive(defaultDirs[i]);
            }

            // StoryFlow defaultStory = new StoryFlow(StoryBase + "/Flows/Starter.flow");
            // StoryManager.StorierData.Add("Starter", "/Flows/Starter.flow");
            // defaultStory.Save();
            // CurrentStoryElement = defaultStory;
        }

        public Dictionary LookHierarchy()
        {
            GD.Print(JSON.Print(FileManager.DirectoryTree(StoryDirectory), "\t"));
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