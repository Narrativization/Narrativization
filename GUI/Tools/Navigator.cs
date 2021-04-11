using Godot;
using System;
using System.Collections;
using Godot.Collections;
using Storier;

public class Navigator : Tree
{
    // Tree's Root
    private TreeItem root;

    private Dictionary<string, string> iconResources = new Dictionary<string, string>()
    {
        {"Folder", "res://ThirdParty/FeatherIcons/folder.svg"},
        {"File", "res://ThirdParty/FeatherIcons/file.svg"},
        {"XSquare", "res://ThirdParty/FeatherIcons/x-square.svg"},
        {"Flows", "res://ThirdParty/FeatherIcons/wind.svg"},
        {"Entities", "res://ThirdParty/FeatherIcons/codepen.svg"},
        {"Assets", "res://ThirdParty/FeatherIcons/shopping-bag.svg"},
        {"References", "res://ThirdParty/FeatherIcons/link-2.svg"},
        {"Characters", "res://ThirdParty/FeatherIcons/user.svg"},
        {"Locations", "res://ThirdParty/FeatherIcons/map-pin.svg"},
    };

    private Dictionary<string, Texture> icons = new Dictionary<string, Texture>();

    public override void _Ready()
    {
        // Resize icons 16x16
        foreach (var iRes in iconResources)
        {
            ImageTexture imgTex = FileManager.ResizeImage(ResourceLoader.Load<Texture>(iRes.Value), 16, 16); 
            icons.Add(iRes.Key, (Texture)imgTex);
        }
        // Navigator Group
        AddToGroup("Navigator");
    }

//  public override void _Process(float delta)
//  {
//      
//  }

    public void Refresh()
    {
        if(IsInstanceValid(StoryManager.CurrentStory))
        {
            CreateTree(StoryManager.CurrentStory.LookHierarchy(), StoryManager.CurrentStory.StoryName);
        }
        else
        {
            Clear();
            root = CreateItem();
            root.SetText(0,"Open Project!");
        }
    }

    public void CreateTree(Dictionary dictionary, string rootName)
    {
        Clear();
        root = CreateItemWithIcon(0, rootName, icons["Folder"], null);
        CreateTreeDisplay(dictionary, root); 
    }

    private void CreateTreeDisplay(Dictionary dictionary, TreeItem parent = null)
    {
        IDictionaryEnumerator _dicEn = dictionary.GetEnumerator();
        for (int i = 0; i < dictionary.Count; i++)
        {
            _dicEn.MoveNext();
            if (_dicEn.Key is string && _dicEn.Value is string)
            {
                if (IsInstanceValid(parent) && !((parent == root) && (parent.GetText(0) == (string)_dicEn.Key))) // Ignore Project(.storier) File
                {
                    TreeItem treeItem = CreateItemWithIcon(0, (string)_dicEn.Key, icons["File"], parent);
                    treeItem.SetMeta("Extension", _dicEn.Value);
                }
            }
            else if (_dicEn.Key is string && _dicEn.Value is Dictionary)
            {
                TreeItem treeItem = CreateItemWithIcon(0, (string)_dicEn.Key, SelectIcon((string)_dicEn.Key, parent), parent);
                CreateTreeDisplay((Dictionary)_dicEn.Value, treeItem);

                // if (!IsInstanceValid(treeItem.GetChildren()))
                // {
                //     TreeItem treeChild = CreateItemWithIcon(0, "> Empty", icons["XSquare"], treeItem);
                // }
                treeItem.Collapsed = true;
            }
            else
            {
                GD.PushWarning("Unknown dictionary member: " + _dicEn.Value);
            }
        }
    }

    private TreeItem CreateItemWithIcon(int column, string text, Texture icon,  TreeItem parent = null)
    {
        TreeItem treeItem = CreateItem(parent);
        treeItem.SetText(column, text);
        if (IsInstanceValid(icon)) treeItem.SetIcon(column, icon);
        return treeItem;
    }
    
    private Texture SelectIcon(string _str, TreeItem _parent)
    {   
        Texture selected = icons["Folder"];
        if (!IsPredefined(_str, _parent))
        {
            return selected; 
        }
        
        switch (_str)
        {
            case "Flows":
                selected = icons["Flows"];
                break;
            case "Entities":
                selected = icons["Entities"];
                break;
            case "Assets":
                selected = icons["Assets"];
                break;
            case "References":
                selected = icons["References"];
                break;
            case "Characters":
                selected = icons["Characters"];
                break;
            case "Locations":
                selected = icons["Locations"];
                break;
            default:
                selected = icons["Folder"];
                break;
        }
        return selected;
    }

    private bool IsPredefined(string _str, TreeItem _parent)
    {
        return IsInstanceValid(_parent) 
        && ((_parent == root) 
        ||  (_parent.GetText(0) == "Entities" && (
            _str =="Characters" || _str =="Locations"// || _str ==""
        )));
    }

    public override void _GuiInput(InputEvent inputEvent)
    {
        if (inputEvent is InputEventMouseButton mouseEvent)
        {
            if (mouseEvent.Doubleclick && IsInstanceValid(GetItemAtPosition(mouseEvent.Position)))
            {
                TreeItem treeItem = GetItemAtPosition(mouseEvent.Position);
                string path = PathFromTree(treeItem);
                GD.Print("Double Click: " + treeItem.GetText(0) + " -> \"" + path + "\"");
                StoryManager.CurrentStory.CurrentStoryElement = new StoryElement(StoryManager.CurrentStory.StoryBase + path);
            }
        }
    }

    private string PathFromTree(TreeItem treeItem)
    {
        if (treeItem == root) { return "/"; }
        TreeItem parent = treeItem.GetParent();
        string path = "/" + treeItem.GetText(0) + (treeItem.GetChildren() != null ? "/" : "");
        while (parent != root)
        {
            path =  "/" + parent.GetText(0) + path;
            parent = parent.GetParent();
        }
        if (treeItem.GetChildren() == null) { path += "." + treeItem.GetMeta("Extension"); }
        return path;
    }
}
