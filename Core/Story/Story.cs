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
        

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            AddToGroup("Story");
        }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }

        private bool SaveStory()
        {
            
            return true;
        }

        public void LoadStory()
        {
            
        }

        // Create Folder/File Structure of project
        public void CreateHierarchy()
        {
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
    Create a project file structure | .storier
    {
        Story: {
            Name: "Storier",  // Title
            Description: "Story Managing Software for Game Creators.",
            Author: "Atakan Ateş",
            Copyright: "Copyright © Atakan Ateş, All Rights Reserved!"
        },
        Storier: {
            // 
            // Version: "v0.2"
        }
    }
    Additional Story Informations
    + Directory
*/