using Godot;
using System;

public class OutlineMenu : HBoxContainer
{
    // Exports
    [Export]
    private NodePath refreshNodePath = new NodePath("./Refresh");
    
    public Button refreshButton;

    public override void _Ready()
    {
        StoryManager.CurrentStoryManager.Connect("StoryChanged", this, "_OnStoryChanged");
        StoryManager.CurrentStoryManager.Connect("StoryElementCreated", this, "_OnStoryElementCreated");

        refreshButton = GetNode<Button>(refreshNodePath);
        refreshButton.Connect("pressed", this, "_OnRefreshPressed");
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

    public void _OnStoryChanged(Story story)
    {
        GetTree().CallGroup("Navigator", "Refresh");
    }

    public void _OnStoryElementCreated(StoryElementOld storyElement)
    {
        GetTree().CallGroup("Navigator", "Refresh");
    }

    public void _OnRefreshPressed()
    {
        GetTree().CallGroup("Navigator", "Refresh");
    }
}
