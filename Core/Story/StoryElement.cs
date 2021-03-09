using Godot;
using System;


namespace Storier
{
    public enum StoryElementType
    {
        Flow, Entity, Asset // ~ ,Reference, Template
    }

    public class StoryElement : Resource
    {
        public StoryElementType storyElementType = StoryElementType.Entity;
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
            string storyElementPath = StoryManager.currentStory.storyData.directory + "" + _relativePath;
            StoryElement storyElement = new StoryElement(_name, storyElementPath, true);
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