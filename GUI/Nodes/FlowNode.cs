using Godot;
using System;

public enum FlowState
{
    Begin,      // Start
    Progress,   // Process
    End         // Finish
}

// Generic Flow Node
public class FlowNode : FlowGraphNode
{
    [Export]
    public StoryElement storyFlow;
        
    public FlowNode() : base() { }

    public override void _Ready()
    {
        base._Ready();
    }

    //  public override void _Process(float delta)
    //  {
    //      
    //  }

    public override void _GuiInput(InputEvent inputEvent)
    {
        base._GuiInput(inputEvent);
    }

    public StoryElement ReferenceStoryFlow(StoryElement _storyFlow)
    {
        storyFlow = _storyFlow;
        if (IsInstanceValid(storyFlow))
        {
            Title = (storyFlow.StoryElementName != "") ? storyFlow.StoryElementName : "Story Flow";
            HintTooltip = storyFlow.StoryElementId;
        }
        return storyFlow;
    }

    public override void OpenNode()
    {
        // base.OpenNode();
        if (IsInstanceValid(storyFlow))
        {
            StoryManager.CurrentStoryElement = storyFlow;
            GetTree().CallGroup("Editors", "EditStoryElement", StoryManager.CurrentStoryElement);
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

Begin/Start - Progress - End

*/

