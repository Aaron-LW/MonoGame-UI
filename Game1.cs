using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using UI;

namespace MonoGame;

public class Game1 : Game
{
    Texture2D ballTexture;
    Vector2 ballPosition;
    float ballSpeed;

    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private float YVel = 0f;
    private float Gravity = 10f;

    private UIElement testElement;
    private Panel topTest;
    private Panel centerTest;
    private Panel bottomTest;

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
        ballPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);
        ballSpeed = 100f;

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        ballTexture = Content.Load<Texture2D>("ball");

        testElement = new Panel(GraphicsDevice, 100, 100, 200, 150, Color.Transparent, Align.Center);
        topTest = new Panel(GraphicsDevice, 0, 0, 190, 50, Color.Red, Align.TopCenter, testElement);
        centerTest = new Panel(GraphicsDevice, 0, 0, 190, 50, Color.LightGreen, Align.Center, testElement);
        bottomTest = new Panel(GraphicsDevice, 0, 0, 190, 50, Color.Blue, Align.BottomCenter, testElement);
    }
    
    protected override void Update(GameTime gameTime)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        YVel += Gravity;
        
        float updatedBallSpeed = ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        float dYVel = YVel * (float)gameTime.ElapsedGameTime.TotalSeconds;        

        var KeyboardState = Keyboard.GetState();

        ballPosition.Y += dYVel; 
        if (KeyboardState.IsKeyDown(Keys.A)) { ballPosition.X -= updatedBallSpeed; }
        if (KeyboardState.IsKeyDown(Keys.D)) { ballPosition.X += updatedBallSpeed; }

        testElement.LocalPosition.X = Mouse.GetState().Position.X;
        testElement.LocalPosition.Y = Mouse.GetState().Position.Y;

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();
        _spriteBatch.Draw(ballTexture, ballPosition, null, Color.White, 0f, new Vector2(ballTexture.Width / 2, ballTexture.Height / 2), Vector2.One, SpriteEffects.None, 0f);
        
        testElement.Draw(_spriteBatch);
        topTest.Draw(_spriteBatch);
        centerTest.Draw(_spriteBatch);
        bottomTest.Draw(_spriteBatch);
        
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
