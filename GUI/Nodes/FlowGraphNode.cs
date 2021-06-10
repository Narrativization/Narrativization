using Godot;
using System;
using Godot.Collections;

// Basic Flow Node Controls for GraphNode(Without Storier)
public class FlowGraphNode : GraphNode
{
    // Flow Fragment Id
    public static int GenericIdIndex { get; set; } = 0; // Node id Index

    [Export]
    public int NodeIndex { get; set; } = GenericIdIndex++; // Node Id(Noid)

    public StoryFragment NodeFragment 
    { 
        get { return StoryFragment.StoryFragments[NodeFragmentIndex]; } 
        set { NodeFragmentIndex = value.FragmentIndex; } 
    } // Fragment

    // Graph Node Display Informations
    public int NodeFragmentIndex { get; set; } // Fragment Id/Index
    public string NodeId { get { return Name; } set { Name = value; } } 
    public string NodeType { get { return Filename; } set { Filename = value; } }
    public string NodeTitle { get { return Title; } set { Title = value; } }
    public Vector2 NodeOffset { get { return Offset; } set { Offset = value; } }

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

    public FlowGraphNode()
    {
        NodeId = "FlowNode" + NodeIndex;
    }

    public override void _Ready()
    {

    }

//  public override void _Process(float delta)
//  {
//      
//  }

    public override void _GuiInput(InputEvent inputEvent)
    {
        if (inputEvent is InputEventMouseButton eventMouseButton)
        {
            // CTRL + Double Click
            
            if (eventMouseButton.Doubleclick)
            {
                OpenNode();
            }
            if (IsInstanceValid(StoryManager.CurrentStory) && // if story exist, show context menu 
                inputEvent.IsActionPressed("Context"))
            {
                GD.Print("NODED!");
                AcceptEvent();
            }
        }
    }

    // Create Flow Graph Node
    public virtual void CreateFlowNode(string nodeType) { NodeId = "FlowNode" + NodeIndex; NodeFragment = new StoryFragment(); }
    public virtual void CreateFlowNode(Dictionary<string, object> nodeData) { NodeData = nodeData; }

    // Reset GenericIdIndex from GraphEditor
    public static void Reset() { GenericIdIndex = 0; }

    // Opening Node(Ex: Double Clicked)
    public virtual void OpenNode() { }

}


/*

NodeId: Array Index
FragmentId: Referenced Fragments Array Index 

Nodes 
[
    0: 
    {
        FragmentId,
        Name, // Connection Identifier
        Title,
        Offset, 
        // Ports, // Slots 
        Type? -> Hub, Jump etc.
    },
    1: 
    {
        ...
    }
]

Connections
[
    {from, from_port, to, to_port},
    ...
]

*/