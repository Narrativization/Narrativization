using Godot;
using System;

public class DetailsPane : Panel
{
    
    public override void _Ready()
    {
        StoryManager.CurrentStoryManager.Connect("StoryChanged", this, "_OnStoryChanged");
        StoryManager.CurrentStoryManager.Connect("StoryElementChanged", this, "_OnStoryElementChanged");
        // StoryManager.CurrentStoryManager.Connect("StoryFragmentSelected", this, "_OnStoryFragmentSelected");
    }

    public override void _Process(float delta)
    {
        // if (IsInstanceValid(StoryManager.CurrentStory) && IsInstanceValid(StoryManager.CurrentStory.CurrentStoryElement))
        // {
        //     GetNode<RichTextLabel>("DetailsText").Text = JSON.Print(StoryManager.CurrentStory.CurrentStoryElement.StoryElementData, "\t");
        // }
    }

    public void _OnStoryChanged(Story story)
    {
        if (!IsInstanceValid(story)) { return; }
        GetNode<RichTextLabel>("DetailsText").Text = story.StoryName;
    }

    public void _OnStoryElementChanged(StoryElement storyElement)
    {
        if (!IsInstanceValid(storyElement)) { return; }
        GetNode<RichTextLabel>("DetailsText").Text = storyElement.StoryElementName;
        if (storyElement is StoryFlow)
        {
            if (!Editors.flowEditor.IsConnected("node_selected", this, "_OnStoryFragmentSelected"))
            {
                Editors.flowEditor.Connect("node_selected", this, "_OnStoryFragmentSelected");
            }
        }
    }

    public void _OnStoryFragmentSelected(FlowNode flowNode)
    {
        if (!IsInstanceValid(flowNode)) { return; }
        GetNode<RichTextLabel>("DetailsText").Text = "" + JSON.Print(flowNode.NodeData, "\t");
    }

}
