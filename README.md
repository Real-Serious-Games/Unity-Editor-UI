# UnityEditorUI

A wrapper around the Unity editor GUI system for constructing editor windows using a fluent API instead of Unity's `OnGUI` functions. Editor GUI widgets can be bound to properties and methods in other classes to create GUIs that implement the MVVM pattern.

## Constructing editor windows

Set up a simple editor window with a label and a button:

```
// Create an instance of the view you want to bind the UI to
var viewModel = new ExampleView();

// Create the UI
var gui = new UnityEditorUI.GUI();
gui.Root()
    .Label()
        .Text.Value("My new editor window")
    .End()
    .Button()
        .Text.Value("Do something!")
        .Click.Bind(() => viewModel.DoSomething())
        .Tooltip.Value("Click to trigger an event")
    .End()
    
// Bind the UI to the view
gui.BindViewModel(viewModel);
```

And then render and update it by adding the following line to your editor window's existing `OnGUI()` method:
```
gui.OnGUI();
```

Every property on a GUI widget can have its value set to a constant value using `.Value()`, or bound to another property using `.Bind()`. If the class being bound to implements `INotifyPropertyChanged`, this will set up a two way data binding, so that the underlying view gets updated when the UI changes and vice-versa.

## Examples
The project in the `Examples` directory has been tested with Unity 5.2.3p2 and should contain everything you need to load up and run the examples. Since this library is purely for Unity editor extensions, there is no scene included in the project.

### Example 1
This example shows the most basic sample of a use case for the UnityEditorUI system, binding a Unity editor window to a simple view class but not subscribing to property changed events.

### Example 2
This example demonstrates how to set up your view class so that, when bound to a UI, property changed events will be passed on to the UI and the UI will update itself accordingly, as well as the properties in the view being updated when valeus are changed by the user via the UI.

## Widgets
### Button
Clickable push button widget.
#### Bindable properties
- Text : `string`
- Tooltip : `string`
- Width : `int`
- Height : `int`

#### Bindable events
- Click
     
### Checkbox
Boolean check box widget.
#### Bindable properties 
- Checked : `bool`
- Label : `string`

### DateTimePicker
Widget for entering a date and time. Essentially a TextBox with date validation on it.
#### Bindable properties
- Date : `DateTime`
- Width : `int`
- Height : `int`

### DropdownBox
Drop-down selection field. Labels for individual items are set by calling the bound object's `ToString()` method.
#### Bindable properties
- SelectedItem : `object`
- Items : `object[]`
- Label : `string`
- Tooltip : `string`

### Label
Widget for displaying read-only text.
#### Bindable properties
- Text : `string`
- Tooltip : `string`
- Bold : `bool`
- Width : `int`
- Height : `int`

### LayerPicker
Widget for selecting Unity layers.
#### Bindable properties
- Label : `string`
- SelectedLayers : `string[]`

### Spacer
Inserts a space between other widgets

### TextBox
Widget for entering text
#### Bindable properties
- Text : `string`
- Width : `int`
- Height : `int`

### Vector3Field
Widget for entering vectors with X, Y and Z coordinates.
#### Bindable properties
- Label : `string`
- Tooltip : `string`
- Vector : `Vector3`

## Layouts
### RootLayout
### HorizontalLayout
### VerticalLayout


