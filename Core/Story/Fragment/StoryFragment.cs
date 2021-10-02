using Godot;
using System;
using Godot.Collections;
using Array = Godot.Collections.Array;

// Story Flow Fragment | Informations of Nodes Contained
public class StoryFragment : Godot.Object
{
    // Fragment Id
    public string FragmentId { get { return FragmentData["Id"] as string; } set { FragmentData["Id"] = value; } }

    // Story Fragment Data
    public Dictionary<string, object> FragmentData { get; set; } = new Dictionary<string, object>(); 

    // Fragment Data
    public StoryFragmentType FragmentType { get { return (StoryFragmentType)FragmentData["Type"]; } set { FragmentData["Type"] = value; } }
    public string FragmentName { get { return FragmentData["Name"] as string; } set { FragmentData["Name"] = value; } }
    public string FragmentDescription { get { return FragmentData["Description"] as string; } set { FragmentData["Description"] = value; } }
    public string FragmentContent { get { return FragmentData["Content"] as string; } set { FragmentData["Content"] = value; } }

    public StoryFragment() 
    { 
        FragmentType = StoryFragmentType.Outline;
        FragmentName = "";
        FragmentDescription = "";
        FragmentContent = "";
    }

    
}

/*

Currently not in use!
~Templatable
[Dialogue]
Data: {
    Name: ""
    Character: ""
    Parenthetical: ""
    Dialogue: ""
}
Block: "/Template/Block/..."
Node: "/Template/Nodes/..."



*/

/*
Unique Id
Guid guid = Guid.NewGuid();
GD.Print(guid);
*/


/*
Flow Fragment Informations
---
    FlowFragment
FlowFragmentType: <Type>,
Name: , 
Description: , 
Content: , 
---

--- Example
    Dialogue
FlowFragmentType: Dialogue
Name: ""
Description: , 
Character: ""
Parenthetical: ""
Dialogue: ""
---

Unique: FlowFragment-0, FlowFragment-1, FlowFragment-10, FlowFragment-1000, ...
*/