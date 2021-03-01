using Godot;
using System;
using Godot.Collections;

namespace Storier
{
    // Story(Project) 
    public class Story : Node
    {
        // Current Story
        public static Story story;

        // Story Data
        public StoryData storyData = new StoryData();

        
        public Story() { story = this; } // Create New Empty Story 
        // Create New Story 
        public Story(StoryData _storyData)
        {
            story = this;
            storyData = _storyData;
            Name = storyData.name;
            StoryManager.PushRecentStory();
            SaveStory();
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

        public void SaveStory()
        {
            Dictionary saveData = new Dictionary() {
                {
                    "Story", storyData.SaveData()
                }
                // ,
                // {
                //     "Storier", new Dictionary(){
                //         {"Version", "v0.2"}
                //     }
                // }
            };
            FileManager.SaveFile(storyData.directory + storyData.name + ".storier", JSON.Print(saveData, "\t"));
            CreateHierarchy();
        }

        public void LoadStory(string fileName)
        {
            string str = FileManager.LoadFile(fileName);
            JSONParseResult jsonObject = JSON.Parse(str);
            if(jsonObject.Error != 0) { GD.Print("Error:", jsonObject.ErrorLine); } 
            else
            {
                // GD.Print(JSON.Print(jsonObject.Result));
                Dictionary storyJson = jsonObject.Result as Dictionary; // Godot.Collections.Array  <-  [] 
                storyData = new StoryData(fileName.BaseName(), "", fileName.GetBaseDir());

                if (storyJson.Contains("Story"))
                {
                    Dictionary storyInfos = storyJson["Story"] as Dictionary; 
                    storyData.LoadData(storyInfos);
                }
                GD.Print(storyData);
            }
            // Story Name/Recent
            Name = storyData.name;
            StoryManager.PushRecentStory();
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