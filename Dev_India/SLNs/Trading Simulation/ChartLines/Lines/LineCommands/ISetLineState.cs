using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartLines.Lines.LineCommands
{
    interface ISetLineState
    {
        /// <summary>
        /// Make Line visible but not movable 
        /// </summary>
        void SetStatic();

        /// <summary>
        /// Set line visible and movable
        /// </summary>
        void SetActive();

        /// <summary>
        /// Hihe The line(s) hidden
        /// </summary>
        void SetHidden();

        /// <summary>
        /// Removes lines of its own type 
        /// </summary>
        void RemoveLines();

    }
}
