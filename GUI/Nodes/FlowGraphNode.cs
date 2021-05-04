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

    public string NodeType = ""; // Decide Displayed Node Also view(Scene) < (Res://)
    [Export]
    public int FragmentId { get; set; } = 0; // Fragment Id

    // Custom Node Data
    public Dictionary<string, object> NodeData 
    { 
        get
        {
            return new Dictionary<string, object>()
            {
                {"FragmentId", FragmentId},
                {"Name", Name},         // Node Identifier
                {"Type", NodeType},     // Identify to Displayed Node
                {"Title", Title},       // Top Text of Node
                {"Offset", Offset},      // Location of Node
                // {"Fragment", FragmentData}
            };
        } 
    } 
    
    // Custom Fragment Data
    public Dictionary<string, object> FragmentData
    { 
        get
        {
            return new Dictionary<string, object>()
            {
                // {"Reference", ""},         // Referencing any file?
            };
        } 
    } 
    public override void _Ready()
    {
        Name = "FlowNode" + NodeId;
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
    
    // // Opening Node(Ex: Double Clicked)
    // public virtual void OpenProperties() { }
    // // Opening Node(Ex: CTRL + Double Clicked)
    // public virtual void OpenReference() { }


    

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