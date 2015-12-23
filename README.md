# UnityEditorUI

A wrapper around the Unity editor GUI system for constructing editor windows using a fluent API instead of Unity's `OnGUI` functions. Editor GUI widgets can be bound to properties and methods in other classes to create GUIs that implement the MVVM pattern.

## Constructing editor windows

Set up a simple editor window with a label and a button:

```
// Construct the GUI
var gui = new UnityEditorUI.GUI();
gui.Root()
    .Label()
        .Text.Value("My new editor window")
    .End()
    .Button()
        .Text.Value("Do something!")
        .Click.Bind(() => Debug.Log("Button clicked"))
        .Tooltip.Value("Click to trigger an event")
    .End()
```

And then render and update it by adding the following line to your editor window's existing `OnGUI()` method:
```
gui.OnGUI();
```

Every property on a GUI widget can have its value set to a constant value using `.Value()`, or bound to another property using `.Bind()`. If the class being bound to implements `INotifyPropertyChanged`, this will set up a two way data binding, so that the underlying view gets updated when the UI changes and vice-versa.


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


