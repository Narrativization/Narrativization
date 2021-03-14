using Godot;
using System;

// Basic Flow Node Controls for GraphNode(Without Storier)
public class FlowGraphNode : GraphNode
{
    

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
            if (eventMouseButton.Doubleclick)
            {
                OpenNode();
            }
        }
    }

    // Opening Node(Ex: Double Clicked)
    public virtual void OpenNode() { }

}
