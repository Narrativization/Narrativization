using Godot;
using System;

public class DetailsModal : WindowDialog
{
    // Exports
    [Export]
    private NodePath PropertiesPath = new NodePath("./Tabs/Properties");
    [Export]
    private NodePath readOnlyPath = new NodePath("./ReadOnly");

    public Node properties;
    public PackedScene storyElementTemplate; // Story Element Properties Template
    public CheckButton readOnlyButton;
    public Storier.StoryElement editedStoryElement;
    

    public override void _Ready()
    {
        properties = GetNode<Node>(PropertiesPath);
        readOnlyButton = GetNode<CheckButton>(readOnlyPath);
    }

    public void InitDialogue(Storier.StoryElement storyElement)
    {
        foreach (Node item in properties.GetChildren())
        {
            item.Free();
        }
        PackedScene propertyTemplate = ResourceLoader.Load(storyElement.defaultTemplate) as PackedScene;
        properties.AddChild(propertyTemplate.Instance());
        editedStoryElement = storyElement;
    }

    public override void _Notification(int what)
{
    switch(what)
    {
        case NotificationModalClose:
            editedStoryElement.Save();
            break;
    }
}

}
