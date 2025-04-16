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

    private Panel MainElement;
    private Panel MainElementHeader;

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

        MainElement = UIutils.CreatePanel(GraphicsDevice, 100, 200, 175, 200, Color.DarkGray, Align.Center);
        MainElementHeader = UIutils.CreatePanel(GraphicsDevice, 0, 0, 175, 30, Color.DimGray, Align.TopCenter, MainElement);
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

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();
        _spriteBatch.Draw(ballTexture, ballPosition, null, Color.White, 0f, new Vector2(ballTexture.Width / 2, ballTexture.Height / 2), Vector2.One, SpriteEffects.None, 0f);
        UIutils.DrawUIElements(_spriteBatch);
        
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
