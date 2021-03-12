using Godot;
using System;
using Godot.Collections;

// Basic Flow Controls for GraphEdit(Without Storier)
public class GraphNodeEditor : GraphEdit
{
    [Export]
    public float zoomSpeed = 1.0f;

    protected bool hovering = false; // ~Focusing
    protected bool dragging = false;

    public override void _Ready()
    {
        Connect("connection_request", this, "_OnGraphEditorConnectionRequest");
        Connect("disconnection_request", this, "_OnGraphEditorDisconnectionRequest");
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

    public override void _GuiInput(InputEvent inputEvent)
    {
        if (inputEvent.IsActionPressed("Home"))
        {
            GD.Print("Home");
            Godot.Collections.Array selected = GetSelected();
            GraphNode graphNode = selected[0] as GraphNode;
            if (IsInstanceValid(graphNode))
                FocusNode(graphNode);
        }
        else if (inputEvent.IsActionPressed("MoveLeft"))
        {
            GD.Print("MoveLeft");
            Godot.Collections.Array selected = GetSelected();
            GraphNode graphNode = selected[0] as GraphNode;
            if (IsInstanceValid(graphNode))
            {
                Godot.Collections.Array<GraphNode> lefts = GetLeftNodes(graphNode);
                if (lefts.Count > 0)
                {
                    FocusNode(lefts[0]);
                    UnselectAll();
                    lefts[0].Selected = true;
                }
            }
        }
        else if (inputEvent.IsActionPressed("MoveRight"))
        {
            GD.Print("MoveRight");
            Godot.Collections.Array selected = GetSelected();
            GraphNode graphNode = selected[0] as GraphNode;
            if (IsInstanceValid(graphNode))
            {
               Godot.Collections.Array<GraphNode> rights = GetRightNodes(graphNode);
                if (rights.Count > 0)
                {
                    FocusNode(rights[0]);
                    UnselectAll();
                    rights[0].Selected = true;
                } 
            }
        }
        else if (inputEvent.IsActionPressed("MoveUp"))
        {
            GD.Print("MoveUp");
            Godot.Collections.Array selected = GetSelected();
            GraphNode graphNode = selected[0] as GraphNode;
            if (IsInstanceValid(graphNode))
            {
                GraphNode upNode = selected[0] as GraphNode;
                Godot.Collections.Array<GraphNode> lefts = GetLeftNodes(graphNode);
                if (lefts.Count > 0)
                {
                    Godot.Collections.Array<GraphNode> rights = GetRightNodes(lefts[0]);
                    for (int i = 0; i < rights.Count; i++)
                    {
                        if (graphNode.Offset.y > rights[i].Offset.y &&
                            ((upNode.Offset.y <= rights[i].Offset.y) || upNode == graphNode))
                        {
                            upNode = rights[i];
                        }
                    }
                }
                FocusNode(upNode);
                UnselectAll();
                upNode.Selected = true;
            }
        }
        else if (inputEvent.IsActionPressed("MoveDown"))
        {
            GD.Print("MoveDown");
            Godot.Collections.Array selected = GetSelected();
            GraphNode graphNode = selected[0] as GraphNode;
            if (IsInstanceValid(graphNode))
            {
                GraphNode downNode = selected[0] as GraphNode;
                Godot.Collections.Array<GraphNode> lefts = GetLeftNodes(graphNode);
                if (lefts.Count > 0)
                {
                    Godot.Collections.Array<GraphNode> rights = GetRightNodes(lefts[0]);
                    for (int i = 0; i < rights.Count; i++)
                    {
                        if (graphNode.Offset.y < rights[i].Offset.y &&
                            ((downNode.Offset.y > rights[i].Offset.y) || downNode == graphNode))
                        {
                            downNode = rights[i];
                        }
                    }
                }
                FocusNode(downNode);
                UnselectAll();
                downNode.Selected = true;
            }
        }
        base._GuiInput(inputEvent);
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

    // Centered to given GraphNode
    public void FocusNode(GraphNode _node)
    {
        if (!IsInstanceValid(_node)) { GD.Print("Focus Node isn't valid!"); }
        ScrollOffset = _node.Offset - (RectSize/2) + (_node.RectSize/2);
    }

    // Get Left/Previous Nodes
    public Godot.Collections.Array<GraphNode> GetLeftNodes(GraphNode graphNode)
    {
        Godot.Collections.Array<GraphNode> lefts = new Array<GraphNode>();
        Godot.Collections.Array connectionList = GetConnectionList();
        for (int i = 0; i < connectionList.Count; i++)
        {
            if (connectionList[i] is Dictionary connection)
            {
                if ((connection["from"] != null && connection["to"] != null)
                    && graphNode.Equals(GetNode<GraphNode>("./" + connection["to"])))
                {
                    lefts.Add(GetNode<GraphNode>("./" + connection["from"]));
                }
            }
        }
        return lefts;
    }

    // Get Right/Next Nodes
    public Godot.Collections.Array<GraphNode> GetRightNodes(GraphNode graphNode)
    {
        Godot.Collections.Array<GraphNode> rights = new Array<GraphNode>();
        Godot.Collections.Array connectionList = GetConnectionList();
        for (int i = 0; i < connectionList.Count; i++)
        {
            if (connectionList[i] is Dictionary connection)
            {
                if ((connection["from"] != null && connection["to"] != null)
                    && graphNode.Equals(GetNode<GraphNode>("./" + connection["from"])))
                {
                    rights.Add(GetNode<GraphNode>("./" + connection["to"]));
                }
            }
        }
        return rights;
    }

    // Get selected nodes | Return selected GraphNodes
    public Godot.Collections.Array GetSelected()
    {
        Godot.Collections.Array selectedNodes = new Godot.Collections.Array();
        Godot.Collections.Array graphNodes = GetChildren();
        for (int i = 0; i < graphNodes.Count; i++)
        {
            if (graphNodes[i] is GraphNode graphNode)
            {
                if (graphNode.Selected)
                {
                    selectedNodes.Add(graphNode);
                }
            }
        }
        return selectedNodes;
    }

    // Select All
    public void SelectAll()
    {
        Godot.Collections.Array graphNodes = GetChildren();
        for (int i = 0; i < graphNodes.Count; i++)
        {
            if (graphNodes[i] is GraphNode graphNode)
            {
                graphNode.Selected = true;
            }
        }
    }

    // Unselect All
    public void UnselectAll()
    {
        Godot.Collections.Array graphNodes = GetChildren();
        for (int i = 0; i < graphNodes.Count; i++)
        {
            if (graphNodes[i] is GraphNode graphNode)
            {
                graphNode.Selected = false;
            }
        }
    }

    // Delete/Removes Selected Nodes | Delete selected GraphNodes
    public void DeleteSelected()
    {
        Godot.Collections.Array selectedNodes = GetSelected();
        for (int i = 0; i < selectedNodes.Count; i++)
        {
            if (selectedNodes[i] is GraphNode selectedNode)
            {
                selectedNode.QueueFree();
            }
        }
    }

    // Delete/Removes All Nodes | Delete All GraphNodes
    public void DeleteAll()
    {
        Godot.Collections.Array graphNodes = GetChildren();
        for (int i = 0; i < graphNodes.Count; i++)
        {
            if (graphNodes[i] is GraphNode graphNode)
            {
                if (graphNode.Selected)
                {
                    graphNode.QueueFree();
                }
            }
        }
    }

    public void _OnGraphEditorConnectionRequest(string from, int fromSlot, string to, int toSlot)
    {
        ConnectNode(from, fromSlot, to, toSlot);
        GD.Print(GetConnectionList());
    }

    public void _OnGraphEditorDisconnectionRequest(string from, int fromSlot, string to, int toSlot)
    {
        DisconnectNode(from, fromSlot, to, toSlot);
        GD.Print(GetConnectionList());
    }

}
