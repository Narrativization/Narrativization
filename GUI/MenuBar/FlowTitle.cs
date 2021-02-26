using Godot;
using System;

public class FlowTitle : RichTextLabel
{
    public override void _Ready()
    {
        
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
