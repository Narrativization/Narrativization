using Godot;
using System;

// Basic Flow Controls for GraphEdit(Without Storier)
public class GraphNodeEditor : GraphEdit
{
    [Export]
    public float zoomSpeed = 1.0f;

    protected bool hovering = false; // ~Focusing
    protected bool dragging = false;

    public override void _Ready()
    {
        
    }

    public override void _Input(InputEvent inputEvent)
    {
        // Generic Node Editor Inputs
        if (inputEvent.IsAction("ZoomIn") && hovering)
        {
            Zoom += zoomSpeed * GetProcessDeltaTime();
            AcceptEvent();
        } 
        else if (inputEvent.IsAction("ZoomOut") && hovering)
        {
            Zoom -= zoomSpeed * GetProcessDeltaTime();
            AcceptEvent();
        } 
        else if (inputEvent.IsActionPressed("ZoomHold") && hovering)
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
    }

    public override void _Notification(int what)
    {
        switch (what)
        {
            case NotificationMouseEnter:
                hovering = true;
                break;
            case NotificationMouseExit:
                hovering = false;
                break;
            default:
                break;
        }

        base._Notification(what);
    }

}
