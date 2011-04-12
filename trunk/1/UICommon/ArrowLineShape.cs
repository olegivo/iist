using Microsoft.VisualBasic.PowerPacks;

namespace TP.CyclonAndScrubber
{
    /// <summary>
    /// 
    /// </summary>
    public class ArrowLineShape : LineShape
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Microsoft.VisualBasic.PowerPacks.LineShape"/> class.
        /// </summary>
        public ArrowLineShape()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Microsoft.VisualBasic.PowerPacks.LineShape"/> class, specifying the <see cref="T:Microsoft.VisualBasic.PowerPacks.ShapeContainer"/> where it will be parented.
        /// </summary>
        /// <param name="parent">A <see cref="T:Microsoft.VisualBasic.PowerPacks.ShapeContainer"/> where the shape will be parented
        ///             </param>
        public ArrowLineShape(ShapeContainer parent) : base(parent)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Microsoft.VisualBasic.PowerPacks.LineShape"/> class, specifying the coordinates for the line.
        /// </summary>
        /// <param name="x1">The X (horizontal) coordinate of the starting point of the line.
        ///             </param><param name="y1">The Y (vertical) coordinate of the starting point of the line.
        ///             </param><param name="x2">The X (horizontal) coordinate of the ending point of the line.
        ///             </param><param name="y2">The Y (vertical) coordinate of the ending point of the line.
        ///             </param>
        public ArrowLineShape(int x1, int y1, int x2, int y2) : base(x1, y1, x2, y2)
        {
        }
    }
}