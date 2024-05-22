using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace wellAI
{
    public class RoundedPanel : Panel
    {
        public int CornerRadius { get; set; } = 10;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var rect = this.ClientRectangle;
            var path = new GraphicsPath();
            int radius = CornerRadius * 2;

            path.StartFigure();
            path.AddArc(new Rectangle(0, 0, radius, radius), 180, 90);
            path.AddArc(new Rectangle(rect.Width - radius, 0, radius, radius), 270, 90);
            path.AddArc(new Rectangle(rect.Width - radius, rect.Height - radius, radius, radius), 0, 90);
            path.AddArc(new Rectangle(0, rect.Height - radius, radius, radius), 90, 90);
            path.CloseFigure();

            this.Region = new Region(path);

            using (var brush = new SolidBrush(this.BackColor))
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.FillPath(brush, path);
            }
        }
    }
}
