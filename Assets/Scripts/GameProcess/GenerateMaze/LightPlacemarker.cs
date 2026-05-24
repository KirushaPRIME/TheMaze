using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace gameProcess{
    namespace MazeGenerator
    {
        abstract class LightPlacemarker
        {
            static ushort intensity = 8;
            static Maze maze;
            static ushort[,] LightMap;
            public static void PlaceLight(Maze maze)
            {
                LightMap = new ushort[maze.GetWidth(), maze.GetHeight()];
                LightPlacemarker.maze = maze;

                for (int i = 0; i < maze.GetWidth(); i++)
                {
                    for (int j = 0; j < maze.GetHeight(); j++)
                    {
                        if (maze.GetCell(i, j) != 'W' && maze.GetCell(i, j) != 'L')
                        {
                            if (LightMap[i, j] < 1)
                                AddLightOnMap(i, j);
                        }
                    }
                }
            }

            static void AddLightOnMap(int x, int y)
            {
                maze.SetCell(x, y, 'L');
                for (
                    int i = (x - intensity > 0) ? x - intensity : 0;
                    i < x + intensity && i < maze.GetWidth();
                    i++)
                {
                    for (
                    int j = (y - intensity > 0) ? y - intensity : 0;
                    j < y + intensity && j < maze.GetHeight();
                    j++)
                    {
                        LightMap[i, j] += 1;
                    }
                }
            }
        }
    }
}