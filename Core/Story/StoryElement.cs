using Godot;
using System;


namespace Storier
{
    public class StoryElement : Resource
    {
        public string defaultTemplate = "res://Templates/DefaultTemplate.tscn";

        private StoryElement() {}
        public StoryElement(string _name, string _path, bool _localToScene = false)
        {
            ResourceName = _name;
            TakeOverPath(_path);
            ResourceLocalToScene = _localToScene;
        }

        // Creating Story Element from anywhere
        public static StoryElement CreateStoryElement(string _name = "NewStoryElement", string _relativePath = "/Entities/Characters/")
        {
            string sePath = StoryManager.currentStory.storyData.directory + "" + _relativePath;
            StoryElement storyElement = new StoryElement(_name, sePath, true);
            return storyElement;
        }

        // Saving Story Element | ~Maybe create Resource Format Saver/Loader
        public void Save(string _data)
        {
            if (!FileManager.SaveFile(ResourcePath + ResourceName + ".story", _data))
            {
                GD.Print(ResourceName + " save Failed!");
                return; 
            }
            GD.Print(ResourceName + " saved!");
        }

        public void Load()
        {
            // PackedScene packedScene = ResourceLoader.Load("res://PackedScene.tscn") as PackedScene;
            // Story customSprite = packedScene.Instance() as Story;
            // AddChild(customSprite);
        }

        public void Reload()
        {
            
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

Entity Example
{
    Id: "", 
    Name: "", 
    Description: "",
    Template: "",
    Data: {
        ...
    }
}

StoryElement General Informations (All time included to Property/Details Modal)
Id, Name, Description, Template 
StoryElement Template Informations (After selected template)
Data

*/