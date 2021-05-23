using Godot;
using System;
using Godot.Collections;

public class Editors : TabContainer
{
    // Exports
    [Export]
    private NodePath flowEditorNodePath = new NodePath("./Flow/FlowEditor");
    [Export]
    private NodePath sceneEditorNodePath = new NodePath("./Scene");
    [Export]
    private NodePath scriptEditorNodePath = new NodePath("./Script/ScriptEditor");
    [Export]
    private NodePath propertyEditorNodePath = new NodePath("./Property/PropertyEditor");

    // Properties
    public string CurrentEditor { get { return GetTabTitle(CurrentTab); } }

    public static FlowEditor flowEditor;
    // public static TextEdit sceneEditor;
    public static TextEdit scriptEditor;
    public static PropertyEditor propertyEditor;

    public Godot.Collections.Array<StoryElement> editedStoryElements = new Godot.Collections.Array<StoryElement>();
    public StoryElement currentStoryElement;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        AddToGroup("Editors");
        flowEditor = GetNode<FlowEditor>(flowEditorNodePath);
        // sceneEditor = GetNode<TextEdit>(sceneEditorNodePath);
        scriptEditor = GetNode<TextEdit>(scriptEditorNodePath);
        propertyEditor = GetNode<PropertyEditor>(propertyEditorNodePath);
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

        switch (CurrentEditor)
        {
            case "Flow":
                break;
            case "Scene":
                break;
            case "Script":
                UpdateScript();
                break;
            case "Property":
                UpdateProperty();
                break;
            default:
                break;
        }
    }

    private void UpdateFlow()
    {

    }

    private void UpdateScene()
    {

    }

    private void UpdateScript()
    {
        scriptEditor.Text = "CharacterName: <CharacterName>";
        scriptEditor.AddKeywordColor("CharacterName", new Color(0, 0.5f, 0.5f));
        scriptEditor.AddColorRegion("<", ">", new Color(0, 1, 0));
    }

    private void UpdateProperty()
    {
        propertyEditor.CreateProperties();
    }
}
