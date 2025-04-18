# MonoGame UI
 A script for making UIs with MonoGame

 **How to Install**
- Go to releases
- Download the UI.cs file
- put the file somewhere in your project folder
- In your main file or just anywhere add `using UI` at the top
- you can now start using the script

**How to use** <br>
Making a simple Panel:
- First you make a reference to the Panel by using `private Panel myPanel`
- Now you'll go to LoadContent and write `myPanel = UIutils.CreatePanel(graphicsDevice, x, y, width, height, color, align, parent, visible)` <br>






Making Text: <br>
- Make a reference using `private Text myText` <br>
- Go to LoadContent and write `myText = UIutils.CreateText(graphicsDevice, x, y, text, font, color, scale, align, parent, visible, rotation, origin)`
- To add a font you need to go to the mgcb editor and add a .spritebatch file, you then make a reference to that file by using `private SpriteFont font`
- Then in LoadContent you use `Content.Load<SpriteFont>("name of the file")

To draw everything you'll have to use `DrawUIElements(SpriteBatch)` in the Draw function

**Alignments**
- TopCenter
- Center
- BottomCenter
- LeftCenter
