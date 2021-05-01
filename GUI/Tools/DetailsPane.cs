using Godot;
using System;
using Storier;

public class DetailsPane : Panel
{
    
    private Editors editors;

    public override void _Ready()
    {
        StoryManager.Self.Connect("StoryOpened", this, "_OnStoryOpened");
        StoryManager.Self.Connect("StoryElementOpened", this, "_OnStoryElementOpened");
        StoryManager.Self.Connect("StoryFragmentSelected", this, "_OnStoryFragmentSelected");
    }

    public override void _Process(float delta)
    {
        // if (IsInstanceValid(StoryManager.CurrentStory) && IsInstanceValid(StoryManager.CurrentStory.CurrentStoryElement))
        // {
        //     GetNode<RichTextLabel>("DetailsText").Text = JSON.Print(StoryManager.CurrentStory.CurrentStoryElement.StoryElementData, "\t");
        // }
    }

    public void _OnStoryOpened(Story story)
    {
        if (!IsInstanceValid(story)) { return; }
        GetNode<RichTextLabel>("DetailsText").Text = story.StoryName;
    }

    public void _OnStoryElementOpened(StoryElement storyElement)
    {
        if (!IsInstanceValid(storyElement)) { return; }
        GetNode<RichTextLabel>("DetailsText").Text = storyElement.Name;
        if (storyElement is StoryFlow storyFlow)
        {
            editors = GetTree().GetNodesInGroup("Editors")[0] as Editors;
            editors.flowEditor.Connect("node_selected", this, "_OnStoryFragmentSelected");
        }
    }

    public void _OnStoryFragmentSelected(StoryFlowNode flowNode)
    {
        if (!IsInstanceValid(flowNode)) { return; }
        GetNode<RichTextLabel>("DetailsText").Text = flowNode.FragmentId;
    }

}
