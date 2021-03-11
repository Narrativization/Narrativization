using Godot;
using System;

namespace Storier
{
    public enum StoryElementType
    {
        Flow, Entity, Asset // ~ ,Reference, Template
    }

    // Base Class of Story Elements
    public class StoryElement : Resource
    {
        // Relative path to project(<ProjectName>.storier)
        public string Id { get; protected set; } // Example: /Entity/Character/MyChar
        public string Name { get { return ResourceName; } set { ResourceName = value; } }  // Example: MyChar | Uses ResourceName 
        public string Description { get; set; }  // Example: Character Example

        public StoryElementType storyElementType = StoryElementType.Asset;
        // public string storyElementTemplate = "res://Templates/DefaultTemplate.tscn";
        public string storyElementExtension = ""; // Example ext: ".ext" 

        protected StoryElement() {}
        protected StoryElement(string _id, string _name, string _description, string _ext) 
        {
            InitStoryElement(_id, _name, _description, _ext);
        }

        protected void InitStoryElement(string _id, string _name, string _description, string _ext)
        {
            Id = _id;
            Name = ResourceName = _name;
            Description = _description;
            
            GD.Print(StoryManager.currentStory.storyData.directory + Id + _ext); // Del
            storyElementExtension = _ext;
            // Full path of StoryElement(with extension)
            TakeOverPath(StoryManager.currentStory.storyData.directory + Id + _ext);
        }

        // Replace/Move Story Element | Not ready for use -> Move to File Manager?
        private Error Move(string _destination)
        {
            Directory dir = new Directory();
            Error errCopy = dir.Copy(ResourcePath, _destination);
            if (errCopy != Error.Ok) { return errCopy; }
            Error errDelete = dir.Remove(ResourcePath);
            if (errDelete != Error.Ok) { return errDelete; }
            return Error.Ok;
        }

        // Change Id(Relative Path) Story Element 
        public Error ReId(string _id)
        {
            string _destination = StoryManager.currentStory.storyData.directory + Id + storyElementExtension;
            Id = Id.Replace(Name, _id);
            Error err = Move(_destination);
            TakeOverPath(_destination);
            return err; // Error.Ok;
        }         

        // Saving Story Element | ~Maybe create Resource Format Saver/Loader
        public virtual void Save(string _data) { } // ~Save text  -- Change to bool?
        // Reloading Story Element
        public virtual void Reload() { }
        // Import Story Element
        public virtual void Import() { }
        // Export Story Element
        public virtual void Export() { } // ~List of Character(csv), Single File(json)
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