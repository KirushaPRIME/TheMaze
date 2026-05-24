#define DEBUG

using NUnit.Framework;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


namespace gameProcess
{
    namespace MazeGenerator
    {
        abstract class OptimizateMaze
        {
            public static List<SpawnInstruction> MakeInstructions(Maze maze)
            {
                bool StartNewWall = false;
                List<SpawnInstruction> spawnInstructions;
                spawnInstructions = new List<SpawnInstruction>();
                SpawnInstruction SI = new SpawnInstruction();
                for (int j = 0; j < maze.GetHeight(); j++)
                {
                    for (int i = 0; i < maze.GetWidth(); i++)
                    {
                        if (!StartNewWall && !maze[i, j])
                        {
                            SI = new SpawnInstruction();
                            SI.SetWhereFrom(i, j);
                            StartNewWall = true;
                        }
                        if ((maze[i + 1, j] || i + 1 >= maze.GetWidth()) && StartNewWall)
                        {
                            SI.SetWhereGo(i, j);
                            spawnInstructions.Add(SI);
                            StartNewWall = false;
                        }
                    }
                }

                return spawnInstructions;
            }
        }
        struct SpawnInstruction
        {
            position WhereFrom;
            position WhereGo;

            public void SetWhereFrom(int X, int Y)
            {
                WhereFrom = new position(X, Y);
            }
            public void SetWhereGo(int X, int Y)
            {
                WhereGo = new position(X, Y);
            }
            internal SpawnInstruction(position whereFrom, position whereGo)
            {
                WhereFrom = whereFrom;
                WhereGo = whereGo;
            }
            public SpawnInstruction(int whereFromX, int whereFromY, int whereGoX, int whereGoY)
            {
                WhereFrom.X = whereFromX;
                WhereFrom.Y = whereGoY;
                WhereGo.X = whereGoX;
                WhereGo.Y = whereGoY;

            }
            public position this[int index]
            {
                get
                {
                    switch (index)
                    {
                        case 0:
                            return WhereFrom;
                        case 1:
                            return WhereGo;
                        default:
                            throw new IndexOutOfRangeException();
                    }
                }
                internal set
                {
                    switch (index)
                    {
                        case 0:
                            WhereFrom = value;
                            break;
                        case 1:
                            WhereGo = value;
                            break;
                        default:
                            throw new IndexOutOfRangeException();
                    }
                }
            }
        }
    }
}