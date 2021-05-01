using Godot;
using System;
using Storier;

public class OutlineMenu : HBoxContainer
{
    // Exports
    [Export]
    private NodePath refreshNodePath = new NodePath("./Refresh");
    
    public Button refreshButton;

    public override void _Ready()
    {
        StoryManager.Self.Connect("StoryOpened", this, "_OnStoryOpened");
        StoryManager.Self.Connect("StoryElementOpened", this, "_OnStoryElementOpened");

        refreshButton = GetNode<Button>(refreshNodePath);
        refreshButton.Connect("pressed", this, "_OnRefreshPressed");
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

    public void _OnStoryOpened(Story story)
    {
        if (!IsInstanceValid(story)) { return; }
        GetTree().CallGroup("Navigator", "Refresh");
    }

    public void _OnStoryElementOpened(StoryElement storyElement)
    {
        if (!IsInstanceValid(storyElement)) { return; }
        GetTree().CallGroup("Navigator", "Refresh");
    }

    public void _OnRefreshPressed()
    {
        GetTree().CallGroup("Navigator", "Refresh");
    }
}
