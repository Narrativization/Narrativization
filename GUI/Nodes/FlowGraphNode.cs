using Godot;
using System;

// Basic Flow Node Controls for GraphNode(Without Storier)
public class FlowGraphNode : GraphNode
{
    // Flow Fragment Id
    public static int UidIndex = 0;
    public string Uid = "FlowFragment" + UidIndex;

    public override void _Ready()
    {
        UidIndex++;
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

    // Opening Node(Ex: Double Clicked)
    public virtual void OpenNode() { }

}
