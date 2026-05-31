using gameProcess.MazeGenerator;
using UnityEngine;

namespace gameProcess
{
    abstract class PositionConvertor
    {
        static public float WidthBlock;
        static public float HighBlock;
        static public float GroundLevel;
        static public Vector3 MazeInGlobal(int X, int Y)
        {
            return new Vector3(X * WidthBlock + WidthBlock / 2, GroundLevel, Y * WidthBlock + WidthBlock / 2);
        }
        static public position GlobalInMaze(Vector3 Vec)
        {
            return new position((int)(Vec.x / WidthBlock), (int)(Vec.z / WidthBlock));
        }
    }
}