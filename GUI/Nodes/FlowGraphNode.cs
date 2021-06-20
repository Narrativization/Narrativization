using Godot;
using System;
using Godot.Collections;

// Basic Flow Node Controls for GraphNode(Without Storier)
public class FlowGraphNode : GraphNode
{
    // Generic Node Index
    public static int GenericIdIndex { get; set; } = 0; // Node id Index

    [Export]
    public int NodeIndex { get; set; } = GenericIdIndex++; // Node Id(Noid)

    // Graph Node Informations
    public string NodeId { get { return Name; } set { Name = value; } } 
    public string NodeType { get { return Filename; } set { Filename = value; } }
    public string NodeTitle { get { return Title; } set { Title = value; } }
    public Vector2 NodeOffset { get { return Offset; } set { Offset = value; } }

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
                AcceptEvent();
            }
            else if (eventMouseButton.Pressed)
            {
                SelectNode();
            }
            if (IsInstanceValid(StoryManager.CurrentStory) && // if story exist, show context menu 
                inputEvent.IsActionPressed("Context"))
            {
                ContextNode();
                AcceptEvent();
            }
        }
    }

    // Reset GenericIdIndex from GraphEditor
    public static void Reset() { GenericIdIndex = 0; }
    // Opening Node(Ex: Double Clicked)
    public virtual void OpenNode() { }
    // Opening Node(Ex: Left Clicked)
    public virtual void SelectNode() { }
    // Context Node(Ex: Right Clicked)
    public virtual void ContextNode() { }

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