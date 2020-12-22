using Godot;
using System;
using System.Collections;
using Godot.Collections;
using Storier;

public class NavigationTree : Tree
{
    [Export]
    private Texture folderIcon;
    


    public override void _Ready()
    {
        GetNode<WindowDialog>("../").Show();
        Refresh(StoryManager.story.LookHierarchy(), StoryManager.story.storyData.name);
    }

//  public override void _Process(float delta)
//  {
//      
//  }

    public void Refresh(Dictionary _dictionary, string rootName)
    {
        GD.Print(_dictionary);
        Clear();
        TreeItem root = CreateItem();
        root.SetText(0, rootName);
        if (IsInstanceValid(folderIcon))
        {
            root.SetIcon(0, folderIcon);
        }
        CreateTree(_dictionary, root); 
        
    }

    private void CreateTree(Dictionary _dictionary, TreeItem _parent)
    {
        IDictionaryEnumerator _dicEn = _dictionary.GetEnumerator();
        for (int i = 0; i < _dictionary.Count; i++)
        {
            _dicEn.MoveNext();
            GD.Print(_dicEn.Key + " : " + _dicEn.Value);
            if (_dicEn.Key is string && _dicEn.Value is string)
            {
                TreeItem treeItem = CreateItem(_parent);
                treeItem.SetText(0, (string)_dicEn.Key);
            }
            else if (_dicEn.Key is string && _dicEn.Value is Dictionary)
            {
                TreeItem treeItem = CreateItem(_parent);
                treeItem.SetText(0, (string)_dicEn.Key + "");
                if (IsInstanceValid(folderIcon))
                {
                    treeItem.SetIcon(0, folderIcon);
                }
                CreateTree((Dictionary)_dicEn.Value, treeItem);
                if (!IsInstanceValid(treeItem.GetChildren()))
                {
                    TreeItem treeChild = CreateItem(treeItem);
                    treeChild.SetText(0, "> Empty");
                }
                treeItem.Collapsed = true;
            }
            else
            {
                GD.PushWarning("Unknown dictionary member: " + _dicEn.Value);
            }
        }
    }


}
