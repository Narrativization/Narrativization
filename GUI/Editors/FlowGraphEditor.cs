using Godot;
using System;
using Godot.Collections;
using Array = Godot.Collections.Array;

// Basic Flow Controls for GraphEdit(Without Storier)
public class FlowGraphEditor : GraphEdit
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
            // ~ZoomIn to Mouse Position
            AcceptEvent();
        }
        else if (inputEvent.IsAction("ZoomOut") && hovering)
        {
            Zoom -= zoomSpeed * GetProcessDeltaTime();
            // ~ZoomOut from Mouse Position
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
            if (selected.Count > 0 && selected[0] is GraphNode graphNode)
            {
                if (IsInstanceValid(graphNode))
                    FocusNode(graphNode);
            }
        }
        
        if (inputEvent.IsActionPressed("JumpLeft")) // CTRL + Shift + Left Arrow | Get Nearlest(Left) Graph Node
        {
            GD.Print("JumpLeft");
            Godot.Collections.Array selected = GetSelected();
            Godot.Collections.Array allNodes = GetChildren();
            if (selected.Count > 0 && selected[0] is GraphNode selectedNode)
            {
                if (allNodes.Count > 0)
                {
                    GraphNode jumpNode = selectedNode as GraphNode;
                    for (int i = 0; i < allNodes.Count; i++)
                    {
                        if (allNodes[i] is GraphNode graphNode)
                        {
                            if (graphNode.Offset.x < selectedNode.Offset.x && (jumpNode == selectedNode || 
                                Mathf.Abs(selectedNode.Offset.x - graphNode.Offset.x) < Mathf.Abs(selectedNode.Offset.x - jumpNode.Offset.x)))
                            {
                                jumpNode = graphNode;
                            }
                        }
                    }
                    FocusNode(jumpNode);
                    UnselectAll();
                    jumpNode.Selected = true;
                }
            }
        }
        else if (inputEvent.IsActionPressed("JumpRight"))
        {
            GD.Print("JumpRight");
            Godot.Collections.Array selected = GetSelected();
            Godot.Collections.Array allNodes = GetChildren();
            if (selected.Count > 0 && selected[0] is GraphNode selectedNode)
            {
                if (allNodes.Count > 0)
                {
                    GraphNode jumpNode = selectedNode as GraphNode;
                    for (int i = 0; i < allNodes.Count; i++)
                    {
                        if (allNodes[i] is GraphNode graphNode)
                        {
                            if (graphNode.Offset.x > selectedNode.Offset.x && (jumpNode == selectedNode || 
                                Mathf.Abs(selectedNode.Offset.x - graphNode.Offset.x) < Mathf.Abs(selectedNode.Offset.x - jumpNode.Offset.x)))
                            {
                                jumpNode = graphNode;
                            }
                        }
                    }
                    FocusNode(jumpNode);
                    UnselectAll();
                    jumpNode.Selected = true;
                }
            }
        }
        else if (inputEvent.IsActionPressed("JumpUp"))
        {
            GD.Print("JumpUp");
            Godot.Collections.Array selected = GetSelected();
            Godot.Collections.Array allNodes = GetChildren();
            if (selected.Count > 0 && selected[0] is GraphNode selectedNode)
            {
                if (allNodes.Count > 0)
                {
                    GraphNode jumpNode = selectedNode as GraphNode;
                    for (int i = 0; i < allNodes.Count; i++)
                    {
                        if (allNodes[i] is GraphNode graphNode)
                        {
                            if (graphNode.Offset.y < selectedNode.Offset.y && (jumpNode == selectedNode || 
                                Mathf.Abs(selectedNode.Offset.y - graphNode.Offset.y) < Mathf.Abs(selectedNode.Offset.y - jumpNode.Offset.y)))
                            {
                                jumpNode = graphNode;
                            }
                        }
                    }
                    FocusNode(jumpNode);
                    UnselectAll();
                    jumpNode.Selected = true;
                }
            }
        }
        else if (inputEvent.IsActionPressed("JumpDown"))
        {
            GD.Print("JumpDown");
            Godot.Collections.Array selected = GetSelected();
            Godot.Collections.Array allNodes = GetChildren();
            if (selected.Count > 0 && selected[0] is GraphNode selectedNode)
            {
                if (allNodes.Count > 0)
                {
                    GraphNode jumpNode = selectedNode as GraphNode;
                    for (int i = 0; i < allNodes.Count; i++)
                    {
                        if (allNodes[i] is GraphNode graphNode)
                        {
                            if (graphNode.Offset.y > selectedNode.Offset.y && (jumpNode == selectedNode || 
                                Mathf.Abs(selectedNode.Offset.y - graphNode.Offset.y) < Mathf.Abs(selectedNode.Offset.y - jumpNode.Offset.y)))
                            {
                                jumpNode = graphNode;
                            }
                        }
                    }
                    FocusNode(jumpNode);
                    UnselectAll();
                    jumpNode.Selected = true;
                }
            }
        }
        else if (inputEvent.IsActionPressed("MoveLeft"))  // CTRL + Left Arrow
        {
            GD.Print("MoveLeft");
            Godot.Collections.Array selected = GetSelected();
            if (selected.Count > 0 && selected[0] is GraphNode graphNode)
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
            if (selected.Count > 0 && selected[0] is GraphNode graphNode)
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
            if (selected.Count > 0 && selected[0] is GraphNode graphNode)
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
            if (selected.Count > 0 && selected[0] is GraphNode graphNode)
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
    
    // Reset GraphEditor
    public void Reset() { DeleteAll(); FlowGraphNode.Reset(); StoryFragment.Reset(); }

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


    public bool CreateFlowNode(string nodeType = "res://GUI/Nodes/FlowNode.tscn")
    {
        GD.Print("Create Story Flow Node");
        PackedScene storyFlowNodeScene = GD.Load<PackedScene>(nodeType);
        FlowGraphNode flowNode = storyFlowNodeScene.Instance() as FlowGraphNode;
        flowNode.NodeFragment = new StoryFragment();
        AddChild(flowNode);
        flowNode.Offset = ScrollOffset + GetLocalMousePosition() - (flowNode.RectSize/2);
        return true;
    }

    public bool CreateFlowNode(Dictionary<string, object> nodeData)
    {
        GD.Print("Create Story Flow Node");
        PackedScene storyFlowNodeScene = GD.Load<PackedScene>(nodeData["Type"] as string);
        FlowGraphNode flowNode = storyFlowNodeScene.Instance() as FlowGraphNode;
        AddChild(flowNode);
        flowNode.NodeData = nodeData; 
        return true;
    }

    // All Nodes
    public Godot.Collections.Array GetAllNodes()
    {
        Godot.Collections.Array childrenNodes = GetChildren();
        Godot.Collections.Array allNodes = new Array();
        for (int i = 0; i < childrenNodes.Count; i++)
        {
            if (childrenNodes[i] is GraphNode graphNode)
            {
                allNodes.Add(graphNode);
            }
        }
        return allNodes;
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

    // Select Nodes
    public void SelectNodes(Godot.Collections.Array graphNodes)
    {
        for (int i = 0; i < graphNodes.Count; i++)
        {
            if (graphNodes[i] is GraphNode graphNode)
            {
                graphNode.Selected = true;
            }
        }
    }

    // Unselect Nodes
    public void UnselectNodes(Godot.Collections.Array graphNodes)
    {
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
                /* // Disconnect Left Slot/Port | Also implement Right Slot -> Fix Error
                Godot.Collections.Array connectionList = GetConnectionList();
                for (int j = 0; j < connectionList.Count; j++)
                {
                    if (connectionList[i] is Dictionary connection)
                    {
                        if ((connection["from"] != null && connection["to"] != null)
                            && selectedNode.Equals(GetNode<GraphNode>("./" + connection["to"])))
                        {
                            DisconnectNode((string)connection["from"], (int)connection["from_port"], (string)connection["to"], (int)connection["to_port"]);
                        }
                    }
                }*/
                selectedNode.QueueFree();
            }
        }
    }

    // Delete/Removes All Nodes | Delete All GraphNodes
    public void DeleteAll()
    {
        Godot.Collections.Array graphNodes = GetAllNodes();
        for (int i = 0; i < graphNodes.Count; i++)
        {
            if (graphNodes[i] is GraphNode graphNode)
            {
                graphNode.Name = "Deleted" + graphNode.Name;
                graphNode.QueueFree();
            }
        }
    }

    // Duplicate Selected Nodes | Duplicate selected GraphNodes
    public void DuplicateSelected()
    {
        Godot.Collections.Array duplicatedNodes = new Godot.Collections.Array();
        Godot.Collections.Array selectedNodes = GetSelected();
        for (int i = 0; i < selectedNodes.Count; i++)
        {
            if (selectedNodes[i] is GraphNode selectedNode)
            {
                GraphNode duplicated = selectedNode.Duplicate((int)DuplicateFlags.UseInstancing) as GraphNode;
                duplicated.Offset += new Vector2(10, 10); 
                duplicatedNodes.Add(duplicated);
                AddChild(duplicated);
            }
        }
        UnselectAll();
        SelectNodes(duplicatedNodes);
    }

    public void _OnGraphEditorConnectionRequest(string from, int fromSlot, string to, int toSlot)
    {
        ConnectNode(from, fromSlot, to, toSlot);
        GD.Print(from + " " + fromSlot + " - " + to + " " + toSlot);
    }

    public void _OnGraphEditorDisconnectionRequest(string from, int fromSlot, string to, int toSlot)
    {
        DisconnectNode(from, fromSlot, to, toSlot);
    }

}
