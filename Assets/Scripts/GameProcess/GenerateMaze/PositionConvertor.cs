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
            return new Vector3(X * WidthBlock, GroundLevel + HighBlock / 2, Y * WidthBlock);
        }
        static public position GlobalInMaze(Vector3 Vec)
        {
            return new position((int)(Vec.x / WidthBlock), (int)(Vec.z / WidthBlock));
        }
    }
}