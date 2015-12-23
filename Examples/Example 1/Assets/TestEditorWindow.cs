using UnityEngine;
using UnityEditor;
using UnityEditorUI;

/// <summary>
/// An example of a simple editor window.
/// </summary>
class TestEditorWindow : EditorWindow
{
    /// <summary>
    /// Our GUI object.
    /// </summary>
    private UnityEditorUI.GUI gui;
    
    /// <summary>
    /// Allow the window to be opened via a menu item.
    /// </summary>
    [MenuItem("Example/Editor window")]
    public static void ShowWindow()
    {
        var window = (TestEditorWindow) EditorWindow.GetWindow<TestEditorWindow>(false, "Editor window");
        
        if (window.gui == null)
        {
            window.SetUpGUI();
        }
        window.Show();
    }
    
    /// <summary>
    /// Set up the GUI.
    /// </summary>
    private void SetUpGUI()
    {
        // First, create an instance of the view we want to bind the GUI to
        var viewModel = new ExampleView();
        
        // Set up the GUI widgets
        gui = new UnityEditorUI.GUI();
        gui.Root()
            .Label()
                .Text.Value("My new editor window")
            .End()
            .Button()
                .Text.Value("Do something!")
                .Click.Bind(() => viewModel.DoSomething())
                .Tooltip.Value("Click to trigger an event")
            .End();
            
        // Bind the resulting GUI to the view.
        gui.BindViewModel(viewModel);
    }
    
    void OnGUI()
    {
        // Calling OnGUI on the root will update the whole GUI.
        gui.OnGUI();
    }
    
    /// <summary>
    /// A simple view to bind our GUI to.
    /// </summary>
    private class ExampleView
    {
        // Properties and events to bind to go here
        public void DoSomething()
        {
            Debug.Log("Event triggered!");
        }
    }
}