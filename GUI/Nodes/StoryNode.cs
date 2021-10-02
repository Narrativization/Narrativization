using Godot;
using System;
using Godot.Collections;

// Story Flow Node
public class StoryNode : FlowGraphNode
{
    // Node Data
    [Export]
    private StoryNodeType _NodeType { get; set; }   // Story Node Type | Editable from Editor 
    public StoryNodeType NodeType { get { NodeData["Type"] = _NodeType; return _NodeType; } set { NodeData["Type"] = _NodeType = value; } } // Match _NodeType to NodeData(Dictionary)
    // public StoryNodeType NodeType { get { return (StoryNodeType)NodeData["Type"]; } set { NodeData["Type"] = value; } } // Directly use NodeData

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

        switch (NodeType)
        {
            case StoryNodeType.Story:
                string recentStory = StorierManager.ReadRecentStory();
                NodeTitle = recentStory.Replace(StorierManager.workspace, "");;
                break;
            case StoryNodeType.StoryElement:
                break;
            case StoryNodeType.StoryFragment:
                break;
            default:
                break;
        }
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

        switch (NodeType)
        {
            case StoryNodeType.Story:
                OpenStory();
                break;
            case StoryNodeType.StoryElement:
                break;
            case StoryNodeType.StoryFragment:
                break;
            default:
                break;
        }
        GD.Print("OpenNodeDone");
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
         // IsInstanceValid(StoryManager.CurrentStory) && // if story exist, show context menu 
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
                    Editors.nodeEditor.DeleteAll();
                } 
                else
                {
                   StorierManager.DeleteRecentStory();
                }
            }
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

    public override void OpenNode()
    {
        // base.OpenNode();
        if (IsInstanceValid(storyFlow))
        {
            StoryManager.CurrentStoryElement = storyFlow;
            GetTree().CallGroup("Editors", "EditStoryElement", StoryManager.CurrentStoryElement);
        }
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