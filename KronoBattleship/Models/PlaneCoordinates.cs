using KronoBattleship.DESIGN_PATTERNS.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KronoBattleship.Models
{

    /// <summary>
    /// Plane coordinates class
    /// </summary>
    public class PlaneCoordinates
    {
        public int PlaneCoordinatesId { get; set; }//pk
        public int x { get; set; }
        public int y { get; set; }
        public bool Alive { get; set; } = true;

        public int PlaneId { get; set; } //fk
        public virtual Plane Plane { get; set; }

        /// <summary>
        /// Empty Plane Coordinates constructor
        /// </summary>
        public PlaneCoordinates() { }

       /// <summary>
       /// Plane Coordinates constructor
       /// </summary>
       /// <param name="xx">x coordinat</param>
       /// <param name="yy">y coordinate</param>
       /// <param name="plane">Plane object</param>
       public PlaneCoordinates(int xx, int yy, Plane plane)
        {
            Plane = plane;
            PlaneId = plane.PlaneId;
            x = xx;
            y = yy;
        }
    }

}