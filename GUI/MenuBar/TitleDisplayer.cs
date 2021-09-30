using Godot;
using System;

// Rename with Navigation Bar | Display Seid 
public class TitleDisplayer : RichTextLabel
{
    // Signature for Modified/Unsaved Story Element
    public bool isModified = false; 

    public override void _Ready()
    {
        AddToGroup("SeidDisplayer");
        Connect("meta_clicked", this, "_OnMetaClicked");
        StoryManager.CurrentStoryManager.Connect("OnStoryElementIdEdited", this, "_OnStoryElementIdEdited");
        StoryManager.CurrentStoryManager.Connect("OnStoryElementEdited", this, "_OnStoryElementEdited");
    }

    // public override void _Process(float delta)
    // {
    //     // ChangeTitle(); // Replaced with StoryOpened and StoryElementOpened
    //     base._Process(delta);
    // }

    public void UpdateTitle() { TitleText(StoryManager.CurrentSeid); }

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
        cFlow += (isModified)?"(*)":""; // Check isModified?
        cFlow += "[/center]";
        BbcodeText = cFlow;
    }

    public void _OnStoryElementIdEdited(string titleId)
    {
        UpdateTitle();
    }

    public void _OnStoryElementEdited(StoryElement storyElement)
    {
        isModified = false;
        UpdateTitle();
    }

    public void _OnMetaClicked(string _meta)
    {
        GD.Print("Clicked: \""+ _meta + "\""); // "/<StoryElement Id> ~or storier://<fileName>
    }

}
