﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PuzzleEngineAlpha.Scene.Editor.Menu
{
    using Components;

    class MainMenu : IScene
    {

        #region Declarations

        List<AGUIComponent> components;
        Texture2D backGround;

        #endregion

        #region Constructor

        public MainMenu(ContentManager Content,MenuHandler menuHandler)
        {
            components = new List<AGUIComponent>();
            InitializeGUI(Content, menuHandler);
            backGround = Content.Load<Texture2D>(@"textures/whiteRectangle");

            Resolution.ResolutionHandler.Changed += ResetSizes;

        }

        #endregion

        #region Handle Resolution Change

        void ResetSizes(object sender, EventArgs e)
        {
            for (int i = 0; i < components.Count; i++)
            {
                components[i].Position = this.Location + new Vector2(ButtonOffSet, (ButtonSize.Y + ButtonOffSet) * i);
                components[i].GeneralArea = this.MenuRectangle;
            }
        }

        #endregion    

        #region Helper Methods

        public void GoInactive()
        {
            isActive = false;
        }

        #endregion

        #region Properties

        Vector2 ButtonSize
        {
            get
            {
                return new Vector2(160, 100);
            }
        }

        float ButtonOffSet
        {
            get
            {
                return 5.0f;
            }
        }

        Vector2 Size
        {
            get
            {
                return new Vector2(ButtonSize.X + ButtonOffSet * 2, 5 * (ButtonSize.Y + ButtonOffSet));
            }

        }

        Vector2 Location
        {
            get
            {
                return new Vector2(Resolution.ResolutionHandler.WindowWidth / 2 - Size.X / 2, Resolution.ResolutionHandler.WindowHeight / 2 - Size.Y / 2);
            }
           
        }

        Rectangle MenuRectangle
        {
            get
            {
                return new Rectangle((int)Location.X, (int)Location.Y, (int)Size.X, (int)Size.Y);
            }
        }

        bool isActive;
        public bool IsActive
        {
            get
            {
                return isActive;
            }
            set
            {
                isActive = value;
            }
        }

        #endregion

        #region Initialize GUI

        void InitializeGUI(ContentManager Content, MenuHandler menuHandler)
        {
            DrawProperties button = new DrawProperties(Content.Load<Texture2D>(@"Buttons/button"), 0.9f, 1.0f, 0.0f, Color.White);
            DrawProperties frame = new DrawProperties(Content.Load<Texture2D>(@"Buttons/frame"), 0.8f, 1.0f, 0.0f, Color.White);
            DrawProperties clickedButton = new DrawProperties(Content.Load<Texture2D>(@"Buttons/clickedButton"), 0.8f, 1.0f, 0.0f, Color.White);
            DrawTextProperties textProperties = new DrawTextProperties("editor", 11, Content.Load<SpriteFont>(@"Fonts/menuButtonFont"), Color.Black, 1.0f, 1.0f);

            components.Add(new Components.Buttons.MenuButton(button, frame, clickedButton, textProperties, Location + new Vector2(ButtonOffSet, ButtonOffSet),ButtonSize, this.MenuRectangle));

            textProperties.text = "new";
            components.Add(new Components.Buttons.MenuButton(button, frame, clickedButton, textProperties, Location + new Vector2(ButtonOffSet, ButtonSize.Y + ButtonOffSet), ButtonSize, this.MenuRectangle));
            components[1].StoreAndExecuteOnMouseRelease(new Actions.SwapWindowAction(menuHandler, "newMap"));
            textProperties.text = "load";
            components.Add(new Components.Buttons.MenuButton(button, frame, clickedButton, textProperties, Location + new Vector2(ButtonOffSet, (ButtonSize.Y + ButtonOffSet) * 2), ButtonSize, this.MenuRectangle));

            textProperties.text = "save";
            components.Add(new Components.Buttons.MenuButton(button, frame, clickedButton, textProperties, Location +  new Vector2(ButtonOffSet, (ButtonSize.Y + ButtonOffSet) * 3), ButtonSize, this.MenuRectangle));

            textProperties.text = "settings";
            components.Add(new Components.Buttons.MenuButton(button, frame, clickedButton, textProperties, Location +  new Vector2(ButtonOffSet, (ButtonSize.Y + ButtonOffSet) * 4), ButtonSize, this.MenuRectangle));
        }

        #endregion

        public void Update(GameTime gameTime)
        {
            foreach (AGUIComponent component in components)
            {
                component.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
                                   
            foreach (AGUIComponent component in components)
            {
                component.Draw(spriteBatch);
            }

            spriteBatch.Draw(backGround, MenuRectangle, null, Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 1.0f);

        }


    }
}
