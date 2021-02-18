using Godot;
using System;
using Storier;

public class MenuToolBar : HBoxContainer
{
    // Exports
    [Export]
    private NodePath editorNodePath = new NodePath("./Editor");
    [Export]
    private NodePath navigatorNodePath = new NodePath("./Navigator");

    // Publics
    public Button editorButton;
    public Button navigatorButton;

    public override void _Ready()
    {
        editorButton = GetNode<Button>(editorNodePath);
        navigatorButton = GetNode<Button>(navigatorNodePath);
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

    public void _OnNavigatorButtonDown()
    {
        // var node = navigatorModalRes.Instance();
        // AddChild(node);
        ModalManager.modals.ShowNavigatorModal();
    }
}
