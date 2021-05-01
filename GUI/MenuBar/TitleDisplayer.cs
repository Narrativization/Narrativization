using Godot;
using System;

public class TitleDisplayer : RichTextLabel
{

    public override void _Ready()
    {
        Connect("meta_clicked", this, "_OnMetaClicked");
        StoryManager.Self.Connect("StoryOpened", this, "_OnStoryOpened");
        StoryManager.Self.Connect("StoryElementOpened", this, "_OnStoryElementOpened");
    }

    // public override void _Process(float delta)
    // {
    //     // ChangeTitle(); // Replaced with StoryOpened and StoryElementOpened
    //     base._Process(delta);
    // }

    private void ChangeTitle()
    {
        if (IsInstanceValid(StoryManager.CurrentStory)) // ~Replace with Signal
        {
            TitleText(StoryManager.CurrentStory.CurrentStoryElementTitle);
        }
    }

    private void TitleText(string _flowName)
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

    public void _OnStoryOpened(Story story)
    {
        // GD.Print(story.StoryName);
        ChangeTitle();
    }

    public void _OnStoryElementOpened(StoryElement storyElement)
    {
        // GD.Print(storyElement.Name);
        ChangeTitle();
    }

    public void _OnMetaClicked(string _meta)
    {
        GD.Print("Clicked: \""+ _meta + "\""); // "/<StoryElement Id> ~or storier://<fileName>
    }

}
