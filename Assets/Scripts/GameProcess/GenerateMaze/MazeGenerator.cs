//#define VISUAL
#define DEBUGING
#if VISUAL
using System.Threading;
#endif
using System;

namespace gameProcess
{
    namespace MazeGenerator
    {
        internal class Maze
        {
            char[,] Cell;
            int Width, Height;
            public int GetWidth() { return Width; }
            public int GetHeight() { return Height; }

            public Maze(int Width, int Height)
            {
                if (Width < 0 || Height < 0)
                {
                    throw new ArgumentException();
                }
                this.Width = Width;
                this.Height = Height;
                Cell = new char[Width, Height];
                for (int i = 0; i < Width; i++)
                {
                    for (int j = 0; j < Height; j++)
                    {
                        Cell[i, j] = 'W';
                    }
                }
            }
            public void SetCell(int W, int H, char C)
            {
                Cell[W, H] = C;
            }
            public char GetCell(int W, int H)
            {
                if (Cell == null)
                    throw new CastomThrow("Ћабиринт ещЄ не проиницилизирован!");
                if (W >= Width || H >= Height)
                    throw new IndexOutOfRangeException();
                else
                    return Cell[W, H];
            }
            public void ResetCell(int W, int H)
            {
                Cell[W, H] = 'W';
            }
            public void ShowMaze()
            {
                for (int i = 0; i < Height; i++)
                {
                    for (int j = 0; j < Width; j++)
                    {
                        if (Cell[j, i] != 'W')
                        {
                            Console.Write("  ");
                        }
                        else
                        {
                            Console.Write("[]");
                        }
                    }
                    Console.WriteLine();
                }
            }

            public bool this[int w, int h]
            {
                get
                {
                    if (w < 0 || h < 0) { return false; }
                    if (w >= Width || h >= Height) { return false; }
                    return Cell[w, h] != 'W';
                }
                protected set
                {
                    Cell[w, h] = 'S';
                }
            }
        }

        class BugGenerator : Maze
        {

            bool ExitFound = false;
            int NamberStartBug;
            position Exit;
            //static System.Random random = new System.Random();
            delegate void DoStep();
            event DoStep OnDoStep;



            public BugGenerator(int Width, int Height, int WStart, int HStart, int NamberStartBug) : base(Width, Height)
            {
                //Directions[] ways = new Directions[4];
                //Directions ChosenWay;
                //ushort WaysCount = 0;
                this.SetCell(WStart, HStart, 'S');
                this.NamberStartBug = NamberStartBug;
                Exit.X = -1;
                position Pos;
                Pos.X = WStart;
                Pos.Y = HStart;

                //Create base tunnel

                ConstructionBug[] Bugs = new ConstructionBug[NamberStartBug];
                for (int i = 0; i < Bugs.Length; i++)
                {
                    Bugs[i] = new ConstructionBug(Pos, this, ConstructionBug.OperatingMode.FindExit);
                }


                while (!ExitFound)
                {
                    if (OnDoStep != null)
                        OnDoStep();
                    else
                        break;

#if VISUAL
                Console.Clear();
                ShowMaze();
                Thread.Sleep(50);
#endif
                }

                for (int i = 0; i < Bugs.Length; i++)
                {
                    Bugs[i].KillBug();
                }


                //Make mini tunnel

                int SizeRipArea = 10;
                Bugs = new ConstructionBug[Width / SizeRipArea * Height / SizeRipArea];
                int CountRipper = 0;
                for (int i = 0; i < Width / SizeRipArea; i++)
                {
                    for (int j = 0; j < Height / SizeRipArea; j++)
                    {
                        for (int iRip = 0; iRip < SizeRipArea; iRip++)
                        {
                            for (int jRip = 0; jRip < SizeRipArea; jRip++)
                            {
                                if (this[i * SizeRipArea + iRip, j * SizeRipArea + jRip])
                                {
                                    goto NEXT;
                                }
                            }
                            Console.WriteLine();
                        }
                        Pos.X = i * SizeRipArea + SizeRipArea / 2;
                        Pos.Y = j * SizeRipArea + SizeRipArea / 2;
                        Bugs[CountRipper++] = new ConstructionBug(Pos, this, ConstructionBug.OperatingMode.Ripper);
                    NEXT:;
                    }
                }
                Console.WriteLine(CountRipper);

                for (int i = 1; i < SizeRipArea * 6.5; i++)
                {
                    if (OnDoStep != null)
                        OnDoStep();
                    else
                        break;
#if VISUAL
                Console.Clear();
                ShowMaze();
                Thread.Sleep(50);
#endif
                }


#if !VISUAL
                ShowMaze();
#endif
            }

            struct ConstructionBug
            {
                Directions[] ways;
                Directions ChosenWay;
                Directions LastWay;
                ushort WaysCount;
                position Pos;
                BugGenerator obj;

