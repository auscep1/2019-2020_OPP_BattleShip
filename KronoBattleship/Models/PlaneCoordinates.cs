using KronoBattleship.DESIGN_PATTERNS.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KronoBattleship.Models
{
    /**public class PlaneCoordinates
	{
		public PlaneCoordinates() { }
		public PlaneCoordinates(int xx, int yy, Unit plane)
		{
			Unit = plane;
			UnitId = plane.GetId();
			x = xx;
			y = yy;
		}
		public int PlaneCoordinatesId { get; set; }//pk
		public int UnitId { get; set; } //fk
		public int PlaneId { get; set; } //fk
		public virtual Unit Unit { get; set; }
		public int x { get; set; }
		public int y { get; set; }
		public bool Alive { get; set; } = true;
	}
	**/
    /** komentintas 20191030**/
    public class PlaneCoordinates
    {
        public int PlaneCoordinatesId { get; set; }//pk
        public int x { get; set; }
        public int y { get; set; }
        public bool Alive { get; set; } = true;

        public int PlaneId { get; set; } //fk
        public virtual Plane Plane { get; set; }

        public PlaneCoordinates() { }
        public PlaneCoordinates(int xx, int yy, Plane plane)
        {
            Plane = plane;
            PlaneId = plane.PlaneId;
            x = xx;
            y = yy;
        }
    }

}