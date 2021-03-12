using Godot;
using System;
using Godot.Collections;
// using GCollection = Godot.Collections; // ~Maybe?

namespace Storier
{
    // Entity Defination
    public class StoryEntity : StoryElement
    {
        // All Opened Entities
        private static Godot.Collections.Array<StoryEntity> storyEntities = new Godot.Collections.Array<StoryEntity>();

        protected StoryEntity() {} 
        public StoryEntity(string _id, string _name, string _description) 
        {
            storyElementType = StoryElementType.Entity;
            base.InitStoryElement(_id, _name, _description, ".entity");
            storyEntities.Add(this);
        }

        public override bool Save(string _data)
        {
            // if (!FileManager.SaveFile(ResourcePath + ResourceName + ".entity", _data))
            // {
            //     GD.Print(ResourceName + " save Failed!");
            //     return; 
            // }
            // base.Save(_data);
            return false;
        }

        public override bool Reload()
        {
            // if (FileManager.LoadFile(ResourcePath + ResourceName + ".entity") == "")
            // {
            //     GD.Print(ResourcePath + ResourceName + ".entity" + " load failed!");
            //     return; 
            // }
            // base.Reload();
            return false;
        }

        public Godot.Collections.Dictionary<string, object> SaveData()
        {
            return new Godot.Collections.Dictionary<string, object>()
            {
                { "Id", Id },
                { "Name", Name },
                { "Description", Description }, 
                { "Template", Template },
                { "Data", TemplateData }
            };
        }
        


        public bool LoadData()
        {
            
            return true;
        }

        public static StoryEntity Load(string _id)
        {
            string fileName = StoryManager.currentStory.storyData.directory + _id + ".entity";
            // File Checks
            File loadFile = new File(); 
            if (!loadFile.FileExists(fileName)) { GD.Print("File isn't exist!"); return null; }
            Error err = loadFile.Open(fileName, File.ModeFlags.Read);
            if (err != Error.Ok) { GD.Print("Error: ", err); return null; }
            JSONParseResult jsonObject = JSON.Parse(loadFile.GetAsText());
            if(jsonObject.Error != Error.Ok) { GD.Print("Error:", jsonObject.ErrorLine); return null; } 
            // Informations : Get Id, Name, Description
            Dictionary<string, object> entityData = jsonObject.Result as Dictionary<string, object>;
            string loadedId = entityData["Id"] as String;
            string loadedName = entityData["Name"] as String;
            string loadedDescription = entityData["Description"] as String;
            string loadedTemplate = entityData["Template"] as String;
            Dictionary<string, object> loadedData = entityData["Data"] as Dictionary<string, object>; // Custom Informations
            loadFile.Close();
            return new StoryEntity(loadedId, loadedName, loadedDescription);
        }

        public static StoryEntity GetStoryEntity(string _id)
        {
            for (int i = 0; i < storyEntities.Count; i++)
            {
                if (IsInstanceValid(storyEntities[i]) && storyEntities[i].Id == _id)
                {
                    return storyEntities[i];
                }
            }
            return Load(_id);
        }
    }
}


/*
Entity
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



// // When new story opened Reset all entities
// public static void Reset() { storyEntities = new Godot.Collections.Array<StoryEntity>(); }