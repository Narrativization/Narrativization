using Godot;
using System;
using Godot.Collections;

public class Editors : TabContainer
{
    // Exports
    [Export]
    private NodePath flowEditorNodePath = new NodePath("./Flow/FlowEditor");
    // [Export]
    // private NodePath propertyEditorNodePath = new NodePath("./Property");
    [Export]
    private NodePath textEditorNodePath = new NodePath("./Text/TextEditor");

    // Properties
    public string CurrentEditor { get { return GetTabTitle(CurrentTab); } }

    public static FlowEditor flowEditor;
    public static TextEdit textEditor;

    public Godot.Collections.Array<StoryElement> editedStoryElements = new Godot.Collections.Array<StoryElement>();
    public StoryElement currentStoryElement;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        AddToGroup("Editors");
        flowEditor = GetNode<FlowEditor>(flowEditorNodePath);
        textEditor = GetNode<TextEdit>(textEditorNodePath);
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }

    public void EditStoryElement(StoryElement storyElement)
    {
        editedStoryElements.Add(storyElement);
        if (storyElement is StoryFlow)
        {
            flowEditor.Reset();
            // flowEditor.LoadData(storyElement.StoryFlowData);
        }
    }

    public string NextEditor()
    {
        CurrentTab = (CurrentTab + 1) % GetTabCount();
        ChangeEditor();
        return CurrentEditor;
    }
    
    public string PreviousEditor()
    {
        CurrentTab = (CurrentTab - 1 + GetTabCount()) % GetTabCount();
        ChangeEditor();
        return CurrentEditor;
    }

    private void ChangeEditor()
    {
        // textEditor.Text = JSON.Print(StoryManager.CurrentStory.CurrentStoryElement.StoryElementData, "\t");
        textEditor.Text = "CharacterName: <CharacterName>";
        textEditor.AddKeywordColor("CharacterName", new Color(0, 0.5f, 0.5f));
        textEditor.AddColorRegion("<", ">", new Color(0, 1, 0));
    }
}
