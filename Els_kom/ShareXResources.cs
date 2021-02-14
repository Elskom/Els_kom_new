// Copyright (c) 2014-2021, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Drawing;
    using System.Windows.Forms;
    using Els_kom.Controls;
    using Elskom.Generic.Libs;

    public static class ShareXResources
    {
        private static readonly List<Button> HoveredButtonList = new();
        private static readonly List<Button> FocusedButtonList = new();

        public static Icon Icon => Icons.FormIcon;

        [SuppressMessage("IDisposableAnalyzers.Correctness", "IDISP012:Property should not return created disposable.", Justification = "Needed.")]
        public static Image Logo => Icons.FormImage;

        public static ShareXTheme Theme { get; set; } = new ShareXTheme();

        public static void ApplyTheme(Form form)
            => ApplyTheme(form, true);

        public static void ApplyTheme(Form form, bool setIcon)
        {
            if (setIcon)
            {
                form.Icon = Icon;
            }

            ApplyDarkThemeToControl(form);
            if (form.IsHandleCreated)
            {
                _ = NativeMethods.UseImmersiveDarkMode(form.Handle, true);
            }
            else
            {
                form.HandleCreated += (s, e) => NativeMethods.UseImmersiveDarkMode(form.Handle, true);
            }
        }

        internal static void ApplyDarkThemeToControl(Control control)
            => InternalApplyDarkThemeToControl(control);

        private static void InternalApplyDarkThemeToControl(Control control)
        {
            if (control.ContextMenuStrip != null)
            {
                control.ContextMenuStrip.Renderer = new ToolStripDarkRenderer();
            }

            if (control is MenuButton mb && mb.Menu != null)
            {
                mb.Menu.Renderer = new ToolStripDarkRenderer();
            }

            switch (control)
            {
                case MessageManager mm:
                {
                    mm.ContextMenuStrip.Renderer = new ToolStripDarkRenderer();
                    return;
                }

                case Button btn:
                {
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderColor = Theme.BorderColor;
                    btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(62, 68, 78);
                    btn.ForeColor = Theme.TextColor;
                    btn.BackColor = Theme.LightBackgroundColor;
                    btn.Paint += new PaintEventHandler(Btn_Paint);
                    btn.MouseEnter += Btn_MouseEnter;
                    btn.MouseLeave += Btn_MouseLeave;
                    return;
                }

                case CheckBox cb when cb.Appearance == Appearance.Button:
                {
                    cb.FlatStyle = FlatStyle.Flat;
                    cb.FlatAppearance.BorderColor = Theme.BorderColor;
                    cb.ForeColor = Theme.TextColor;
                    cb.BackColor = Theme.LightBackgroundColor;
                    return;
                }

                case TextBox tb:
                {
                    tb.ForeColor = Theme.TextColor;
                    tb.BackColor = Theme.LightBackgroundColor;
                    tb.BorderStyle = BorderStyle.FixedSingle;

                    // change text box boarder.
                    NativeMethods.MARGINS margins = default;
                    _ = NativeMethods.DwmExtendFrameIntoClientArea(tb.Handle, ref margins);

                    // hopefully this is correct.
                    var boarderRect = new Rectangle(0, 0, margins.rightWidth, margins.bottomHeight);
                    if (boarderRect.Width > 0 && boarderRect.Height > 0)
                    {
                        using var pen1 = new Pen(Theme.BorderColor);
                        using var g = tb.CreateGraphics();
                        g.DrawRectangle(pen1, boarderRect);
                    }

                    return;
                }

                case ComboBox cb:
                {
                    cb.FlatStyle = FlatStyle.Flat;
                    cb.ForeColor = Theme.TextColor;
                    cb.BackColor = Theme.LightBackgroundColor;
                    return;
                }

                case ListBox lb:
                {
                    lb.ForeColor = Theme.TextColor;
                    lb.BackColor = Theme.LightBackgroundColor;
                    return;
                }

                case ThemedListView lv:
                {
                    lv.ForeColor = Theme.TextColor;
                    lv.BackColor = Theme.LightBackgroundColor;
                    lv.FillLastColumn();
                    return;
                }

                case SplitContainer sc:
                {
                    sc.Panel1.BackColor = Theme.BackgroundColor;
                    sc.Panel2.BackColor = Theme.BackgroundColor;
                    break;
                }

                case PropertyGrid pg:
                {
                    pg.CategoryForeColor = Theme.TextColor;
                    pg.CategorySplitterColor = Theme.BackgroundColor;
                    pg.LineColor = Theme.BackgroundColor;
                    pg.SelectedItemWithFocusForeColor = Theme.BackgroundColor;
                    pg.SelectedItemWithFocusBackColor = Theme.TextColor;
                    pg.ViewForeColor = Theme.TextColor;
                    pg.ViewBackColor = Theme.LightBackgroundColor;
                    pg.ViewBorderColor = Theme.BorderColor;
                    pg.HelpForeColor = Theme.TextColor;
                    pg.HelpBackColor = Theme.BackgroundColor;
                    pg.HelpBorderColor = Theme.BorderColor;
                    return;
                }

                case DataGridView dgv:
                {
                    dgv.BackgroundColor = Theme.LightBackgroundColor;
                    dgv.GridColor = Theme.BorderColor;
                    dgv.DefaultCellStyle.BackColor = Theme.LightBackgroundColor;
                    dgv.DefaultCellStyle.SelectionBackColor = Theme.LightBackgroundColor;
                    dgv.DefaultCellStyle.ForeColor = Theme.TextColor;
                    dgv.DefaultCellStyle.SelectionForeColor = Theme.TextColor;
                    dgv.ColumnHeadersDefaultCellStyle.BackColor = Theme.BackgroundColor;
                    dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = Theme.BackgroundColor;
                    dgv.ColumnHeadersDefaultCellStyle.ForeColor = Theme.TextColor;
                    dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor = Theme.TextColor;
                    dgv.EnableHeadersVisualStyles = false;
                    break;
                }

                case ToolStrip ts:
                {
                    ts.Renderer = new ToolStripDarkRenderer();
                    ApplyDarkThemeToToolStripItemCollection(ts.Items);
                    return;
                }

                case LinkLabel ll:
                {
                    ll.LinkColor = Theme.LinkColor;
                    break;
                }

                case Label lbl:
                {
                    lbl.BackColor = Color.Transparent;
                    return;
                }

                case GroupBox gb:
                {
                    gb.ForeColor = Theme.TextColor;
                    gb.BackColor = Theme.BackgroundColor;
                    gb.Paint += new PaintEventHandler(Gb_Paint);
                    break;
                }

                case PictureBox pb when pb.Name == "picIcon":
                {
                    pb.Image = Logo;
                    break;
                }

                default:
                    // nothing to do.
                    break;
            }

            control.ForeColor = Theme.TextColor;
            control.BackColor = Theme.BackgroundColor;
            foreach (Control child in control.Controls)
            {
                ApplyDarkThemeToControl(child);
            }
        }

        private static void Btn_MouseEnter(object sender, System.EventArgs e)
        {
            HoveredButtonList.Clear();
            HoveredButtonList.Add((Button)sender);
        }

        private static void Btn_MouseLeave(object sender, System.EventArgs e)
            => HoveredButtonList.Clear();

        [SuppressMessage("IDisposableAnalyzers.Correctness", "IDISP004:Don't ignore return value of type IDisposable.", Justification = "Disposing would close the form the button is on.")]
        private static void Btn_Paint(object sender, PaintEventArgs e)
        {
            var btn = (Button)sender;
            var g = e.Graphics;
            if (btn.Focused)
            {
                FocusedButtonList.Clear();
                FocusedButtonList.Add(btn);
            }

            if (HoveredButtonList.Contains(btn))
            {
                g.Clear(btn.FlatAppearance.MouseOverBackColor);
            }
            else
            {
                g.Clear(btn.BackColor);
            }

            using (var pen1 = new Pen(btn.FlatAppearance.BorderColor))
            {
                g.DrawRectangle(
                    pen1,
                    btn.FlatAppearance.BorderSize - 1,
                    btn.FlatAppearance.BorderSize - 1,
                    btn.ClientSize.Width - btn.FlatAppearance.BorderSize,
                    btn.ClientSize.Height - btn.FlatAppearance.BorderSize);

                // ensure this is drawn even if the form does not have focus.
                // to ensure we do not break things like how the normal one would look.
                if ((btn.Focused || !btn.FindForm().Focused) && FocusedButtonList.Contains(btn))
                {
                    g.DrawRectangle(
                        pen1,
                        btn.FlatAppearance.BorderSize,
                        btn.FlatAppearance.BorderSize,
                        btn.ClientSize.Width - 3,
                        btn.ClientSize.Height - 3);
                }
            }

            var contentBounds = Rectangle.Inflate(btn.ClientRectangle, -3, -5);
            TextRenderer.DrawText(
                g,
                btn.Text,
                btn.Font,
                contentBounds,
                btn.Enabled ? btn.ForeColor : Color.Black,
                TextFormatFlags.SingleLine | TextFormatFlags.HorizontalCenter | TextFormatFlags.NoClipping);
        }

        private static void Gb_Paint(object sender, PaintEventArgs e)
        {
            var gb = (GroupBox)sender;
            var g = e.Graphics;
            g.Clear(gb.BackColor);
            using var pen1 = new Pen(Theme.BorderColor);
            g.DrawRectangle(pen1, new Rectangle(0, 6, gb.ClientSize.Width - 1, gb.ClientSize.Height - 8));
        }

        private static void ApplyDarkThemeToToolStripItemCollection(ToolStripItemCollection collection)
        {
            foreach (ToolStripItem tsi in collection)
            {
                switch (tsi)
                {
                    case ToolStripControlHost tsch:
                        ApplyDarkThemeToControl(tsch.Control);
                        break;
                    case ToolStripDropDownItem tsddi:
                        ApplyDarkThemeToToolStripItemCollection(tsddi.DropDownItems);
                        break;
                    default:
                        // nothing to do.
                        break;
                }
            }
        }
    }
}
