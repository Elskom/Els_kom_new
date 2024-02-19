// Copyright (c) 2014-2024, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom.Forms
{
    using Els_kom.Controls;

    internal partial class GameSelectionForm : ThemedForm
    {
        private readonly List<Button> buttons = new();

        public GameSelectionForm()
            => this.InitializeComponent();

#if NEVER_DEFINED
        public bool ExecuteGame { get; set; } = false;

        private void OnGrandChaseInstanceButtonClick()
        {
            // TODO: Imeplement this and use this with dynamically created buttons.
            if (this.ExecuteGame)
            {
                // TODO: Execute the game.
            }
            else
            {
                // TODO: Do some sort of work then.
            }
        }

        private void OnElswordInstanceButtonClick()
        {
            // TODO: Imeplement this and use this with dynamically created buttons.
            if (this.ExecuteGame)
            {
                // TODO: Execute the game.
            }
            else
            {
                // TODO: Do some sort of work then.
            }
        }
#endif

        private void GameSelectionForm_Load(object sender, System.EventArgs e)
        {
#if NEVER_DEFINED
            SettingsFile.SettingsJson = SettingsFile.SettingsJson?.ReopenFile();

            // we need to pad this by 6 * 3. (per control that is.
            var padding = 6;

            // TODO: Do stuff here.
            var gamedirs = SettingsFile.Settingsxml?.TryRead("GameDirs", "dir", null).ToList();
            if (gamedirs.Count <= 4)
            {
                // expand the form until there are 4 or more. And relative to that value as well.
                this.Panel1.Height = (57 * gamedirs.Count) + (padding * 3);  // basically only show the controls based on the # in the list.
                this.Height = this.Panel1.Height + (padding + 3); // this is really padded by 9.
            }

            foreach (var gamedir in gamedirs)
            {
                // TODO: Identify if voidels or not.
                var btn = new Button
                {
                    ImeMode = ImeMode.NoControl,
                    Location = new Point(0, this.buttons[gamedirs.IndexOf(gamedir) - 1].Location.Y + padding),
                    Size = new Size(250, 57),
                    TabIndex = this.buttons[gamedirs.IndexOf(gamedir) - 1].TabIndex + 1,
                    TextImageRelation = TextImageRelation.ImageBeforeText,
                    Image = Icons.VoidElsLogo,
                };

                btn.Text = btn.Image == Icons.VoidElsLogo ? "Void Elsword (%s)".Replace("%s", gamedir) : "Elsword (%s)".Replace("%s", gamedir);
                btn.Click += (object sender, EventArgs e) =>
                {
                    if (btn.Image == Icons.VoidElsLogo || btn.Image == Icons.ElsLogo)
                    {
                        this.OnElswordInstanceButtonClick();
                    }
                    else
                    {
                        this.OnGrandChaseInstanceButtonClick();
                    }
                };
                btn.Name = "Button1".Replace("1", (gamedirs.IndexOf(gamedir) + 1).ToString());
                btn.UseVisualStyleBackColor = true;

                this.buttons.Add(btn);
            }
#else
            _ = string.Equals(null, null, StringComparison.Ordinal);

            // throw for now.
            throw new NotSupportedException("Not finished yet.");
#endif
        }
    }
}
