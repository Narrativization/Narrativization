using Godot;
using System;
using Storier;

public class FlowTitle : RichTextLabel
{
    [Export]
    public string displayedFlow = "";

    public override void _Ready()
    {
        Connect("meta_clicked", this, "_OnMetaClicked");
        
    }

    public override void _Process(float delta)
    {
        if (Story.flowTarget != "" && Story.flowTarget != displayedFlow) // ~Replace with Signal
        {
            ChangeFlowTitle(Story.flowTitle);
            displayedFlow = Story.flowTarget;
            GD.Print(StoryManager.workspace  + Story.flowTarget);
            Godot.Collections.Dictionary dictionary = new Godot.Collections.Dictionary();
            dictionary = FileManager.DirectoryTree(StoryManager.currentStory.storyData.directory);
            GD.Print("Flow Changed");
            GD.Print(dictionary);
            GD.Print("Folder: " + StoryManager.currentStory.storyData.directory + Story.flowTitle);
            GD.Print("Folder Content: " + FileManager.DirectoryContents(StoryManager.currentStory.storyData.directory + Story.flowTitle));
        }
        base._Process(delta);
    } 

    public void ChangeFlowTitle(string _flowName)
    {
        string cFlow = "[center]";
        string fileName = "";
        string[] flowNames = _flowName.Split("/");

        for (int i = 0; i < flowNames.Length; i++)
        {
            fileName += flowNames[i] + "/";
            cFlow += ("[url=" + fileName + "]" + flowNames[i] + "[/url]" + " / ");
        }
        cFlow += "[/center]";
        BbcodeText = cFlow;
    }

    public void _OnMetaClicked(string _meta)
    {
        GD.Print("Clicked: "+ _meta); // "/<StoryElement Id> ~or storier://<fileName>
    }

}
