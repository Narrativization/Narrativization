using Godot;
using System;
using Godot.Collections;
using Storier;

public class Editors : TabContainer
{
    [Export]
    public StoryElementType editorType = StoryElementType.Flow; 

    public Godot.Collections.Array editedStoryElements = new Godot.Collections.Array();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }

    public void EditStoryElement(StoryElement _storyElement)
    {
        editedStoryElements.Add(_storyElement);
    }
}
