using Godot;
using System;
using Godot.Collections;

// Basic Flow Node Controls for GraphNode(Without Storier)
public class FlowGraphNode : GraphNode
{
    // Flow Fragment Id
    public static int GenericIdIndex { get; set; } = 0; // Uid, Nid -> Unique or Node id

    [Export]
    public int NodeId { get; set; } = GenericIdIndex++; // Node Id


    public StoryFragment NodeFragment 
    { 
        get { return StoryFragment.StoryFragments[NodeFragmentId]; } 
        set { NodeFragmentId = value.FragmentId; } 
    } // Fragment

    // Graph Node Display Informations
    public int NodeFragmentId { get; set; } // Fragment Id/Index
    public string NodeName { get { return Name; } set { Name = value; } } 
    public string NodeType { get { return Filename; } set { Filename = value; } } // ~Set Value
    public string NodeTitle { get { return Title; } set { Title = value; } }
    public Vector2 NodeOffset { get { return Offset; } set { Offset = value; } }


    // Custom Node Data
    public Dictionary<string, object> NodeData 
    { 
        get
        {
            return new Dictionary<string, object>()
            {
                { "FragmentId", NodeFragmentId }, // Fragment Identifier
                { "Name", NodeName },         // Node Identifier
                { "Type", NodeType },     // Identify to Displayed Node
                { "Title", NodeTitle },       // Top Text of Node
                { "Offset", NodeOffset },      // Location of Node
            };
        } 
        set
        {
            NodeFragmentId = (int)value["FragmentId"];
            NodeName = value["Name"] as string;
            NodeType = value["Type"] as string;
            NodeTitle = value["Title"] as string;
            NodeOffset = (Vector2)value["Offset"];
        }
    } 

    public override void _Ready()
    {
        NodeName = "FlowNode" + NodeId;
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
        }
    }

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
        // FragmentId,
        Offset,
        Slots, 
        Rights,
        ~Type? -> Hub, Jump etc.
    },
    1: 
    {
        ...
    }
]



*/