using Godot;
using System;

public class DetailsModal : WindowDialog
{
    // Exports
    [Export]
    private NodePath PropertiesPath = new NodePath("./Tabs/Properties");
    [Export]
    private NodePath pureDataPath = new NodePath("./Tabs/Pure Data/PureData");
    [Export]
    private NodePath resourceNamePath = new NodePath("./ResourceName");
    [Export]
    private NodePath readOnlyPath = new NodePath("./ReadOnly");

    public Node properties;
    public PackedScene storyElementTemplate; // Story Element Properties Template
    public CheckButton readOnlyButton;
    public TextEdit pureData;
    public LineEdit resourceName;
    public Storier.StoryElement editedStoryElement;

    public override void _Ready()
    {
        properties = GetNode<Node>(PropertiesPath);
        readOnlyButton = GetNode<CheckButton>(readOnlyPath);
        pureData = GetNode<TextEdit>(pureDataPath);
        resourceName = GetNode<LineEdit>(resourceNamePath);
    }

    public void InitModal(Storier.StoryElement storyElement)
    {
        foreach (Node item in properties.GetChildren())
        {
            item.Free();
        }
        PackedScene propertyTemplate = ResourceLoader.Load(storyElement.defaultTemplate) as PackedScene;
        properties.AddChild(propertyTemplate.Instance());
        editedStoryElement = storyElement;
        resourceName.Text = editedStoryElement.ResourceName;
    }

    public override void _Notification(int what)
    {
        switch(what)
        {
            case NotificationModalClose:
                StoryElementTemplate templateProperties = properties.GetChild<StoryElementTemplate>(0);
                GD.Print(templateProperties.GetSaveData());
                editedStoryElement.ResourceName = resourceName.Text;
                editedStoryElement.Save(JSON.Print(templateProperties.GetSaveData(), "\t"));
                break;
        }
    }

    public void _OnTabsTabChanged(int tab)
    {
        StoryElementTemplate templateProperties = properties.GetChild<StoryElementTemplate>(0);
        pureData.Text = JSON.Print(templateProperties.GetSaveData(), "\t");
    }

    public void _OnReadOnlyToggled(bool _buttonPressed)
    {
        if (_buttonPressed)
        {
            resourceName.Editable = false;
        }
        else
        {
            resourceName.Editable = true;
        }
    }

}
