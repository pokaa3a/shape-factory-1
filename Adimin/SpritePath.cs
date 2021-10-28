using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritePath
{
    public class UI
    {

    }

    public class Map
    {
        public const string tile = "Sprites/Map/Tile";
    }

    public class Module
    {
        // Source
        public const string source = "Sprites/Module/source/Source";
        public const string sourceUp = "Sprites/Module/source/Source_up";
        public const string sourceDown = "Sprites/Module/source/Source_down";
        public const string sourceLeft = "Sprites/Module/source/Source_left";
        public const string sourceRight = "Sprites/Module/source/Source_right";

        // Target
        public const string target = "Sprites/Module/target/Target";

        // Turn
        public const string turnLeft = "Sprites/Module/turn/turn_left";
        public const string turnRight = "Sprites/Module/turn/turn_right";

        // Merge
        public const string merge = "Sprites/Module/merge/Merge";

        // Split
        public const string split = "Sprites/Module/split/Split";

        // Rotate
        public const string rotateClockwise = "Sprites/Module/rotate/rotate_clockwise";
        public const string rotateCounterClockwise = "Sprites/Module/rotate/rotate_counterclockwise";

        // Paint
        public const string paintWhite = "Sprites/Module/paint/paint_white";
        public const string paintYellow = "Sprites/Module/paint/paint_yellow";
        public const string paintBlue = "Sprites/Module/paint/paint_blue";
        public const string paintRed = "Sprites/Module/paint/paint_red";

        // Grow
        public const string grow = "Sprites/Module/grow/grow";
    }

    public class Element
    {
        public class Circle
        {
            public const string white = "Sprites/Element/circle/white";
            public const string yellow = "Sprites/Element/circle/yellow";
            public const string blue = "Sprites/Element/circle/blue";
            public const string red = "Sprites/Element/circle/red";
        }
        public class Cross
        {
            public const string white = "Sprites/Element/cross/white";
            public const string yellow = "Sprites/Element/cross/yellow";
            public const string blue = "Sprites/Element/cross/blue";
            public const string red = "Sprites/Element/cross/red";
        }
        public class Square
        {
            public const string white = "Sprites/Element/square/white";
            public const string yellow = "Sprites/Element/square/yellow";
            public const string blue = "Sprites/Element/square/blue";
            public const string red = "Sprites/Element/square/red";
        }
        public class Triangle
        {
            public const string white = "Sprites/Element/triangle/white";
            public const string yellow = "Sprites/Element/triangle/yellow";
            public const string blue = "Sprites/Element/triangle/blue";
            public const string red = "Sprites/Element/triangle/red";
        }
    }
}