                public enum OperatingMode { FindExit, Ripper }
                OperatingMode OM;

                public ConstructionBug(position Pos, BugGenerator obj, OperatingMode OM)
                {
                    ways = new Directions[4];
                    this.OM = OM;
                    LastWay = ChosenWay = 0;
                    WaysCount = 0;
                    this.Pos.X = Pos.X;
                    this.Pos.Y = Pos.Y;
                    this.obj = obj;
                    obj.OnDoStep += DoStep;
                }

                public void DoStep()
                {
                    WaysCount = 0;
                    foreach (Directions way in Enum.GetValues(typeof(Directions)))
                    {
                        if ((int)LastWay + way == 0)
                            continue;
                        if (cheakWay(Pos, way))
                        {
                            ways[WaysCount++] = way;
#if SHOWECHOSE
                        Console.Write(way + " ");
#endif
                        }
                    }
                    ChosenWay = ways[UnityEngine.Random.Range(0, WaysCount)];
                    //ChosenWay = ways[0];
                    ways[0] = ChosenWay;
                    LastWay = ChosenWay;
#if SHOWECHOSE
                Console.WriteLine(" Choose: " + ChosenWay);
#endif
                    if ((int)ChosenWay % 2 == 0)
                    {
                        Pos.X += (int)ChosenWay / 2;
                    }
                    else
                    {
                        Pos.Y += (int)ChosenWay;
                    }
                    if (Pos.X >= obj.GetWidth() || Pos.X < 0 || Pos.Y >= obj.GetHeight() || Pos.Y < 0)
                    {
                        obj.ExitFound = true;
                        obj.OnDoStep -= DoStep;
                        if (obj.Exit.X == -1) obj.Exit = Pos;
                    }
                    else
                        obj.SetCell(Pos.X, Pos.Y, (OM == OperatingMode.Ripper) ? 'R' : 'S');
                }

                bool cheakWay(position Pos, Directions Move)
                {
                    switch (OM)
                    {
                        case OperatingMode.Ripper:
                            if ((int)Move % 2 == 0)
                            {
                                return !obj[Pos.X + (int)Move / 2, Pos.Y - 1] &&
                                    !obj[Pos.X + (int)Move / 2, Pos.Y + 1] &&
                                    (Pos.X + (int)Move) > 0 && (Pos.X + (int)Move) < obj.GetWidth() - 1; //дальнейшее продвижение не выйдет за границы
                            }
                            else
                            {
                                return !obj[Pos.X - 1, Pos.Y + (int)Move] &&
                                    !obj[Pos.X + 1, Pos.Y + (int)Move] &&
                                    (Pos.Y + (int)Move * 2) > 0 && (Pos.Y + (int)Move * 2) < obj.GetHeight() - 1;
                            }
                        case OperatingMode.FindExit:
                            if ((int)Move % 2 == 0)
                            {
                                return !obj[Pos.X + (int)Move / 2, Pos.Y - 1] && //ячейка ниже пуста€
                                    !obj[Pos.X + (int)Move / 2, Pos.Y + 1] && //ячейка выше пуста€
                                    !obj[Pos.X + (int)Move, Pos.Y] && //ячейка дальше пуста€
                                    !obj[Pos.X + (int)Move, Pos.Y - 1] &&
                                    !obj[Pos.X + (int)Move, Pos.Y + 1];
                            }
                            else
                            {
                                return !obj[Pos.X - 1, Pos.Y + (int)Move] &&
                                    !obj[Pos.X + 1, Pos.Y + (int)Move] &&
                                    !obj[Pos.X, Pos.Y + (int)Move * 2] &&
                                    !obj[Pos.X + 1, Pos.Y + (int)Move * 2] &&
                                    !obj[Pos.X - 1, Pos.Y + (int)Move * 2];
                            }
                    }
                    if ((int)Move % 2 == 0)
                    {
                        return !obj[Pos.X + (int)Move / 2, Pos.Y - 1] && !obj[Pos.X + (int)Move / 2, Pos.Y + 1];
                    }
                    else
                    {
                        return !obj[Pos.X - 1, Pos.Y + (int)Move] && !obj[Pos.X + 1, Pos.Y + (int)Move];
                    }
                }

                public void KillBug()
                {
                    obj.OnDoStep -= DoStep;
                }
            }
        }

        enum Directions { up = -1, down = 1, right = 2, left = -2 }

        struct position
        {
            public int X;
            public int Y;
            public position(int X, int Y)
            {
                this.X = X;
                this.Y = Y;
            }
            public override string ToString()
            {
                return X.ToString() + " " + Y.ToString();
            }

            public static position operator +(position other, position th)
            {
                return new position(other.X + th.X, other.Y + th.Y);
            }

            public static position operator /(position other, float value)
            {
                return new position((int)(other.X / value), (int)(other.Y / value));
            }
        }
    }
}