using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using UI;

namespace MonoGame;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Panel MainHeader;
    
    private Button TestButton;
    private Text ButtonText;

    private Button RemoveButton;
    private Text RemoveButtonText;
    
    private List<Panel> ListElements = new List<Panel>();
    private List<Text> ListTexts = new List<Text>();

    private List<Task> Tasks = new List<Task>();

    private SpriteFont font;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";

        //_graphics.PreferredBackBufferWidth = 1280;
        //_graphics.PreferredBackBufferHeight = 720;
        //_graphics.IsFullScreen = true;
        //_graphics.ApplyChanges();
        
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        font = Content.Load<SpriteFont>("font");

        MainHeader = UIutils.CreatePanel(GraphicsDevice, _graphics.PreferredBackBufferWidth / 2, 0, _graphics.PreferredBackBufferWidth, 60, UIutils.ColorFromHex("#383838"), Align.TopCenter);
        
        TestButton = UIutils.CreateButton(GraphicsDevice, 5, 0, 150, 50, UIutils.ColorFromHex("#484848"), AddTask, Align.LeftCenter, MainHeader);
        ButtonText = UIutils.CreateText(GraphicsDevice, 0, 0, "Button", font, Color.White, 1f, Align.Center, TestButton);
        
        RemoveButton = UIutils.CreateButton(GraphicsDevice, 155, 0, 150, 50, UIutils.ColorFromHex("#484848"), RemoveTask, Align.LeftCenter, TestButton);
        RemoveButtonText = UIutils.CreateText(GraphicsDevice, 0, 0, "Remove", font, Color.White, 1f, Align.Center, RemoveButton);
    }

    MouseState previousMouseState = Mouse.GetState();
    protected override void Update(GameTime gameTime)
    {
        MouseState mouseState = Mouse.GetState();
    
        if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        if (mouseState.LeftButton == ButtonState.Released && previousMouseState.LeftButton == ButtonState.Pressed) 
        {
            UIutils.TickButtons();
        }

        previousMouseState = mouseState;
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(UIutils.ColorFromHex("#282828"));

        _spriteBatch.Begin();
        UIutils.DrawUIElements(_spriteBatch);
        
        _spriteBatch.End();

        base.Draw(gameTime);
    }
    
    private void AddTask() 
    {
        Tasks.Add(new Task("ClickTask"));
        
        ListElements.Add(UIutils.CreatePanel(GraphicsDevice, 0, (60 * ListElements.Count) + 70, 600, 50, UIutils.ColorFromHex("#484848"), Align.None, null, true));
        ListTexts.Add(UIutils.CreateText(GraphicsDevice, 10, 0, Tasks[Tasks.Count - 1].Name, font, Color.White, 1f, Align.LeftCenter, ListElements[ListElements.Count - 1]));
    }
    
   private void RemoveTask() 
    {
        if (ListElements.Count > 0 && ListTexts.Count > 0) 
        {
            var lastPanel = ListElements[ListElements.Count - 1];
            var lastText = ListTexts[ListTexts.Count - 1];

            UIutils.RemoveUIElement(lastPanel);
            UIutils.RemoveUIElement(lastText);

            ListElements.RemoveAt(ListElements.Count - 1);
            ListTexts.RemoveAt(ListTexts.Count - 1);

            Tasks.RemoveAt(Tasks.Count - 1);
        }
    }
}

public class Task 
{
    public string Name;
    public bool Done;
    
    public Task (string name, bool done = false) 
    {
        Name = name;
        Done = done;
    }
}