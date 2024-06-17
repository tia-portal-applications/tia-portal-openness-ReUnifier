# ReUnifier

A DB-like query replacer for TIA Portal projects. 

Easily search for specific items and add, delete and modify properties on that item.


## Build this project

Open "ReUnifier.sln" in Visual Studio and simply build the project.


## Run the app

### Import

![Import project](./img/04.png)

Select a project from a running TIA Portal instance and click on the "Connect" button.


### Operation
![Operation](./img/03.png)

Choose the operation you want to do.

You can choose between updating screen items, screen properties, tags etc to creating new screen items, tags, alarms etc.


### Where

![Where](./img/02.png)

Choose the Query parameters.

You can choose between using a name with RegEx and selecting the definitive item type for screen items.

I.E. use Name Textfeld And Type = HmiTextBox to change every Textbox with String "Textfeld" in the item name.


### Set

![Set](./img/01.png)

Set properties and values.

I.E. set Left = 10 to change the property "Left" value of all items selected in "Where" to 10.

You can change basically all properties, including colors and fonts.


### Execute

Click on the "Execute" button to apply your changes to the TIA Portal project!


## Usecase "Custom Scaling"

![Operation](./img/08.png)

To update items in all screens, use the Regular Expression ".".


![Where](./img/07.png)

To update all items just use an empty name String.


![Set](./img/06.png)

Use the factor X for scaling on the horizontal axis. This factor (1.3 in this example) has to be applied to the properties "Left" and "Width".
Caution: Use 1,3 on Windows with german locale.

Use the factor Y for scaling on the vertical axis. This factor (1.12 in this example) has to be applied to the properties "Top" and "Height".
Caution: Use 1,12 on Windows with german locale.


![Set](./img/05.png)

Check the operation you are about to execute.

Execute and wait for the operation to take place.
