//#define USELIGHT

using gameProcess.MazeGenerator;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;


namespace gameProcess
{
    class LoaderMaze : MonoBehaviour
    {
        [SerializeField] static public GameObject BlockPref;
        [SerializeField] static public GameObject LightPref;
        [SerializeField] static public Transform BlockParent;
        static BigCell[][] LoadMap;
        static public int SizeBigCell = 10;
        static int LoadMapWidth;
        static int LoadMapHeight;
        static short offset = -1;
        public static Maze maze;
        static List<Transform> Users = new List<Transform>();

        delegate void OneUpdate();
        static event OneUpdate OnOneUpdate;

        void Start()
        {

            if (maze == null)
            {
                throw new CastomThrow("Лаберинт не проинсталезирован!");
            }
            LoadMapWidth = maze.GetWidth() / SizeBigCell + ((maze.GetWidth() % SizeBigCell == 0) ? 0 : 1) + 1;
            LoadMapHeight = maze.GetHeight() / SizeBigCell + ((maze.GetHeight() % SizeBigCell == 0) ? 0 : 1) + 1;
            LoadMap = new BigCell[LoadMapWidth][];
            for (int i = 0; i < LoadMapWidth; i++)
            {
                LoadMap[i] = new BigCell[LoadMapHeight];
            }
        }
        private void FixedUpdate()
        {
            if (OnOneUpdate != null)
                OnOneUpdate();
            else
                Debug.Log("NONE LODAER USERS");
            for (int i = 0; i < Users.Count; i++)
            {
                if (Users[i] == null)
                    Users.RemoveAt(i);
                position Pos = PositionConvertor.GlobalInMaze(Users[i].position);
                Pos.X = (Pos.X - offset) / SizeBigCell;
                Pos.Y = (Pos.Y - offset) / SizeBigCell;
                Debug.Log(Pos.ToString());
                for (
                    int x = (Pos.X - 1 >= 0) ? Pos.X - 1 : 0;
                    x < Pos.X - 1 + 3 && x < LoadMapWidth;
                    x++
                    )
                    for (
                        int y = (Pos.Y - 1 >= 0) ? Pos.Y - 1 : 0;
                        y < Pos.Y - 1 + 3 && y < LoadMapHeight;
                        y++
                        )
                    {

                        if (LoadMap[x][y] == null)
                        {
                            LoadMap[x][y] = new BigCell(x, y);
                        }
                        else
                        {
                            if (LoadMap[x][y].GetImpuls() < 1)
                            {
                                LoadMap[x][y].AddImpuls();
                            }
                        }
                    }
            }
        }

        public static void AddUser(Transform transform)
        {
            Users.Add(transform);
        }

        public static void RemuveUser(Transform transform)
        {
            Users.Remove(transform);
        }

        class BigCell
        {
            position Pos;
            short ActiveImpuls;
            public short GetImpuls() { return ActiveImpuls; }
            Transform[,] LoadZone;
            GameObject Parent;
            static int IDGenerate;
            public BigCell(int X, int Y)
            {
                Pos = new position(X, Y);
                LoadZone = null;
                OnOneUpdate += DoWithUpdate;
                Parent = new GameObject();
                Parent.name = "BigCell:" + X + "," + Y;
                Parent.transform.SetParent(BlockParent);
                AddImpuls();
            }
            void DoWithUpdate()
            {
                ActiveImpuls--;
                if (ActiveImpuls < 0)
                    ActiveImpuls = 0;
                else return;
                if (LoadZone != null)
                {
                    for (int i = 0; i < SizeBigCell; i++)
                    {
                        for (int j = 0; j < SizeBigCell; j++)
                        {
                            if (LoadZone[i, j] != null)
                            {
                                Destroy(LoadZone[i, j].GameObject());
                            }
                        }
                    }
                    OnOneUpdate -= DoWithUpdate;
                    LoadZone = null;
                    LoadMap[Pos.X][Pos.Y] = null;
                    Destroy(Parent);
                }
            }
            public void AddImpuls()
            {
                ActiveImpuls++;
                if (LoadZone == null)
                {
                    LoadZone = new Transform[SizeBigCell, SizeBigCell];

                    int Xi = 0, Yi = 0;

                    for (
                        int i = Pos.X * SizeBigCell + offset;
                        i < SizeBigCell + Pos.X * SizeBigCell + offset;
                        i++
                        )
                    {
                        Yi = 0;
                        for (
                            int j =  Pos.Y * SizeBigCell + offset;
                            j < SizeBigCell + Pos.Y * SizeBigCell + offset;
                            j++
                            )
                        {
                            try
                            {
                                if (maze.GetCell(i, j) == 'W')
                                {
                                    LoadZone[Xi, Yi] = Instantiate(BlockPref, Parent.transform).GetComponent<Transform>();
                                    LoadZone[Xi, Yi].GetComponent<Transform>().position = PositionConvertor.MazeInGlobal(i, j) + 
                                        new Vector3(0, PositionConvertor.HighBlock / 2, 0);
                                    LoadZone[Xi, Yi].name += i.ToString() + " " + j;
                                }
#if USELIGHT
                            else if (maze.GetCell(i, j) == 'L')
                            {
                                LoadZone[Xi, Yi] = Instantiate(LightPref, Parent.transform).GetComponent<Transform>();
                                LoadZone[Xi, Yi].GetComponent<Transform>().position = PositionConvertor.MazeInGlobal(i, j) +
                                    new Vector3(0, PositionConvertor.HighBlock / 2, 0);
                            }
#endif
                            }
                            catch (IndexOutOfRangeException)
                            {
                                LoadZone[Xi, Yi] = Instantiate(BlockPref, Parent.transform).GetComponent<Transform>();
                                LoadZone[Xi, Yi].GetComponent<Transform>().position = PositionConvertor.MazeInGlobal(i, j) + 
                                    new Vector3(0, PositionConvertor.HighBlock / 2, 0);
                                LoadZone[Xi, Yi].name += i.ToString() + " " + j;
                            }

                            Yi++;
                        }
                        Xi++;
                    }
                }
            }
        }
    }
}