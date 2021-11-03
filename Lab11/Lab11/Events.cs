using System;
using System.Collections.Generic;
using System.Text;

namespace Lab11
{
    //Событие деления
    public sealed class EventArgsComplexZeroDivision : EventArgs
    {
        public EventArgsComplexZeroDivision(Complex dividerLeft, Complex dividerRight)
        {
            DivLeft = dividerLeft;
            DivRight = dividerRight;
        }

        public Complex DivLeft { get; }
        public Complex DivRight { get; }
    }
}
