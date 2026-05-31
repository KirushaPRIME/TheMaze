//#define DEBUGING

using System;
using System.Drawing;
using UnityEngine;

namespace gameProcess
{
    namespace MazeGenerator
    {
        abstract class MazePlaceholder
        {
            public static Maze maze;

            static public void PlaceObject(GameObject go, bool AgainstTheWall)
            {
                if (go == null)
                    return;
                bool FindCell = false;
                Vector3 Size;
                if (go.GetComponent<MeshRenderer>() == null){
                    for (int i = 0; i < go.transform.childCount; i++)
                    {
                        if (go.transform.GetChild(i).GetComponent<MeshRenderer>() != null)
                        {
                            Size = go.transform.GetChild(i).GetComponent<MeshRenderer>().bounds.size;
                            goto FIND_MR;
                        }
                    }
                    Size = Vector3.zero;
                }
                else
                    Size = go.GetComponent<MeshRenderer>().bounds.size;
                FIND_MR:
                Debug.Log(Size);
                Directions[] D = new Directions[4];
                int CountD = 0;
                int CountTry = 0;

                while (!FindCell && CountTry < 10)
                {
                    CountTry++;
                    for (int i = UnityEngine.Random.Range(0, maze.GetWidth()); i < maze.GetWidth(); i++)
                    {
                        for (int j = UnityEngine.Random.Range(0, maze.GetHeight()); j < maze.GetHeight(); j++)
                        {
                            CountD = 0;
                            if (maze.GetCell(i, j) == 'S')
                            {
                                if (AgainstTheWall)
                                {
                                    if (!maze[i, j + 1]) D[CountD++] = Directions.up;
                                    if (!maze[i, j - 1]) D[CountD++] = Directions.down;
                                    if (!maze[i - 1, j]) D[CountD++] = Directions.left;
                                    if (!maze[i + 1, j]) D[CountD++] = Directions.right;
                                    if (CountD == 0) continue;
                                    Directions ROr = D[UnityEngine.Random.Range(0, CountD)];
                                    go.GetComponent<Transform>().position =
                                        PositionConvertor.MazeInGlobal(i, j) +
                                        new Vector3(
                                            (((int)ROr % 2 != 0) ? 0 : ((int)ROr / 2)) * (PositionConvertor.WidthBlock / 2 - Size.z / 2),
                                            Size.y / 2,
                                            -(((int)ROr % 2 == 0) ? 0 : ((int)ROr)) * (PositionConvertor.WidthBlock / 2 - Size.z / 2)
                                            );
                                    go.GetComponent<Transform>().Rotate(0, ((int)ROr % 2 == 0) ? -90 * (int)ROr / 2 : ((int)ROr > 0) ? 0 : 180, 0);
                                    FindCell = true;
#if DEBUGING
                                    Debug.Log("Spawn: " + go.name + ", Ror = " + ROr + ", Count = " + CountD);
#endif
                                    return;
                                }
                                else
                                {

                                }
                            }
                        }
                    }
                }
                Debug.Log("FailePlace" + go.name);
            }


            static public void PlaceObject(GameObject go, int XPositionInMaze, int YPositionInMaze, Directions ROr)
            {
                if (go == null)
                    throw new CastomThrow("ÏÎÏÛÒÊÀ ÐÀÇÌÅÑÒÈÒÜ ÏÓÑÒÎÉ ÎÁÚÅÊÒ!");
                if (XPositionInMaze > maze.GetWidth() || YPositionInMaze > maze.GetHeight() || XPositionInMaze < 0 || YPositionInMaze < 0)
                {
                    throw new IndexOutOfRangeException();
                }

                Vector3 Size;
                if (go.GetComponent<MeshRenderer>() == null)
                {
                    for (int i = 0; i < go.transform.childCount; i++)
                    {
                        if (go.transform.GetChild(i).GetComponent<MeshRenderer>() != null)
                        {
                            Size = go.transform.GetChild(i).GetComponent<MeshRenderer>().bounds.size;
                            goto FIND_MR;
                        }
                    }
                    Size = Vector3.zero;
                }
                else
                    Size = go.GetComponent<MeshRenderer>().bounds.size;
                FIND_MR:

                go.GetComponent<Transform>().position =
                                        PositionConvertor.MazeInGlobal(XPositionInMaze, YPositionInMaze) +
                                        new Vector3(
                                            (((int)ROr % 2 != 0) ? 0 : ((int)ROr / 2)) * (PositionConvertor.WidthBlock / 2 - Size.z / 2),
                                            Size.y / 2,
                                            -(((int)ROr % 2 == 0) ? 0 : ((int)ROr)) * (PositionConvertor.WidthBlock / 2 - Size.z / 2)
                                            );
                go.GetComponent<Transform>().Rotate(0, ((int)ROr % 2 == 0) ? -90 * (int)ROr / 2 : ((int)ROr > 0) ? 0 : 180, 0);
            }
        }
    }
}