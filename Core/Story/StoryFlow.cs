using Godot;
using System;
using Godot.Collections;
using Array = Godot.Collections.Array;

// Flow Definations
public class StoryFlow : StoryElement
{
    // Custom Story Element Data
    public Array<Dictionary<string, object>> StoryFlowFragments 
    { 
        get { return StoryFragment.FragmentDatas; } 
        set { StoryFragment.Reset(); StoryFragment.FragmentDatas = value; } 
    }

    public Array StoryFlowNodes 
    { 
        get
        {
            Godot.Collections.Array<GraphNode> allNodes = Editors.flowEditor.GetAllNodes();
            Godot.Collections.Array flowNodes = new Array();
            for (int i = 0; i < allNodes.Count; i++)
            {
                if (allNodes[i] is FlowGraphNode flowNode)
                {
                    GD.Print(flowNode);
                    flowNodes.Add(flowNode.NodeData);
                }
            }
            return flowNodes;
        } 
        set
        {
            for (int i = 0; i < value.Count; i++)
            {
                Dictionary loadedData = value[i] as Dictionary;  // Godot Problem: "as Dictionary<string, string>" return null
                Dictionary<string, object> nodeData = new Dictionary<string, object>(loadedData); 
                Editors.flowEditor.CreateFlowNode(nodeData);
            }
        } 
    }
    
    public Array StoryFlowConnections 
    { 
        get
        { 
            return Editors.flowEditor.GetConnectionList(); 
        }
        set
        {
            for (int i = 0; i < value.Count; i++)
            {
                Dictionary loadedData = value[i] as Dictionary;  // Godot Problem: "as Dictionary<string, string>" return null
                Dictionary<string, string> connection = new Dictionary<string, string>(loadedData);
                Editors.flowEditor.ConnectNode(connection["from"], connection["from_port"].ToInt(), connection["to"], connection["to_port"].ToInt());
            }
        }
    }

    protected StoryFlow() {} 
    public StoryFlow(string path, string name = "", string description = "") : base(path, name, description) 
    { 
        
    }

    public override bool Save()
    {
        bool isSuccess = base.Save();
        ConfigFile configFile = new ConfigFile();
        configFile.SetValue("StoryElement", "Data", StoryElementData);
        configFile.SetValue("StoryFlow", "Fragments", StoryFlowFragments);
        configFile.SetValue("StoryFlow", "Nodes", StoryFlowNodes);
        configFile.SetValue("StoryFlow", "Connections", StoryFlowConnections);
        Error err = configFile.Save(ResourcePath);
        if (err != Error.Ok) { GD.Print(err); return false; }
        return isSuccess;
    }

    protected override bool Load()
    {
        bool isSuccess = false;
        ConfigFile configFile = new ConfigFile();
        Error err = configFile.Load(StoryElementPath);
        if (err != Error.Ok) { GD.Print(err); return false; }
        Dictionary loadedData = configFile.GetValue("StoryElement", "Data") as Dictionary;  // Godot Problem: "as Dictionary<string, string>" return null
        StoryElementData = new Dictionary<string, string>(loadedData);
        Array loadedFragments = configFile.GetValue("StoryFlow", "Fragments") as Array;
        StoryFlowFragments = new Array<Dictionary<string, object>>(loadedFragments);
        Array loadedNodes = configFile.GetValue("StoryFlow", "Nodes") as Array;
        StoryFlowNodes = new Array(loadedNodes);
        Array loadedConnections = configFile.GetValue("StoryFlow", "Connections") as Array; 
        StoryFlowConnections = new Array(loadedConnections);
        return isSuccess;
    }



    
}