using System;
using System.Drawing;
using System.Windows.Forms;

class Spline : Form
{
    protected Point[] apt = new Point[4];
    protected float ftension = 0.5f;

    //public static void Main()
    //{
    //    Application.Run(new Spline());
    //}

    public Spline()
    {
        Text = "Spline";
        BackColor = SystemColors.Window;
        ForeColor = SystemColors.WindowText;
        ResizeRedraw = true;

        ScrollBar scroll = new VScrollBar();
        scroll.Parent = this;
        scroll .Dock =DockStyle .Right ;
        scroll .Minimum =-100;
        scroll .Maximum =109;
        scroll.SmallChange = 1;
        scroll.LargeChange = 10;
        scroll.Value = (int)(10 * ftension);
        scroll.ValueChanged += new EventHandler(ScrollValueChanged);

        OnResize(EventArgs.Empty);
    }
    void ScrollValueChanged(object obj, EventArgs ea)
    {
        ScrollBar scrool = (ScrollBar)obj;
        ftension = scrool.Value / 10f;
        Invalidate(false);
    }
    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);

        int cx = ClientSize.Width;
        int cy = ClientSize.Height;

        apt[0] = new Point(cx / 4, cy / 2);
        apt[1] = new Point(cx / 2, cy / 4);
        apt[2] = new Point(cx / 2, 3 * cy / 4);
        apt[3] = new Point(3 * cx / 4, cy / 2);

    }
    protected override void OnMouseDown(MouseEventArgs e)
    {
        Point pt;
        if (e.Button == MouseButtons.Left)
        {
            if (ModifierKeys == Keys.Shift)
                pt = apt[0];
            else if (ModifierKeys == Keys.None)
                pt = apt[1];
            else
                return;
        }
        else if (e.Button == MouseButtons.Right)
        {
            if (ModifierKeys == Keys.None)
                pt = apt[2];
            else if (ModifierKeys == Keys.Shift)
                pt = apt[3];
            else
                return;

        }
        else
            return;
        Cursor.Position = PointToScreen(pt);     
    }
    protected override void OnMouseMove(MouseEventArgs e)
    {
        Point pt = new Point(e.X, e.Y);
        if (e.Button == MouseButtons.Left)
        {


            if (ModifierKeys == Keys.Shift)
                pt = apt[0];
            else if (ModifierKeys == Keys.None)
                pt = apt[1];
            else
                return;

        }
        else if (e.Button == MouseButtons.Right)
        {
            if (ModifierKeys == Keys.None)
                pt = apt[2];
            else if (ModifierKeys == Keys.Shift)
                pt = apt[3];
            else
                return;
        }
        else
            return;
        Invalidate();
    }
    protected override void OnPaint(PaintEventArgs e)
    {
        Graphics graf = e.Graphics;
        Brush brush = new SolidBrush(ForeColor);
        graf.DrawCurve(new Pen(ForeColor), apt, ftension*(float)Math .Cos (3.4)*(float )Math .PI );
        graf.DrawString("Tension = " + ftension, Font, brush, 0, 0);
        for (int i = 0; i < 4; i++)
            graf.FillEllipse(brush, apt[i].X - 3, apt[i].Y - 3, 7, 7);
       
    }
}