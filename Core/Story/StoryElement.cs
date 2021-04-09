using Godot;
using System;
using Godot.Collections;

namespace Storier
{
    public enum StoryElementTypes
    {
        Flow, Entity, Asset // ~ ,Reference, Template
    }

    // Base Class of Story Elements
    public class StoryElement : Resource
    {
        // Custom Story Element Data
        public Dictionary<string, string> StoryElementData { get; set; } = new Dictionary<string, string>();

        // Story Element Data 
        public string Id { get { return StoryElementData["Id"]; } set { ResourceName = StoryElementData["Id"] = value; } } // Example: /Entity/Character/MyChar | Relative path to project(<ProjectName>.storier)
        public string Name { get { return StoryElementData["Name"]; } set { ResourceName = StoryElementData["Name"] = value; } } // Example: MyChar | Uses ResourceName 
        public string Description { get { return StoryElementData["Description"]; } set { StoryElementData["Description"] = value; } }  // Example: Character Example
        
        // public string Template { get { return StoryElementData["Template"]; } set { StoryElementData["Template"] = value; } }  // Template reference
        // public Dictionary<string, object> TemplateData { get; set; } = new Dictionary<string, object>();

        // Story Element Informations
        public StoryElementTypes StoryElementType { get; set; } = StoryElementTypes.Asset; 
        public string StoryElementExtension { get; set; } = ".asset"; // Example ext: ".ext" 

        protected StoryElement() {}
        // StoryElementPath = StoryBase[.../<StoryName>] + StoryElementId[/Flow/Chapter1] + StoryElementExtension
        public StoryElement(string path) // _path means to Full Path
        {
            if (!path.StartsWith(StoryManager.CurrentStory.StoryBase)) { GD.PushWarning("Story Element isn't in Workspace!\n" + path); return; }
            Id = path.Replace(StoryManager.CurrentStory.StoryBase, "").Replace("." + path.Extension(), "");
            Name = path.GetFile().Replace("." + path.Extension(), "");
            StoryElementExtension = "." + path.Extension();
            TakeOverPath(path);
        }
        public StoryElement(string name, string description, string path)
        {
            if (!path.StartsWith(StoryManager.CurrentStory.StoryBase)) { GD.PushWarning("Story Element isn't in Workspace!\n" + path); return; }
            Id = path.Replace(StoryManager.CurrentStory.StoryBase, "").Replace("." + path.Extension(), "");
            Name = name;
            Description = description;
            StoryElementExtension = "." + path.Extension();
            TakeOverPath(path);
        }

        // Replace/Move Story Element | Not ready for use -> Move to File Manager?
        private Error Move(string destination)
        {
            Directory dir = new Directory();
            Error errCopy = dir.Copy(ResourcePath, destination);
            if (errCopy != Error.Ok) { return errCopy; }
            Error errDelete = dir.Remove(ResourcePath);
            if (errDelete != Error.Ok) { return errDelete; }
            return Error.Ok;
        }

        // Change Story Element Id(Relative Path) 
        public Error ReId(string id)
        {
            string _destination = StoryManager.CurrentStory.StoryBase + Id + StoryElementExtension;
            Id = Id.Replace(Name, id);
            Error err = Move(_destination);
            TakeOverPath(_destination);
            return err; // Error.Ok;
        }   

        // Saving Story Element | ~Maybe create Resource Format Saver/Loader
        public virtual bool Save() { return FileManager.SaveFile(ResourcePath, JSON.Print(StoryElementData)); } // ~Save text
        // Reloading Story Element
        public virtual bool Reload() { return false; }
        // Import Story Element
        public virtual void Import() { }
        // Export Story Element
        public virtual void Export() { } // ~List of Character(csv), Single File(json)

        // Loading Story Element
        public static StoryElement Load(string path) 
        { 
            StoryElement loaded = new StoryElement(path);
            JSONParseResult jsonObject = JSON.Parse(FileManager.LoadFile(path));
            if(jsonObject.Error != 0) { GD.Print("Error:", jsonObject.ErrorLine); return null; } 
            else
            {
                Dictionary elementDictionary = jsonObject.Result as Dictionary;  // Godot Problem: "as Dictionary<string, string>" return null
                loaded.StoryElementData = new Dictionary<string, string>(elementDictionary);
                // GD.Print("Story Element Data:\n" + JSON.Print(loaded.StoryElementData, "\t"));
            }
            return loaded; 
        }
    }
}


/*
Story Element Informations
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