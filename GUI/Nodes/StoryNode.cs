using Godot;
using System;
using Godot.Collections;

// Story Flow Node
public class StoryNode : FlowGraphNode
{
    // Node Fragment
    public StoryFragment _NodeFragment = new StoryFragment(); 
    public StoryFragment NodeFragment 
    { 
        get { return _NodeFragment; }
        set { _NodeFragment = value; }
    }

    public StoryNode() : base() 
    {
        NodeId = "StoryNode";
    }

    public StoryNode(StoryFragment storyFragment)
    {
        NodeId = "StoryNode";
        NodeFragment = storyFragment;
    }
    
    public override void _Ready()
    {
        base._Ready();
    }

    //  public override void _Process(float delta)
    //  {
    //      
    //  }

    public override void _Draw()
    {
        base._Draw();
    }

    public override void OpenNode()
    {
        GD.Print("Open Node");
        base.OpenNode();

        // Embed to Fragment
        StoryFragmentType nodeType = NodeFragment.FragmentType;

        switch (nodeType)
        {
            case StoryFragmentType.Story:
                OpenStory();
                break;
            case StoryFragmentType.StoryElement:
                OpenStoryElement();
                break;
            case StoryFragmentType.StoryFragment:
                break;
            default:
                break;
        }
    }

    public override void SelectNode()
    {
        GD.Print("Select Node");
        base.SelectNode();
    }

    public override void ContextNode()
    {
        GD.Print("Context Node");
        base.ContextNode();
    }

    public void OpenStory()
    {
        string recentStory = StorierManager.ReadRecentStory();
        Directory dir = new Directory(); 
        if (dir.FileExists(recentStory))
        {
            GD.Print("Recent Story: " + recentStory);
            if (recentStory != "")
            {
                GD.Print("Load Story");
                if (StoryManager.OpenStory(recentStory) != null)
                {
                    //GetTree().CallGroup("Editors", "<Func>", <Param>);
                    Editors.nodeEditor.DeleteAll();
                } 
                else
                {
                   StorierManager.DeleteRecentStory();
                }
            }
        }
    }

    public void OpenStoryElement()
    {
        object nodePath = "";
        if (NodeData.TryGetValue("", out nodePath))
        {
            
        }
    }

    /*
    public StoryElement ReferenceStoryFlow(StoryElement _storyFlow)
    {
        storyFlow = _storyFlow;
        if (IsInstanceValid(storyFlow))
        {
            Title = (storyFlow.StoryElementName != "") ? storyFlow.StoryElementName : "Story Flow";
            HintTooltip = storyFlow.StoryElementId;
        }
        return storyFlow;
    }
    */

    // public Vector2 Slots { get; set; } = new Vector2(-1, -1); // Left & Right Slot ~

    
    /*
    // Not used!
    public enum FlowState
    {
        Empty,      // 
        Begin,      // Start
        Progress,   // Process
        End         // Finish
    }
    */

}


/*

Nodes:
{
    <NodeId>: 
    {
        Name: NodeName,
        Description: NodeDescription,
        Preset: NodePreset,
        Title: NodeTitle,
        Offset: NodeOffset,
        Fragment: NodeFragment

        // ~Ports/Slots
        // ~Type -> Hub, Jump etc.
    },
    ...
}

Connections: 
[
    {from, from_port, to, to_port},
    ...
]

*/