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

        private Story() {}
        public Story(StoryData _storyData) // Create New Story 
        {
            storyData = _storyData;
            Name = storyData.name;
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

        // // Remove Creating From story every element create themself
        // public static StoryElement CreateStoryElement(string _name = "NewStoryElement", string _relativePath = "/")
        // {
        //     string storyElementPath = StoryManager.currentStory.storyData.directory + _relativePath;
        //     return new StoryElement(_name, storyElementPath);
        // }

        // // Create Story Entity from anywhere
        // public static StoryEntity CreateStoryEntity(string _name = "NewStoryEntity", string _relativePath = "/Entities/Characters/")
        // {
        //     string storyElementPath = StoryManager.currentStory.storyData.directory + _relativePath;
        //     return new StoryEntity(_name, storyElementPath);
        // }

        // // Create Story Flow from anywhere
        // public static StoryFlow CreateStoryFlow(string _name = "NewStoryFlow", string _relativePath = "/Flows/")
        // {
        //     string storyElementPath = StoryManager.currentStory.storyData.directory + _relativePath;
        //     return new StoryFlow(_name, storyElementPath);
        // }

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