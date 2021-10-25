using Godot;
using System;
using Godot.Collections;
using Array = Godot.Collections.Array;

// Story Flow Fragment | Informations of Nodes Contained
public class StoryFragment : Godot.Object
{
    // Fragment Id
    public string StoryFragmentId { get { return StoryFragmentData["Id"] as string; } set { StoryFragmentData["Id"] = value; } }

    // Story Fragment Data
    public Dictionary<string, object> StoryFragmentData { get; set; } = new Dictionary<string, object>(); 

    // Fragment Data
    // public StoryFragmentType StoryFragmentType { get { return (StoryFragmentType)StoryFragmentData["Type"]; } set { StoryFragmentData["Type"] = value; } }
    public string StoryFragmentName { get { return StoryFragmentData["Name"] as string; } set { StoryFragmentData["Name"] = value; } }
    public string StoryFragmentDescription { get { return StoryFragmentData["Description"] as string; } set { StoryFragmentData["Description"] = value; } }
    public string StoryFragmentContent { get { return StoryFragmentData["Content"] as string; } set { StoryFragmentData["Content"] = value; } }

    public StoryFragment() 
    { 
        StoryFragmentId = "";
        // StoryFragmentType = StoryFragmentType.Default;
        StoryFragmentName = "";
        StoryFragmentDescription = "";
        StoryFragmentContent = "";
    }

    public StoryFragment(StoryFragmentType storyFragmentType) 
    { 
        StoryFragmentId = "";
        // StoryFragmentType = storyFragmentType;
        StoryFragmentName = "";
        StoryFragmentDescription = "";
        StoryFragmentContent = "";
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