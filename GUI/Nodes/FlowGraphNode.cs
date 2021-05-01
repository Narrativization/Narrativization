using Godot;
using System;

// Basic Flow Node Controls for GraphNode(Without Storier)
public class FlowGraphNode : GraphNode
{
    // Flow Fragment Id
    public static int IdIndex { get; set; } = 0; // Uid, Nid -> Unique or Node id
    [Export]
    public string FragmentId { get; set; } = "FlowFragment" + IdIndex++; // Node Id

    public override void _Ready()
    {
        Name = FragmentId;
    }

//  public override void _Process(float delta)
//  {
//      
//  }

    public override void _GuiInput(InputEvent inputEvent)
    {
        if (inputEvent is InputEventMouseButton eventMouseButton)
        {
            if (eventMouseButton.Doubleclick)
            {
                OpenNode();
            }
        }
    }

    // Reset IdIndex / GraphEditor
    public static void Reset() { IdIndex = 0; }

    // Opening Node(Ex: Double Clicked)
    public virtual void OpenNode() { }

    

}
