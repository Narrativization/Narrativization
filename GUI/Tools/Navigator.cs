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

    public void CreateTree(Dictionary _dictionary, string rootName)
    {
        Clear();
        root = CreateItemWithIcon(0, rootName, icons["Folder"], null);
        CreateTreeDisplay(_dictionary, root); 
    }

    private void CreateTreeDisplay(Dictionary _dictionary, TreeItem _parent = null)
    {
        IDictionaryEnumerator _dicEn = _dictionary.GetEnumerator();
        for (int i = 0; i < _dictionary.Count; i++)
        {
            _dicEn.MoveNext();
            if (_dicEn.Key is string && _dicEn.Value is string)
            {
                if (IsInstanceValid(_parent) && !((_parent == root) && (_parent.GetText(0) == (string)_dicEn.Key))) // Ignore Project(.storier) File
                {
                    TreeItem treeItem = CreateItemWithIcon(0, (string)_dicEn.Key, icons["File"], _parent);
                }
            }
            else if (_dicEn.Key is string && _dicEn.Value is Dictionary)
            {
                TreeItem treeItem = CreateItemWithIcon(0, (string)_dicEn.Key, SelectIcon((string)_dicEn.Key, _parent), _parent);
                treeItem.Set("Directory", _dicEn.Value);
                GD.Print(_dicEn.Value);
                CreateTreeDisplay((Dictionary)_dicEn.Value, treeItem);

                if (!IsInstanceValid(treeItem.GetChildren()))
                {
                    TreeItem treeChild = CreateItemWithIcon(0, "> Empty", icons["XSquare"], treeItem);
                }
                treeItem.Collapsed = true;
            }
            else
            {
                GD.PushWarning("Unknown dictionary member: " + _dicEn.Value);
            }
        }
    }

    private TreeItem CreateItemWithIcon(int _column, string _str, Texture _icon,  TreeItem _parent = null)
    {
        TreeItem treeItem = CreateItem(_parent);
        treeItem.SetText(_column, _str);
        if (IsInstanceValid(_icon)) treeItem.SetIcon(_column, _icon);
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
                GD.Print("Double Click: " + GetItemAtPosition(mouseEvent.Position).GetText(0));
            }
        }
    }
}
