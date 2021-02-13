using Godot;
using System;

public class NodeEditor : GraphEdit
{
    [Export]
    public float zoomSpeed = 1.0f;

    private bool dragging = false;

    public override void _Ready()
    {
        
    }

    public override void _Input(InputEvent inputEvent)
    {
        // Generic Node Editor Inputs
        if (inputEvent.IsAction("ZoomIn"))
        {
            Zoom += zoomSpeed * GetProcessDeltaTime();
            AcceptEvent();
        } 
        else if (inputEvent.IsAction("ZoomOut"))
        {
            Zoom -= zoomSpeed * GetProcessDeltaTime();
            AcceptEvent();
        } 
        else if (inputEvent.IsActionPressed("ZoomHold"))
        {
            dragging = true;
            AcceptEvent();
        }
        else if (inputEvent.IsActionReleased("ZoomHold"))
        {
            dragging = false;
            AcceptEvent();
        }

        if (inputEvent is InputEventMouseMotion)
        {
            InputEventMouseMotion motion = inputEvent as InputEventMouseMotion;
            if (dragging)
            {
                ScrollOffset += -motion.Relative;
                AcceptEvent();
            }
        }

        // Flow Editor Inputs
        if (inputEvent.IsActionReleased("Context"))
        {
            InputEventMouse mouse = inputEvent as InputEventMouse;
            PopupMenu popupMenu = GetParent().GetNode<PopupMenu>("./FlowContext");
            popupMenu.SetPosition(mouse.GlobalPosition);
            popupMenu.ShowModal();
        }
    }

}
