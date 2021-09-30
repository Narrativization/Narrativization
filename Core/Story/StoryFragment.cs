using Godot;
using System;
using Godot.Collections;
using Array = Godot.Collections.Array;

// Fragment Type
public enum FragmentType
{
    Flow,           // Default Fragment             | Name, Description, Content
    Action,         // Scenario like Action         | Name, Content
    Dialogue        // Scenario like Dialogue       | Name, Entity(Character), Parenthetical, Dialogue
    // SceneHeading // Scenario like Scene Heading
    // Transition   // Scenario like Scene Transition
}

// Story Flow Fragment | Informations of Nodes Contained
public class StoryFragment : Godot.Object
{
    // Generic Fragment Index
    public static int GenericIdIndex { get; set; } = 0; // Fragment id Index
    public static Array<StoryFragment> StoryFragments = new Array<StoryFragment>(); // All Created Story Fragment For Current Story

    public static Array<Dictionary<string, object>> FragmentDatas 
    { 
        get
        {
            Array<Dictionary<string, object>> _fragmentDatas = new Array<Dictionary<string, object>>();
            for (int i = 0; i < StoryFragments.Count; i++)
            {
                _fragmentDatas.Add(StoryFragments[i].FragmentSaveData);
            }
            return _fragmentDatas;
        } 
        set
        {
            for (int i = 0; i < value.Count; i++)
            {
                new StoryFragment(i, value[i]);
            }
        } 
    }

    // Flow Fragment 
    public int FragmentIndex { get; set; } = StoryFragment.GenericIdIndex++; // Fragment Id
    // Fragment Data
    public FragmentType FragmentType { get { return (FragmentType)FragmentData["Type"]; } set { FragmentData["Type"] = value; } }
    public string FragmentName { get { return FragmentData["Name"] as string; } set { FragmentData["Name"] = value; } }
    public string FragmentDescription { get { return FragmentData["Description"] as string; } set { FragmentData["Description"] = value; } }

    // Removes
    public string FragmentContent { get { return FragmentData["Content"] as string; } set { FragmentData["Content"] = value; } } // Content
    public string ReferenceSeid { get; set; } = ""; // Referenced Story Element Id (/Flow/...)

    // Fragment Data Dictionary
    public Dictionary<string, object> FragmentData { get; set; } = new Dictionary<string, object>()
    {
        { "Type", FragmentType.Flow},
        { "Name", ""},
        { "Description", ""},
    }; 

    // Fragment Data
    public Dictionary<string, object> FragmentSaveData 
    { 
        get
        {
            return new Dictionary<string, object>()
            {
                { "Type", FragmentType },                   // Type of Fragment
                { "Name", FragmentName },                   // Fragment Name
                { "Description", FragmentDescription },     // Description
            };
        } 
        set
        {
            FragmentType = (FragmentType)value["Type"];
            FragmentName = value["Name"] as string;
            FragmentDescription = value["Description"] as string;
        }
    } 

    public StoryFragment() 
    {
        StoryFragments.Add(this);
    }

    public StoryFragment(int index, Dictionary<string, object> fragmentData) 
    {
        GenericIdIndex = index + 1;
        FragmentIndex = index;
        FragmentSaveData = fragmentData;
        StoryFragments.Add(this);
    }

    public static StoryFragment GetStoryFragment(int index)
    {
        return StoryFragments[index];
    }
    
    // Reset StoryFragment
    public static void Reset() { GenericIdIndex = 0; StoryFragments = new Array<StoryFragment>(); }
    
    // public Vector2 Slots { get; set; } = new Vector2(-1, -1); // Left & Right Slot ~

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