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
        StoryElementType = StoryElementType.Entity;
    }
    public StoryEntity(string name, string description, string path) : base(name, description, path)
    {
        StoryElementType = StoryElementType.Entity;
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
