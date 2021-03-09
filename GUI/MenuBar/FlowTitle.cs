using Godot;
using System;
using Storier;

public class FlowTitle : RichTextLabel
{
    public override void _Ready()
    {
        
    }

    public override void _Process(float delta)
    {
        base._Process(delta);
        if (StoryManager.flowName != "" && StoryManager.flowName != Text) // ~Replace with Signal
        {
            ChangeFlowTitle(StoryManager.flowName);
        }
    }

    public void ChangeFlowTitle(string _flowName)
    {
        string cFlow = "[center]";
        string[] flowNames = _flowName.Split("/");

        for (int i = 0; i < flowNames.Length; i++)
        {
            if (i != flowNames.Length - 1) // Isn't Last 
            {
                cFlow += ("[url]" + flowNames[i] + "[/url] / ");
            }
            else // Last
            {
                cFlow += ("[url]" + flowNames[i] + "[/url][/center]");
            }
        }
        BbcodeText = cFlow;
    }


}
