using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Runtime.Serialization.Formatters;
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
    private List<Panel> ListElements = new List<Panel>();
    private List<Text> ListTexts = new List<Text>();

    private List<Task> Tasks = new List<Task>();
    
    private SpriteFont font;

    private int counter;

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
        Tasks.Add(new Task("Live"));
        Tasks.Add(new Task("Test"));
        Tasks.Add(new Task("dede"));
    
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        font = Content.Load<SpriteFont>("font");

        MainHeader = UIutils.CreatePanel(GraphicsDevice, _graphics.PreferredBackBufferWidth / 2, 0, _graphics.PreferredBackBufferWidth, 60, Color.DimGray, Align.TopCenter);

        UpdateTaskList();
    }
    
    protected override void Update(GameTime gameTime)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();


        if (Keyboard.GetState().IsKeyDown(Keys.F) && Tasks.Count < 5) 
        {
            Task remove = Tasks[0];
            Tasks.Remove(remove);
            remove = null;           
            
            Tasks.Add(new Task(counter.ToString()));
            counter++;

            GC.Collect();

            UpdateTaskList();
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Gray);

        _spriteBatch.Begin();
        UIutils.DrawUIElements(_spriteBatch);
        
        _spriteBatch.End();

        base.Draw(gameTime);
    }
    
    private void UpdateTaskList() 
    {
        ListElements.Clear();
        ListTexts.Clear();
        
        foreach (Task task in Tasks) 
        {
            ListElements.Add(UIutils.CreatePanel(GraphicsDevice, 0, (60 * ListElements.Count) + 60, 600, 50, Color.DarkGray, Align.None, null, true));
            ListTexts.Add(UIutils.CreateText(GraphicsDevice, 10, 0, task.Name, font, Color.White, 1f, Align.LeftCenter, ListElements[ListElements.Count - 1]));
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