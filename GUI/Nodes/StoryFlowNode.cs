using Godot;
using System;

// Generic Flow Node
public class StoryFlowNode : FlowGraphNode
{
    [Export]
    public StoryFlow storyFlow;

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
    
    public StoryFlow ReferenceStoryFlow(StoryFlow _storyFlow)
    {
        storyFlow = _storyFlow;
        if (IsInstanceValid(storyFlow))
        {
            Title = (storyFlow.Name != "") ? storyFlow.Name : "Story Flow";
            HintTooltip = storyFlow.Id;
        }
        return storyFlow;
    }

    public override void OpenNode()
    {
        // base.OpenNode();
        if (IsInstanceValid(storyFlow))
        {
            StoryManager.CurrentStory.CurrentStoryElement = storyFlow;
            GetTree().CallGroup("Editors", "EditStoryElement", StoryManager.CurrentStory.CurrentStoryElement);
        }
    }

    // InitNode

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

/*
Flow Fragment
{
    FragmentId
    Title
    ReferenceFlow
    Offset
    Slots(Connections)
}
*/

