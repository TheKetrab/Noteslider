using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Noteslider.Code
{
    public class MarginAnimator
    {
        FrameworkElement _element;
        double _step;
        int _delay;


        public MarginAnimator(FrameworkElement element, double step = 5, int delay = 2)
        {
            _element = element;
            _step = step;
            _delay = delay;
        }

        public async Task AnimateMargin(Thickness desiredMargin)
        {
            while (true)
            {
                await Task.Delay(_delay);

                // set setp value
                double left   = Math.Min(_step, Math.Abs(desiredMargin.Left - _element.Margin.Left));
                double top    = Math.Min(_step, Math.Abs(desiredMargin.Top - _element.Margin.Top));
                double right  = Math.Min(_step, Math.Abs(desiredMargin.Right - _element.Margin.Right));
                double bottom = Math.Min(_step, Math.Abs(desiredMargin.Bottom - _element.Margin.Bottom));

                // set direction
                if (desiredMargin.Left   < _element.Margin.Left)   left *= -1;
                if (desiredMargin.Top    < _element.Margin.Top)    top *= -1;
                if (desiredMargin.Right  < _element.Margin.Right)  right *= -1;
                if (desiredMargin.Bottom < _element.Margin.Bottom) bottom *= -1;

                // endif
                if (left == 0 && top == 0 && right == 0 && bottom == 0) break; 

                // update
                _element.Margin = ModifyThickness(_element.Margin, left, top, right, bottom);
                _element.UpdateLayout();
                Program.Window.RepaintTrackRenderer();
            }
        }

        private Thickness ModifyThickness(Thickness current,
            double left, double top, double right, double bottom)
        {
            return new Thickness(
                current.Left + left, current.Top + top,
                current.Right + right, current.Bottom + bottom);
        }


    }
}
