using Godot;
using System;
using Storier;

// Generic Flow Node
public class FlowNode : FlowGraphNode
{
    [Export]
    public string Id = "";
    [Export]
    public StoryFlow storyFlow;

    public bool isFlow = false; // Isn't: Story(.storier), Folder, Entity, Asset etc.   
    StoryElement storyElement; // Referenced Story Element ~StoryFlow
    public Godot.Collections.Dictionary displayData = new Godot.Collections.Dictionary(); 

    public override void _Ready()
    {
        
    }

    //  public override void _Process(float delta)
    //  {
    //      
    //  }

    public override void _GuiInput(InputEvent inputEvent)
    {
        base._GuiInput(inputEvent);
    } 

    public void InitNode(string _id)
    {
        Id = _id;

        if (Id != "")
        {
            storyFlow = StoryFlow.GetStoryFlow(Id);
            if (storyFlow == null)
            {
                string[] fNames = Id.Split("/");
                storyFlow = new StoryFlow(Id, fNames[fNames.Length - 1], "");
            }
            storyFlow.Save("");
            Title = (storyFlow.Name != "") ? storyFlow.Name : "Story Flow";
            HintTooltip = Id;
        }
    }

}

/*
    // When resize, resize description text area
    public void _OnStoryFlowNodeResizeRequest(Vector2 newMinSize)
    {
        RectSize = newMinSize;
        int botMargin = 50;
        if (newMinSize.y - botMargin < textDefaultMinSize.y)
        {
            GetChild<TextEdit>(0).RectMinSize = new Vector2(GetChild<TextEdit>(0).RectMinSize.x, textDefaultMinSize.y);
        }
        else
        {
            GetChild<TextEdit>(0).RectMinSize = new Vector2(GetChild<TextEdit>(0).RectMinSize.x, newMinSize.y - botMargin);
        }
    }
*/