# MonoGame UI
 A script for making UIs with MonoGame

 **How to Install**
- Go to releases
- Download the UI.cs file
- put the file somewhere in your project folder
- In your main file or just anywhere add `using UI` at the top
- you can now start using the script

**How to use**
- to add a basic panel you first make a reference to the panel with `Panel myPanel`
- in LoadContent add `myPanel = new Panel(GraphicsDevice, x, y, width, height, Color, Alignment(optional), Parent(optional))`
- in the Draw function add `myPanel.Draw(_spriteBatch)`
- Now you'll have a rectangle

**Alignments**
- TopCenter (top of itself or the parent object)
- Center (center of itself or the parent object)
- BottomCenter (bottom of itself or the parent object)
