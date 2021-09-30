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
        StoryManager.CurrentStoryManager.Connect("OnStoryEdited", this, "_OnStoryEdited");
        StoryManager.CurrentStoryManager.Connect("OnStoryElementCreated", this, "_OnStoryElementCreated");

        refreshButton = GetNode<Button>(refreshNodePath);
        refreshButton.Connect("pressed", this, "_OnRefreshPressed");
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

    public void _OnStoryEdited(Story story)
    {
        GetTree().CallGroup("Navigator", "Refresh");
    }

    public void _OnStoryElementCreated(StoryElement storyElement)
    {
        GetTree().CallGroup("Navigator", "Refresh");
    }

    public void _OnRefreshPressed()
    {
        GetTree().CallGroup("Navigator", "Refresh");
    }
}
