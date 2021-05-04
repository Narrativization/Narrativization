using Godot;
using System;
using Godot.Collections;

// Story Flow Fragment
public class StoryFragment : Godot.Object
{
    // Flow Fragment Index
    public static int FragmentIndex { get; set; } = 0;

    // Flow Fragment Id
    public int FragmentId { get; set; } = FragmentIndex++; // Unique Fragment Id
    
    // Custom Fragment Data
    public Dictionary<string, object> FragmentData { get; set; } = new Dictionary<string, object>(); 
    
    public Vector2 Slots { get; set; } = new Vector2(-1, -1); // Left & Right Slot ~
    
    public Vector2 Offset { get; set; } = new Vector2(0, 0);
    
    public string ReferenceId { get; set; } = ""; // Referenced Story Element Id (/Flow/...)

    public string FragmentContent = "";


}
