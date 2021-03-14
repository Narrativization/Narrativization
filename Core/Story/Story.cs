using Godot;
using System;
using Godot.Collections;

namespace Storier
{
    // Story(Project) 
    public class Story : Node
    {
        // Story Data
        public StoryData storyData = new StoryData();
        public static string flowTarget = ""; // ~Folder View and Flow View 

        private Story() {}
        public Story(StoryData _storyData) // Create New Story 
        {
            storyData = _storyData;
            Name = storyData.name;
            flowTarget = Name + "/" + "Flow" + "/";
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
            return FileManager.DirectoryContents(storyData.directory);
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