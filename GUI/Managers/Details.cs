using Godot;
using System;
using Storier;

public class Details : Panel
{


    public override void _Ready()
    {
        StoryManager.Self.Connect("StoryOpened", this, "_OnStoryOpened");
        StoryManager.Self.Connect("StoryElementOpened", this, "_OnStoryElementOpened");
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
        GetNode<RichTextLabel>("DetailsText").Text = story.StoryName;
    }

    public void _OnStoryElementOpened(StoryElement storyElement)
    {
        GetNode<RichTextLabel>("DetailsText").Text = storyElement.Name;
    }

}
