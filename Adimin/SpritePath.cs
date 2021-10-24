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
        public const string source = "Sprites/Module/Source";
        public const string sourceUp = "Sprites/Module/Source_up";
        public const string sourceDown = "Sprites/Module/Source_down";
        public const string sourceLeft = "Sprites/Module/Source_left";
        public const string sourceRight = "Sprites/Module/Source_right";

        // Target
        public const string target = "Sprites/Module/Target";

        // Mirror
        public const string mirror = "Sprites/Module/Mirror";

        // Turn
        public const string turnLeft = "Sprites/Module/turn_left";
        public const string turnRight = "Sprites/Module/turn_right";

        // Merge
        public const string merge = "Sprites/Module/Merge";

        // Split
        public const string split = "Sprites/Module/Split";
    }

    public class Element
    {
        public const string circle = "Sprites/Element/circle";
        public const string cross = "Sprites/Element/cross";
        public const string square = "Sprites/Element/square";
        public const string triangle = "Sprites/Element/triangle";
    }
}
