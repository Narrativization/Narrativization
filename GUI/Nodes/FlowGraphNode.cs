using Godot;
using System;
using Godot.Collections;

// Basic Flow Node Controls for GraphNode(Without Storier) | Compatible with FlowGraphEditor
public class FlowGraphNode : GraphNode
{
    // Flow Graph Node Data
    public Dictionary<string, object> NodeData { get; set; } = new Dictionary<string, object>(); 

    // Graph Node Informations
    public string NodeId { get { return Name; } set { Name = value; } } // Unique Id of Node (Query: ?Node="<NodeId>"), Connection Identifier
    public string NodeName { get { return (string)NodeData["Name"]; } set { NodeData["Name"] = value; } } // Node Display Name 
    public string NodeDescription { get { return (string)NodeData["Description"]; } set { NodeData["Description"] = value; } } // Node Tooltip(Description)
    public string NodePreset { get { Filename = (string)NodeData["Preset"]; return Filename; } set { NodeData["Preset"] = Filename = value; } } // Presetted Scene of Flow Node | Also use NodeType if you can create slots
    public string NodeTitle { get { Title = (string)NodeData["Title"]; return Title; } set { NodeData["Title"]= Title = value; } }  // Top(Title) text of Node
    public Vector2 NodeOffset { get { Offset = (Vector2)NodeData["Offset"]; return Offset; } set { NodeData["Offset"] = Offset = value; } }      // Node Position(Location)

    public FlowGraphNode()
    {
        NodeId = "FlowNode";
        NodeName = "";
        NodeDescription = "";
        // Guid guid = Guid.NewGuid(); // C# Guid (Not used: Unique but too Long/Incomprehensible) 
        // NodeId = guid.ToString();
    }

    public FlowGraphNode(string id, Dictionary<string, object> nodeData)
    {
        NodeId = id;
        NodeData = nodeData;
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
            // Add CTRL + Left Click
            if (eventMouseButton.Doubleclick)
            {
                OpenNode();
                AcceptEvent();
            }
            else if (eventMouseButton.Pressed)
            {
                SelectNode();
            }
            if (inputEvent.IsActionPressed("Context")) // Project Input
            {
                ContextNode();
                AcceptEvent();
            }
        }
    }

    // Reset GenericIdIndex from GraphEditor
    public static void Reset() { }
    // Opening Node(Ex: Double Clicked)
    public virtual void OpenNode() { }
    // Opening Node(Ex: Left Clicked)
    public virtual void SelectNode() { }
    // Context Node(Ex: Right Clicked)
    public virtual void ContextNode() { }

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
        Offset: NodeOffset

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