using Godot;
using System;
using Storier;

public class StoryFlowNode : GraphNode
{
    [Export]
    public string Id = "";
    [Export]
    public StoryFlow storyFlow;

    public override void _Ready()
    {
        
    }

//  public override void _Process(float delta)
//  {
//      
//  }


    public void InitNode(string _id)
    {
        Id = _id;
        GD.Print("Ready Id: " + Id);

        if (Id != "")
        {
            storyFlow = StoryFlow.GetStoryFlow(Id);
            GD.Print("Get Story Flow Node!");

            if (storyFlow == null)
            {
                string[] fNames = Id.Split("/");
                storyFlow = new StoryFlow(Id, fNames[fNames.Length - 1], "");
                GD.Print("New Story Flow Node Created!");
            }
            storyFlow.Save("");
            Title = (storyFlow.Name != "") ? storyFlow.Name : "Story Flow";
            HintTooltip = Id;
        }
    }

}
