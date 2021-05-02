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
        StoryManager.CurrentStoryManager.Connect("TitleIdChanged", this, "_OnTitleIdChanged");

        refreshButton = GetNode<Button>(refreshNodePath);
        refreshButton.Connect("pressed", this, "_OnRefreshPressed");
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

    public void _OnTitleIdChanged(string titleId)
    {
        GetTree().CallGroup("Navigator", "Refresh");
    }

    public void _OnRefreshPressed()
    {
        GetTree().CallGroup("Navigator", "Refresh");
    }
}
