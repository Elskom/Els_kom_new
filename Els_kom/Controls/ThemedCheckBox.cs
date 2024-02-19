// Copyright (c) 2014-2024, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom.Controls;

using System.ComponentModel;
using System.Drawing.Drawing2D;

[DefaultEvent("CheckedChanged")]
internal class ThemedCheckBox : Control
{
    private const int CheckBoxSize = 13;
    private readonly LinearGradientBrush backgroundBrush;
    private readonly LinearGradientBrush backgroundCheckedBrush;
    private readonly LinearGradientBrush innerBorderBrush;
    private readonly LinearGradientBrush innerBorderCheckedBrush;
    private readonly Pen innerBorderPen;
    private readonly Pen innerBorderCheckedPen;
    private readonly Pen borderPen;
    private bool isChecked;
    private bool isHover;
    private string? text;

    public ThemedCheckBox()
    {
        this.SpaceAfterCheckBox = 3;
        this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
        this.BackColor = Color.Transparent;
        this.ForeColor = Color.White;

        // http://connect.microsoft.com/VisualStudio/feedback/details/348321/bug-in-fillrectangle-using-lineargradientbrush
        this.backgroundBrush = new LinearGradientBrush(new Rectangle(2, 2, CheckBoxSize - 4, CheckBoxSize - 3), Color.FromArgb(105, 105, 105), Color.FromArgb(55, 55, 55), LinearGradientMode.Vertical);
        this.innerBorderBrush = new LinearGradientBrush(new Rectangle(1, 1, CheckBoxSize - 2, CheckBoxSize - 2), Color.FromArgb(125, 125, 125), Color.FromArgb(65, 75, 75), LinearGradientMode.Vertical);
        this.innerBorderPen = new Pen(this.innerBorderBrush);
        this.backgroundCheckedBrush = new LinearGradientBrush(new Rectangle(2, 2, CheckBoxSize - 4, CheckBoxSize - 3), Color.Black, Color.Black, LinearGradientMode.Vertical);
        var cb = new ColorBlend
        {
            Positions = new float[] { 0, 0.49f, 0.50f, 1 },
            Colors = new Color[] { Color.FromArgb(102, 163, 226), Color.FromArgb(83, 135, 186), Color.FromArgb(75, 121, 175), Color.FromArgb(56, 93, 135) },
        };
        this.backgroundCheckedBrush.InterpolationColors = cb;
        this.innerBorderCheckedBrush = new LinearGradientBrush(new Rectangle(1, 1, CheckBoxSize - 2, CheckBoxSize - 2), Color.FromArgb(133, 192, 241), Color.FromArgb(76, 119, 163), LinearGradientMode.Vertical);
        this.innerBorderCheckedPen = new Pen(this.innerBorderCheckedBrush);
        this.borderPen = new Pen(Color.FromArgb(30, 30, 30));
        this.Font = new Font("Arial", 8);
    }

    public event EventHandler? CheckedChanged;

    [DefaultValue(false)]
    public bool Checked
    {
        get => this.isChecked;
        set
        {
            if (this.isChecked != value)
            {
                this.isChecked = value;
                this.OnCheckedChanged(EventArgs.Empty);
                this.Invalidate();
            }
        }
    }

    public override string Text
    {
        get => this.text!;
        set
        {
            value ??= string.Empty;
            if (this.text != value)
            {
                this.text = value;
                this.Invalidate();
            }
        }
    }

    public int SpaceAfterCheckBox { get; set; }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        var g = e.Graphics;
        this.DrawBackground(g);
        if (!string.IsNullOrEmpty(this.Text))
        {
            this.DrawText(g);
        }
    }

    protected override void OnMouseEnter(EventArgs e)
    {
        base.OnMouseEnter(e);
        this.isHover = true;
        this.Invalidate();
    }

    protected override void OnMouseLeave(EventArgs e)
    {
        base.OnMouseLeave(e);
        this.isHover = false;
        this.Invalidate();
    }

    protected override void OnClick(EventArgs e)
    {
        base.OnClick(e);
        this.Checked = !this.Checked;
    }

    protected virtual void OnCheckedChanged(EventArgs e)
        => this.CheckedChanged?.Invoke(this, e);

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            this.backgroundBrush?.Dispose();
            this.backgroundCheckedBrush?.Dispose();
            this.innerBorderBrush?.Dispose();
            this.innerBorderPen?.Dispose();
            this.innerBorderCheckedBrush?.Dispose();
            this.innerBorderCheckedPen?.Dispose();
            this.borderPen?.Dispose();
        }

        base.Dispose(disposing);
    }

    private void DrawBackground(Graphics g)
    {
        if (this.Checked)
        {
            g.FillRectangle(this.backgroundCheckedBrush, new Rectangle(2, 2, CheckBoxSize - 4, CheckBoxSize - 4));
            g.DrawRectangle(this.innerBorderCheckedPen, new Rectangle(1, 1, CheckBoxSize - 3, CheckBoxSize - 3));
        }
        else
        {
            g.FillRectangle(this.backgroundBrush, new Rectangle(2, 2, CheckBoxSize - 4, CheckBoxSize - 4));
            if (this.isHover)
            {
                g.DrawRectangle(this.innerBorderCheckedPen, new Rectangle(1, 1, CheckBoxSize - 3, CheckBoxSize - 3));
            }
            else
            {
                g.DrawRectangle(this.innerBorderPen, new Rectangle(1, 1, CheckBoxSize - 3, CheckBoxSize - 3));
            }
        }

        g.DrawRectangle(this.borderPen, new Rectangle(0, 0, CheckBoxSize - 1, CheckBoxSize - 1));
    }

    private void DrawText(Graphics g)
    {
        var rect = new Rectangle(CheckBoxSize + this.SpaceAfterCheckBox, 0, this.ClientRectangle.Width - CheckBoxSize + this.SpaceAfterCheckBox, this.ClientRectangle.Height);
        var tff = TextFormatFlags.Left | TextFormatFlags.Top | TextFormatFlags.WordBreak;
        TextRenderer.DrawText(g, this.Text, this.Font, new Rectangle(rect.X, rect.Y + 1, rect.Width, rect.Height + 1), Color.Black, tff);
        TextRenderer.DrawText(g, this.Text, this.Font, rect, this.ForeColor, tff);
    }
}
