using Godot;
using System;
using Godot.Collections;
using Storier;

public class Editors : TabContainer
{
    // Exports
    [Export]
    private NodePath textEditorNodePath = new NodePath("./Text/TextEditor");

    // Properties
    public string CurrentEditor { get { return GetTabTitle(CurrentTab); } }

    private TextEdit textEditor;

    public Godot.Collections.Array<StoryElement> editedStoryElements = new Godot.Collections.Array<StoryElement>();
    public StoryElement currentStoryElement;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        AddToGroup("Editors");
        textEditor = GetNode<TextEdit>(textEditorNodePath);
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

    public string ChangeEditor()
    {
        CurrentTab = (CurrentTab + 1) % GetTabCount();
        // textEditor.Text = JSON.Print(StoryManager.CurrentStory.CurrentStoryElement.StoryElementData, "\t");
        textEditor.Text = "CharacterName: <CharacterName>";
        textEditor.AddKeywordColor("CharacterName", new Color(0, 0.5f, 0.5f));
        textEditor.AddColorRegion("<", ">", new Color(0, 1, 0));
        return CurrentEditor;
    }
}
