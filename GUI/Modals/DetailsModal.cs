using Godot;
using System;
using Storier;

public class DetailsModal : WindowDialog
{
    // Exports
    [Export]
    private NodePath PropertiesPath = new NodePath("./Tabs/Properties");
    [Export]
    private NodePath pureDataPath = new NodePath("./Tabs/Pure Data/PureData");
    [Export]
    private NodePath resourceNamePath = new NodePath("./Menu/ResourceName");
    [Export]
    private NodePath readOnlyPath = new NodePath("./Menu/ReadOnly");
    [Export]
    private NodePath entityTypePath = new NodePath("./Menu/EntityType");

    public Node properties;
    public PackedScene storyElementTemplate; // Story Element Properties Template
    public CheckButton readOnlyButton;
    public TextEdit pureData;
    public LineEdit resourceName;
    public MenuButton entityType;
    public StoryElement editedStoryElement;

    public override void _Ready()
    {
        properties = GetNode<Node>(PropertiesPath);
        readOnlyButton = GetNode<CheckButton>(readOnlyPath);
        pureData = GetNode<TextEdit>(pureDataPath);
        resourceName = GetNode<LineEdit>(resourceNamePath);
        entityType = GetNode<MenuButton>(entityTypePath);
        
        if (IsInstanceValid(entityType))
        {
            entityType.GetPopup().Connect("id_pressed", this, "_OnEntityTypeItemPressed");
        }
    }

    public void InitModal()
    {
        foreach (Node item in properties.GetChildren())
        {
            item.Free();
        }

        // StoryEntity storyEntity = StoryEntity.GetStoryEntity("/");
        // StoryElement storyElement = Story.CreateStoryEntity(resourceName.Text, GetEntityDirectory());//StoryElement.CreateStoryElement(resourceName.Text, GetEntityDirectory());
        // PackedScene propertyTemplate = ResourceLoader.Load(storyElement.storyElementTemplate) as PackedScene;
        // properties.AddChild(propertyTemplate.Instance());
        // editedStoryElement = storyElement;
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

    private string GetEntityDirectory()
    {
        return "Entities/" + (entityType.Text != "Other" ? entityType.Text + "/" : "");
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

    public void _OnEntityTypeItemPressed(int _id)
    {
        switch (_id)
        {
            case 0: // Character
                GD.Print("Characters");
                entityType.Text = "Characters";
                break;
            case 1: // Location
                GD.Print("Locations");
                entityType.Text = "Locations";
                break;
            case 2: // Other
                GD.Print("Other");
                entityType.Text = "Other";
                break;
            default:
            break;
        }
        editedStoryElement.TakeOverPath(StoryManager.currentStory.storyData.directory + "/" + GetEntityDirectory());
        GD.Print(StoryManager.currentStory.storyData.directory + "/" + GetEntityDirectory());
    }
}
