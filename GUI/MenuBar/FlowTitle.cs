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
        if (IsInstanceValid(StoryManager.CurrentStory) && StoryManager.CurrentStory.StoryElementId != "" && StoryManager.CurrentStory.StoryElementId != displayedFlow) // ~Replace with Signal
        {
            ChangeFlowTitle("..." + StoryManager.CurrentStory.StoryElementId); // Dots(...) means root
            displayedFlow = StoryManager.CurrentStory.StoryElementId;
            GD.Print(StoryManager.CurrentStory.StoryBase + StoryManager.CurrentStory.StoryElementId);
            Godot.Collections.Dictionary dictionary = new Godot.Collections.Dictionary();
            dictionary = FileManager.DirectoryTree(StoryManager.CurrentStory.StoryBase + StoryManager.CurrentStory.StoryElementId);
            GD.Print("Flow Changed");
            GD.Print(dictionary);
            GD.Print("Folder: " + StoryManager.CurrentStory.StoryBase + StoryManager.CurrentStory.StoryElementId);
            GD.Print("Folder Content: " + FileManager.DirectoryContents(StoryManager.CurrentStory.StoryBase + StoryManager.CurrentStory.StoryElementId));
        }
        base._Process(delta);
    } 

    public void ChangeFlowTitle(string _flowName)
    {
        string cFlow = "[center]";
        string fileName = "";
        string[] flowNames = _flowName.Split("/", false);

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
