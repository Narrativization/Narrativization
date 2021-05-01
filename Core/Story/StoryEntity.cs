using Godot;
using System;
using Godot.Collections;
// using GCollection = Godot.Collections; // ~Maybe?


// Entity Defination
public class StoryEntity : StoryElement
{
    protected StoryEntity() {} 
    // StoryElementPath = StoryBase[.../<StoryName>] + StoryElementId[/Flow/Chapter1] + StoryElementExtension
    public StoryEntity(string path) : base(path)
    {
        StoryElementType = StoryElementTypes.Entity;
        StoryElementExtension = ".entity";
    }
    public StoryEntity(string name, string description, string path) : base(name, description, path)
    {
        StoryElementType = StoryElementTypes.Entity;
        StoryElementExtension = ".entity";
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