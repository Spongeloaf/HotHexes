using GdCore;
using HotHexes.addons.HexTools;
using GdCore.Extensions;

#if TOOLS

[Tool]
public partial class HexToolPlugin : EditorPlugin
{
    private PopupPanel _popup;
    private const string hexToolsLabel = "Hex Tools...";
    private EditorInterface? editorIndterface;

public override void _EnterTree()
	{
		// Create the popup dialog
        _popup = new PopupPanel();
        AddChild(_popup);
        _popup.Title = "Hex Tools";
        BuildPopupWindow(_popup);
        ;

        // Add a menu item to Project > Tools
        AddToolMenuItem(hexToolsLabel, Callable.From(OpenPopup));
	}

    public override void _ExitTree()
    {
        // Remove the menu item
        RemoveToolMenuItem(hexToolsLabel);

        // Clean up the popup
        if (_popup != null)
        {
            _popup.QueueFree();
            _popup = null;
        }
    }

    private void OpenPopup()
    {
        // Show the popup centered in the editor
        _popup.PopupCentered(new Vector2I(300, 200));
    }

    private void OnSubmitButtonPressed()
    {
        Node root = EditorInterface.Singleton?.GetEditedSceneRoot();
        if (root is null)
        {
            GD.PrintErr("HexToolPlugin: Scene tree or editor interface not found.");
            return;
        }

        GD.Print($"Root name : {root.Name}");
        root.PrintTreePretty();
        HexTools.GenerateHexGrid(root, new HexGridParameters());
        GD.Print("Hex tools finished;");
        root.Name = "hexy boy";
        _popup.Hide(); // Close the popup after submission
    }

    private void BuildPopupWindow(PopupPanel _popup)
    {
        // Create a vertical container for controls
        var vbox = new VBoxContainer();
        _popup.AddChild(vbox);

        // Add a label
        var _label = new Label();
        _label.Text = "Click to generate hexes";
        vbox.AddChild(_label);

        // Add a submit button
        var _submitButton = new Button();
        _submitButton.Text = "Generate!";
        _submitButton.Pressed += OnSubmitButtonPressed;
        vbox.AddChild(_submitButton);
    }
}
#endif
