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
        displayedFlow = Text;
    }

    public override void _Process(float delta)
    {
        if (IsInstanceValid(StoryManager.CurrentStory) && StoryManager.CurrentStory.StoryElementId != displayedFlow) // ~Replace with Signal
        {
            ChangeFlowTitle(StoryManager.CurrentStory.Name + StoryManager.CurrentStory.StoryElementId);
            displayedFlow = StoryManager.CurrentStory.StoryElementId;
            GD.Print("Opened Element: \"" + StoryManager.CurrentStory.StoryElementId + "\"");
            Godot.Collections.Dictionary dictionary = new Godot.Collections.Dictionary();
            dictionary = FileManager.DirectoryTree(StoryManager.CurrentStory.StoryBase + StoryManager.CurrentStory.StoryElementId);
            // GD.Print(JSON.Print(dictionary, "\t"));
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
            cFlow += ("[url=" + fileName + "]" + flowNames[i] + "[/url]");
            if (i != flowNames.Length - 1) { cFlow += " / "; }
        }
        cFlow += "[/center]";
        BbcodeText = cFlow;
    }

    public void _OnMetaClicked(string _meta)
    {
        GD.Print("Clicked: \""+ _meta + "\""); // "/<StoryElement Id> ~or storier://<fileName>
    }

}
