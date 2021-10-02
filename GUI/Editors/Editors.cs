using Godot;
using System;
using Godot.Collections;

public class Editors : TabContainer
{
    // Exports
    [Export]
    private NodePath nodeEditorNodePath = new NodePath("./Node/NodeEditor");
    [Export]
    private NodePath sceneEditorNodePath = new NodePath("./Scene/SceneEditor");
    [Export]
    private NodePath scriptEditorNodePath = new NodePath("./Script/ScriptEditor");
    [Export]
    private NodePath propertyEditorNodePath = new NodePath("./Property/PropertyEditor");

    // Properties
    public string CurrentEditor { get { return GetTabTitle(CurrentTab); } }

    public static NodeEditor nodeEditor;
    public static SceneEditor sceneEditor;
    public static TextEdit scriptEditor;
    public static PropertyEditor propertyEditor;

    public Godot.Collections.Array<StoryElement> editedStoryElements = new Godot.Collections.Array<StoryElement>();
    public StoryElement currentStoryElement;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        AddToGroup("Editors");
        nodeEditor = GetNode<NodeEditor>(nodeEditorNodePath);
        sceneEditor = GetNode<SceneEditor>(sceneEditorNodePath);
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
        // if (storyElement is StoryFlow)
        // {
        //     flowEditor.Reset();
        //     // flowEditor.LoadData(storyElement.StoryFlowData);
        // }
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
            case "Node":
                InitNode();
                break;
            case "Scene":
                InitScene();
                break;
            case "Script":
                InitScript();
                break;
            case "Property":
                InitProperty();
                break;
            default:
                break;
        }
    }

    private void InitNode()
    {
        
    }

    private void InitScene()
    {
        if (nodeEditor.GetSelectedNodes().Count > 0)
        {
            sceneEditor.StartFromNode(nodeEditor.GetSelectedNodes()[0]);
        }
    }

    private void InitScript()
    {
        scriptEditor.Text = "CharacterName: <CharacterName>";
        scriptEditor.AddKeywordColor("CharacterName", new Color(0, 0.5f, 0.5f));
        scriptEditor.AddColorRegion("<", ">", new Color(0, 1, 0));
    }

    private void InitProperty()
    {
        propertyEditor.CreateProperties();
    }
}
