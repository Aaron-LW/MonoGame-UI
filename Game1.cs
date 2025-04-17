using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using UI;

namespace MonoGame;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Panel MainElement;
    private Panel MainElementHeader;

    private Text testText;
    
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

        MainElement = UIutils.CreatePanel(GraphicsDevice, 100, 200, 175, 200, Color.DarkGray, Align.Center);
        MainElementHeader = UIutils.CreatePanel(GraphicsDevice, 0, 0, 175, 25, Color.DimGray, Align.TopCenter, MainElement);

        testText = UIutils.CreateText(GraphicsDevice, 0, 0, "Test", font, Color.White, 1f, Align.Center, MainElementHeader);
    }
    
    protected override void Update(GameTime gameTime)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();
        UIutils.DrawUIElements(_spriteBatch);
        
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
