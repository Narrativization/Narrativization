using Godot;
using System;

namespace Storier
{
    public enum StoryElementType
    {
        None, Flow, Entity, Asset // ~ ,Reference, Template
    }

    // Base Class of Story Elements
    public class StoryElement : Resource
    {
        public StoryElementType storyElementType = StoryElementType.None;

        public string defaultTemplate = "res://Templates/DefaultTemplate.tscn";

        protected StoryElement() {}
        public StoryElement(string _name, string _path, bool _localToScene = false)
        {
            ResourceName = _name;
            TakeOverPath(_path); // _path + "/" + _name + _ext -> Use full path with Extension 
            ResourceLocalToScene = _localToScene;
        }

        // Saving Story Element | ~Maybe create Resource Format Saver/Loader
        public virtual void Save(string _data)
        {
            if (storyElementType != StoryElementType.None)
                GD.Print(ResourceName + " saved!");
        }

        // Loading Story Element | ~Maybe create Resource Format Saver/Loader
        public virtual void Load()
        {
            if (storyElementType != StoryElementType.None)
                GD.Print(ResourceName + " loaded!");
        }

        public virtual void Reload()
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