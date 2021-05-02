using Godot;
using System;

public class TitleDisplayer : RichTextLabel
{

    public override void _Ready()
    {
        Connect("meta_clicked", this, "_OnMetaClicked");
        StoryManager.CurrentStoryManager.Connect("TitleIdChanged", this, "_OnTitleIdChanged");
    }

    // public override void _Process(float delta)
    // {
    //     // ChangeTitle(); // Replaced with StoryOpened and StoryElementOpened
    //     base._Process(delta);
    // }

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

    public void _OnTitleIdChanged(string titleId)
    {
        TitleText(StoryManager.CurrentTitleId);
    }

    public void _OnMetaClicked(string _meta)
    {
        GD.Print("Clicked: \""+ _meta + "\""); // "/<StoryElement Id> ~or storier://<fileName>
    }

}
