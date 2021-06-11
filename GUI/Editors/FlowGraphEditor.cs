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

    // Copy/Paste
    private Array<GraphNode> CopiedNodes { get; set; } = new Array<GraphNode>();
    private Vector2 CopiedScrollOffset { get; set; } = new Vector2();

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
            Array<GraphNode> selected = GetSelectedNodes();
            if (selected.Count > 0 && selected[0] is GraphNode graphNode)
            {
                if (IsInstanceValid(graphNode))
                    FocusNode(graphNode);
            }
        }
        
        if (inputEvent.IsActionPressed("SelectAll"))
        {
            GD.Print("SelectAll");
            SelectAllNodes();
        }

        if (inputEvent.IsActionPressed("Delete"))
        {
            GD.Print("Delete");
            DeleteSelected();
        }  
        
        if (inputEvent.IsActionPressed("Cut"))
        {
            GD.Print("Cut");
            CopySelected();
            DeleteSelected();
        } 
        else if (inputEvent.IsActionPressed("Copy"))
        {
            GD.Print("Copy");
            CopySelected();
        }
        else if (inputEvent.IsActionPressed("Paste"))
        {
            GD.Print("Paste");
            PasteSelected();
        } 
        else if (inputEvent.IsActionPressed("Duplicate"))
        {
            GD.Print("Duplicate Selected Nodes");
            DuplicateSelected();
        }   
        
        if (inputEvent.IsActionPressed("JumpLeft")) // CTRL + Shift + Left Arrow | Get Directional Nearlest(Left) Graph Node
        {
            GD.Print("JumpLeft");
            JumpNode("Left");
        }
        else if (inputEvent.IsActionPressed("JumpRight"))
        {
            GD.Print("JumpRight");
            JumpNode("Right");
        }
        else if (inputEvent.IsActionPressed("JumpUp"))
        {
            GD.Print("JumpUp");
            JumpNode("Up");
        }
        else if (inputEvent.IsActionPressed("JumpDown"))
        {
            GD.Print("JumpDown");
            JumpNode("Down");
        }
        else if (inputEvent.IsActionPressed("AnyLeft")) // Alt + Shift + Left Arrow | Get Any Nearlest(Left) Graph Node
        {
            GD.Print("AnyLeft");
            JumpAnyNode("Left");
        }
        else if (inputEvent.IsActionPressed("AnyRight"))
        {
            GD.Print("AnyRight");
            JumpAnyNode("Right");
        }
        else if (inputEvent.IsActionPressed("AnyUp"))
        {
            GD.Print("AnyUp");
            JumpAnyNode("Up");
        }
        else if (inputEvent.IsActionPressed("AnyDown"))
        {
            GD.Print("AnyDown");
            JumpAnyNode("Down");
        }
        else if (inputEvent.IsActionPressed("MoveLeft"))  // CTRL + Left Arrow
        {
            GD.Print("MoveLeft");
            Array<GraphNode> selected = GetSelectedNodes();
            if (selected.Count > 0 && selected[0] is GraphNode graphNode)
            {
                Array<GraphNode> lefts = GetLeftNodes(graphNode);
                if (lefts.Count > 0)
                {
                    FocusNode(lefts[0]);
                    UnselectAllNodes();
                    lefts[0].Selected = true;
                    EmitSignal("node_selected", lefts[0]);
                }
            }
        }
        else if (inputEvent.IsActionPressed("MoveRight"))
        {
            GD.Print("MoveRight");
            Array<GraphNode> selected = GetSelectedNodes();
            if (selected.Count > 0 && selected[0] is GraphNode graphNode)
            {
                Array<GraphNode> rights = GetRightNodes(graphNode);
                if (rights.Count > 0)
                {
                    FocusNode(rights[0]);
                    UnselectAllNodes();
                    rights[0].Selected = true;
                    EmitSignal("node_selected", rights[0]);
                } 
            }
        }
        else if (inputEvent.IsActionPressed("MoveUp"))
        {
            GD.Print("MoveUp");
            Array<GraphNode> selected = GetSelectedNodes();
            if (selected.Count > 0 && selected[0] is GraphNode graphNode)
            {
                GraphNode upNode = selected[0] as GraphNode;
                Array<GraphNode> lefts = GetLeftNodes(graphNode);
                if (lefts.Count > 0)
                {
                    Array<GraphNode> rights = GetRightNodes(lefts[0]);
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
                UnselectAllNodes();
                upNode.Selected = true;
                EmitSignal("node_selected", upNode);
            }
        }
        else if (inputEvent.IsActionPressed("MoveDown"))
        {
            GD.Print("MoveDown");
            Array<GraphNode> selected = GetSelectedNodes();
            if (selected.Count > 0 && selected[0] is GraphNode graphNode)
            {
                GraphNode downNode = selected[0] as GraphNode;
                Array<GraphNode> lefts = GetLeftNodes(graphNode);
                if (lefts.Count > 0)
                {
                    Array<GraphNode> rights = GetRightNodes(lefts[0]);
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
                UnselectAllNodes();
                downNode.Selected = true;
                EmitSignal("node_selected", downNode);
            }
        }

        if (inputEvent.IsActionPressed("Backward"))
        {
            GD.Print("Previous Flow");
        }
        else if (inputEvent.IsActionPressed("Forward"))
        {
            GD.Print("Next Flow");
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
    public virtual void Reset() { DeleteAll(); FlowGraphNode.Reset(); }

    // Centered to given GraphNode
    public void FocusNode(GraphNode node)
    {
        if (!IsInstanceValid(node)) { GD.Print("Focus Node isn't valid!"); }
        ScrollOffset = node.Offset - (RectSize/2) + (node.RectSize/2);
    }

    // Get Left/Previous Nodes
    public Array<GraphNode> GetLeftNodes(GraphNode graphNode)
    {
        Array<GraphNode> lefts = new Array<GraphNode>();
        Array connectionList = GetConnectionList();
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
    public Array<GraphNode> GetRightNodes(GraphNode graphNode)
    {
        Array<GraphNode> rights = new Array<GraphNode>();
        Array connectionList = GetConnectionList();
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

    public void JumpAnyNode(string direction = "Left")
    {
        Array<GraphNode> selected = GetSelectedNodes();
        Array<GraphNode> allNodes = GetAllNodes();
        if (selected.Count > 0 && allNodes.Count > 0)
        {
            GraphNode jumpNode = selected[0];
            for (int i = 0; i < allNodes.Count; i++)
            {
                if (direction == "Left" && (allNodes[i].Offset.x < selected[0].Offset.x && (jumpNode == selected[0] || 
                    Mathf.Abs(selected[0].Offset.x - allNodes[i].Offset.x) < Mathf.Abs(selected[0].Offset.x - jumpNode.Offset.x))))
                {
                    jumpNode = allNodes[i];
                }
                else if (direction == "Right" && (allNodes[i].Offset.x > selected[0].Offset.x && (jumpNode == selected[0] || 
                    Mathf.Abs(selected[0].Offset.x - allNodes[i].Offset.x) < Mathf.Abs(selected[0].Offset.x - jumpNode.Offset.x))))
                {
                    jumpNode = allNodes[i];
                }
                else if (direction == "Up" && (allNodes[i].Offset.y < selected[0].Offset.y && (jumpNode == selected[0] || 
                    Mathf.Abs(selected[0].Offset.y - allNodes[i].Offset.y) < Mathf.Abs(selected[0].Offset.y - jumpNode.Offset.y))))
                {
                    jumpNode = allNodes[i];
                }
                else if (direction == "Down" && (allNodes[i].Offset.y > selected[0].Offset.y && (jumpNode == selected[0] || 
                    Mathf.Abs(selected[0].Offset.y - allNodes[i].Offset.y) < Mathf.Abs(selected[0].Offset.y - jumpNode.Offset.y))))
                {
                    jumpNode = allNodes[i];
                }
            }
            FocusNode(jumpNode);
            UnselectAllNodes();
            jumpNode.Selected = true;
            EmitSignal("node_selected", jumpNode);
        }
    }

    public void JumpNode(string direction = "Left")
    {
        Array<GraphNode> selected = GetSelectedNodes();
        Array<GraphNode> allNodes = GetAllNodes();
        if (selected.Count > 0 && allNodes.Count > 0)
        {
            GraphNode jumpNode = selected[0];
            for (int i = 0; i < allNodes.Count; i++)
            {
                if (direction == "Left" && (allNodes[i].Offset.x < selected[0].Offset.x && (jumpNode == selected[0] || 
                    Mathf.Abs(selected[0].Offset.y - allNodes[i].Offset.y) < Mathf.Abs(selected[0].Offset.y - jumpNode.Offset.y))))
                {
                    jumpNode = allNodes[i];
                }
                else if (direction == "Right" && (allNodes[i].Offset.x > selected[0].Offset.x && (jumpNode == selected[0] || 
                    Mathf.Abs(selected[0].Offset.y - allNodes[i].Offset.y) < Mathf.Abs(selected[0].Offset.y - jumpNode.Offset.y))))
                {
                    jumpNode = allNodes[i];
                }
                else if (direction == "Up" && (allNodes[i].Offset.y < selected[0].Offset.y && (jumpNode == selected[0] || 
                    Mathf.Abs(selected[0].Offset.x - allNodes[i].Offset.x) < Mathf.Abs(selected[0].Offset.x - jumpNode.Offset.x))))
                {
                    jumpNode = allNodes[i];
                }
                else if (direction == "Down" && (allNodes[i].Offset.y > selected[0].Offset.y && (jumpNode == selected[0] || 
                    Mathf.Abs(selected[0].Offset.x - allNodes[i].Offset.x) < Mathf.Abs(selected[0].Offset.x - jumpNode.Offset.x))))
                {
                    jumpNode = allNodes[i];
                }
            }
            FocusNode(jumpNode);
            UnselectAllNodes();
            jumpNode.Selected = true;
            EmitSignal("node_selected", jumpNode);
        }
    }

    public void RemoveLeftSlotConnections(GraphNode graphNode)
    {
        Array connectionList = GetConnectionList();
        for (int i = 0; i < connectionList.Count; i++)
        {
            if (connectionList[i] is Dictionary connection)
            {
                if (connectionList[i] != null && graphNode.Equals(GetNamedNode("./" + connection["from"])))
                {
                    DisconnectNode((string)connection["from"], (int)connection["from_port"], (string)connection["to"], (int)connection["to_port"]);
                }
            }
        }
    }

    public void RemoveRightSlotConnections(GraphNode graphNode)
    {
        Array connectionList = GetConnectionList();
        for (int i = 0; i < connectionList.Count; i++)
        {
            if (connectionList[i] is Dictionary connection)
            {
                if (connectionList[i] != null && graphNode.Equals(GetNamedNode("./" + connection["to"])))
                {
                    DisconnectNode((string)connection["from"], (int)connection["from_port"], (string)connection["to"], (int)connection["to_port"]);
                }
            }
        }
    }

    public void RemoveAllSlotConnections(GraphNode graphNode)
    {
        RemoveLeftSlotConnections(graphNode);
        RemoveRightSlotConnections(graphNode);
    }

    // All Nodes
    public Array<GraphNode> GetAllNodes()
    {
        Array childrenNodes = GetChildren();
        Array<GraphNode> allNodes = new Array<GraphNode>();
        for (int i = 0; i < childrenNodes.Count; i++)
        {
            if (childrenNodes[i] is GraphNode graphNode)
            {
                allNodes.Add(graphNode);
            }
        }
        return allNodes;
    }

    // Get Given Named Nodes
    public GraphNode GetNamedNode(string nodeName)
    {
        return GetNode<GraphNode>("./" + nodeName);
    }

    // Get Unique Node Name
    public string GetUniqueNodeName(string nodeName) // Guid.NewGuid();
    {
        string uniqueName = nodeName.ToString();
        int counter = 0;
        while (HasNode("./" + uniqueName))
        {
            counter++;
            int countSeperator = uniqueName.LastIndexOf('_');
            if (countSeperator != -1) { uniqueName = uniqueName.Replace(uniqueName.Substring(countSeperator), "_" + counter); }
            else if (uniqueName == nodeName) { uniqueName += "_" + counter;  }
        }
        return uniqueName;
    }

    // Get selected nodes | Return selected GraphNodes
    public Array<GraphNode> GetSelectedNodes()
    {
        Array<GraphNode> allNodes = GetAllNodes();
        Array<GraphNode> selectedNodes = new Array<GraphNode>();
        for (int i = 0; i < allNodes.Count; i++)
        {
            if (allNodes[i].Selected)
            {
                selectedNodes.Add(allNodes[i]);
            }
        }
        return selectedNodes;
    }

    // Select All
    public void SelectAllNodes()
    {
        Array<GraphNode> allNodes = GetAllNodes();
        for (int i = 0; i < allNodes.Count; i++)
        {
            allNodes[i].Selected = true;
        }
    }

    // Unselect All
    public void UnselectAllNodes()
    {
        Array<GraphNode> allNodes = GetAllNodes();
        for (int i = 0; i < allNodes.Count; i++)
        {
            allNodes[i].Selected = false;
        }
    }

    // Select Given Nodes
    public void SelectNodes(Array<GraphNode> graphNodes)
    {
        for (int i = 0; i < graphNodes.Count; i++)
        {
            graphNodes[i].Selected = true;
        }
    }

    // Unselect Given Nodes
    public void UnselectNodes(Array<GraphNode> graphNodes)
    {
        for (int i = 0; i < graphNodes.Count; i++)
        {
            graphNodes[i].Selected = false;
        }
    }

    // Create Empty GraphNode
    public GraphNode CreateGraphNode()
    {
        GraphNode graphNode = new GraphNode();
        AddChild(graphNode);
        graphNode.Offset = ScrollOffset + GetLocalMousePosition() - (graphNode.RectSize/2);
        return graphNode;
    }

    // Delete/Removes Selected Nodes | Delete selected GraphNodes
    public void DeleteSelected()
    {
        Array<GraphNode> selectedNodes = GetSelectedNodes();
        for (int i = 0; i < selectedNodes.Count; i++)
        {

            if (selectedNodes[i] is GraphNode selectedNode)
            {
                RemoveAllSlotConnections(selectedNode);
                selectedNode.Name = "Deleted" + selectedNode.Name;
                selectedNode.QueueFree();
            }
        }
    }

    // Delete/Removes All Nodes | Delete All GraphNodes
    public void DeleteAll()
    {
        Array<GraphNode> graphNodes = GetAllNodes();
        for (int i = 0; i < graphNodes.Count; i++)
        {
            RemoveAllSlotConnections(graphNodes[i]);
            graphNodes[i].Name = "Deleted" + graphNodes[i].Name;
            graphNodes[i].QueueFree();
        }
    }

    // Copy Selected Nodes
    public Array<GraphNode> CopySelected()
    {
        CopiedNodes.Clear();
        Array<GraphNode> selectedNodes = GetSelectedNodes();
        CopiedScrollOffset = ScrollOffset + GetLocalMousePosition();
        for (int i = 0; i < selectedNodes.Count; i++)
        {
            GraphNode copied = selectedNodes[i].Duplicate((int)DuplicateFlags.UseInstancing) as GraphNode;
            CopiedNodes.Add(copied);
        }
        return CopiedNodes;
    }

    // Duplicate Selected Nodes
    public void PasteSelected()
    {
        UnselectAllNodes();
        Array<GraphNode> copiedNodes = new Array<GraphNode>();
        for (int i = 0; i < CopiedNodes.Count; i++)
        {
            GraphNode copied = CopiedNodes[i].Duplicate((int)DuplicateFlags.UseInstancing) as GraphNode;
            copied.Offset = (CopiedNodes[i].Offset + (ScrollOffset - CopiedScrollOffset)) + GetLocalMousePosition();
            copied.Name = GetUniqueNodeName(copied.Name); 
            AddChild(copied);
            copiedNodes.Add(copied);
        }
        SelectNodes(copiedNodes);
        CopySelected();
    }

    // Duplicate Selected Nodes
    public Array<GraphNode> DuplicateSelected()
    {
        Array<GraphNode> duplicatedNodes = new Array<GraphNode>();
        Array<GraphNode> selectedNodes = GetSelectedNodes();
        for (int i = 0; i < selectedNodes.Count; i++)
        {
            GraphNode duplicated = selectedNodes[i].Duplicate((int)DuplicateFlags.UseInstancing) as GraphNode;
            duplicated.Offset += new Vector2(10, 10); 
            duplicated.Name = GetUniqueNodeName(duplicated.Name); 
            duplicatedNodes.Add(duplicated);
            AddChild(duplicated);
        }
        UnselectAllNodes();
        SelectNodes(duplicatedNodes);
        return duplicatedNodes;
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
