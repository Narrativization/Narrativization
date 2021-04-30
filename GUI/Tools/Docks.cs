using Godot;
using System;

public class Docks : HSplitContainer
{
    
    public int LastOffset = 0;

    public override void _Ready()
    {
        AddToGroup("Docks");
    }

//  public override void _Process(float delta)
//  {
//      
//  }

    public void ToggleTools()
    {
        if (DraggerVisibility == DraggerVisibilityEnum.HiddenCollapsed)
        {
            DraggerVisibility = DraggerVisibilityEnum.Visible;
            SplitOffset = LastOffset;
        }
        else
        {
            DraggerVisibility = DraggerVisibilityEnum.HiddenCollapsed;
            LastOffset = SplitOffset;
            SplitOffset = 0;
        }
    }

}
