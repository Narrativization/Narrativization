using Godot;
using System;
using Storier;

public class FlowTitle : RichTextLabel
{
    [Export]
    public string displayedFlow = "";

    public override void _Ready()
    {
        
    }

    public override void _Process(float delta)
    {
        if (Story.flowTarget != "" && Story.flowTarget != displayedFlow) // ~Replace with Signal
        {
            ChangeFlowTitle(Story.flowTarget);
            displayedFlow = Story.flowTarget;
            GD.Print(StoryManager.workspace  + Story.flowTarget);
            Godot.Collections.Dictionary dictionary = new Godot.Collections.Dictionary();
            dictionary = FileManager.DirectoryContents(StoryManager.currentStory.storyData.directory);
            GD.Print("Flow Changed");
            GD.Print(dictionary);
        }
        base._Process(delta);
    } 

    public void ChangeFlowTitle(string _flowName)
    {
        string cFlow = "[center]";
        string[] flowNames = _flowName.Split("/");

        for (int i = 0; i < flowNames.Length; i++)
        {
            cFlow += ("[url]" + flowNames[i] + "[/url] / ");
        }
        cFlow += "[/center]";
        BbcodeText = cFlow;
    }


}
