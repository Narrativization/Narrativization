using Godot;
using System;
using Godot.Collections;

public enum FlowState
{
    Begin,      // Start
    Progress,   // Process
    End         // Finish
}

// Generic Flow Node
public class StoryNode : FlowGraphNode
{
    [Export]
    NodePath contentTextLabelPath = "./Content";

    public RichTextLabel contentTextLabel;

    public StoryFragment NodeFragment 
    { 
        get { return StoryFragment.StoryFragments[NodeFragmentIndex]; } 
        set { NodeFragmentIndex = value.FragmentIndex; } 
    } // Fragment

    public int NodeFragmentIndex { get; set; } // Fragment Id/Index

    // Custom Node Data
    public Dictionary<string, object> NodeData 
    { 
        get
        {
            return new Dictionary<string, object>()
            {
                { "Fragment", NodeFragmentIndex },  // Fragment Identifier
                { "Id", NodeId },                   // Node Identifier
                { "Type", NodeType },               // Identify to Displayed Node
                { "Title", NodeTitle },             // Top Text of Node | Editable
                { "Offset", NodeOffset },           // Location of Node
            };
        } 
        set
        {
            NodeFragmentIndex = (int)value["Fragment"];
            NodeId = value["Id"] as string;
            NodeType = value["Type"] as string;
            NodeTitle = value["Title"] as string;
            NodeOffset = (Vector2)value["Offset"];
        }
    } 


        
    public StoryNode() : base() { }

    public override void _Ready()
    {
        contentTextLabel = GetNode<RichTextLabel>(contentTextLabelPath);
        base._Ready();
    }

    public override void _Draw()
    {
        contentTextLabel.Text = NodeFragment.FragmentName;
        base._Draw();
    }

    //  public override void _Process(float delta)
    //  {
    //      
    //  }

    public override void _GuiInput(InputEvent inputEvent)
    {
        base._GuiInput(inputEvent);
    }

    // Create Flow Node
    public void CreateFlowNode(string nodeType) { NodeId = "FlowNode" + NodeIndex; NodeFragment = new StoryFragment(); }
    public void CreateFlowNode(Dictionary<string, object> nodeData) { NodeData = nodeData; }

    public override void OpenNode()
    {
        GD.Print("Open Node");
        base.OpenNode();
    }

    public override void SelectNode()
    {
        // GD.Print("Select Node");
        base.SelectNode();
    }

    public override void ContextNode()
    {
        GD.Print("Context Node");
        base.ContextNode();
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

}

/*
Flow Node
{
    Fragment
    Id
    Type
    Title
    Offset
}
~Slots(Connections)
*/