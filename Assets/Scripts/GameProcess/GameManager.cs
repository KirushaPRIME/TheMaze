using gameProcess.MazeGenerator;
using UnityEngine;

namespace gameProcess{
    public class GameManager : MonoBehaviour
    {
        //-----------------------------------------------------------------------
        // œ¿–¿Ã≈“–€ √≈Õ≈–¿÷»»
        [SerializeField] public int MazeHigh, MazeWidth;
        [SerializeField] public float LevelGround;
        [SerializeField] private int TunnelBugNamber;
        [SerializeField] private int SizeBigCell;
        [SerializeField] public float WidthBlock;
        [SerializeField] private float HighBlock;
        [SerializeField] MazeSpawn mazeSpawn;
        [SerializeField] Transform BlockParent;
        [SerializeField] Transform InteractiveGOParent;
        [SerializeField] LoaderMaze LM;
        //-----------------------------------------------------------------------
        //Œ¡≈⁄ “€ ƒÀﬂ √≈Õ≈–¿÷»»
        [SerializeField] GameObject BlockPref;
        [SerializeField] private GameObject GroundPref;
        [SerializeField] private GameObject LightPref;
        [SerializeField] private GameObject CellingPref;
        //-----------------------------------------------------------------------
        //»Õ“≈–¿ “»¬Õ€≈ Œ¡⁄≈ “€
        [SerializeField] private GameObject CubePref;
        [SerializeField] private GameObject PrefEXIT;
        //-----------------------------------------------------------------------
        //œ–≈‘¿¡€ —”ŸÕŒ—“≈…
        [SerializeField] GameObject PlayerPref;
        [SerializeField] private GameObject BugPref;

        BugGenerator MazeGenerator;
        GameObject Player;

        void Awake()
        {
            LM.enabled = false;

            GameObject Ground = Instantiate(GroundPref, BlockParent);
            Ground.GetComponent<Transform>().localScale = new Vector3(WidthBlock * MazeWidth / 10 + 1, 1, WidthBlock * MazeHigh / 10 + 1);
            Ground.GetComponent<Transform>().position =
                new Vector3(
                    WidthBlock * MazeWidth / 2 - WidthBlock / 2,
                    LevelGround,
                    WidthBlock * MazeHigh / 2 - WidthBlock / 2
                );

            GameObject Celling = Instantiate(CellingPref, BlockParent);
            Celling.GetComponent<Transform>().localScale = new Vector3(WidthBlock * MazeWidth / 10 + 1, 1, WidthBlock * MazeHigh / 10 + 1);
            Celling.GetComponent<Transform>().position =
                new Vector3(
                    WidthBlock * MazeWidth / 2 - WidthBlock / 2,
                    LevelGround + HighBlock,
                    WidthBlock * MazeHigh / 2 - WidthBlock / 2
                );
            Celling.GetComponent<Transform>().Rotate(180, 0, 0);


            MazeGenerator = new BugGenerator(MazeHigh, MazeWidth, MazeHigh / 2, MazeWidth / 2, TunnelBugNamber);

            LightPlacemarker.PlaceLight(MazeGenerator);

            PositionConvertor.WidthBlock = WidthBlock;
            PositionConvertor.HighBlock = HighBlock;
            PositionConvertor.GroundLevel = LevelGround;
            LoaderMaze.SizeBigCell = SizeBigCell;
            LoaderMaze.maze = MazeGenerator;
            LoaderMaze.BlockPref = BlockPref;
            LoaderMaze.LightPref = LightPref;
            LoaderMaze.BlockParent = BlockParent;
            MazePlaceholder.maze = MazeGenerator;
            BugBehaviour.maze = MazeGenerator;




            Player = Instantiate(PlayerPref);
            PhisicsBodyBehaviour PBB = Player.GetComponent<PhisicsBodyBehaviour>();
            if (PBB != null)
                Player.GetComponent<PhisicsBodyBehaviour>().TransformObject(new Vector3(MazeWidth * mazeSpawn.WidthBlock / 2, 2, MazeHigh * mazeSpawn.WidthBlock / 2));
            else
                Player.GetComponent<Transform>().position = new Vector3(MazeWidth * mazeSpawn.WidthBlock / 2, 2, MazeHigh * mazeSpawn.WidthBlock / 2);

            LoaderMaze.AddUser(Player.GetComponent<Transform>());

            LM.enabled = true;
        }

        private void Start()
        {
            if (InteractiveGOParent == null){
                GameObject GO = new GameObject();
                GO.name = "InteractiveGOParent";
                InteractiveGOParent = GO.GetComponent<Transform>();
            }
            //GameObject TestCube;
            //for (int i = 0; i < 10; i++)
            //{
            //    TestCube = Instantiate(CubePref);
            //    TestCube.name += i;
            //    MazePlaceholder.PlaceObject(TestCube, true);
            //}

            if (PrefEXIT != null)
            {
                int X, Y;
                X = MazeGenerator.GetExit().X;
                Y = MazeGenerator.GetExit().Y;
                GameObject exitGO;
                exitGO = Instantiate(PrefEXIT, InteractiveGOParent);
                MazePlaceholder.PlaceObject(
                    exitGO, 
                    X, 
                    Y, 
                    (X == 0) ? Directions.left : (Y == 0) ? Directions.down : (X == MazeGenerator.GetWidth() - 1) ? Directions.right : Directions.up
                    );
            }

            GameObject Bugs = new GameObject();
            if (BugPref != null)
            {
                GameObject Bug;
                for (int i = 0; i < 100; i++)
                {
                    Bug = Instantiate(BugPref, Bugs.transform);
                    Bug.name += i;
                    MazePlaceholder.PlaceObject(Bug, true);
                }
            }
        }
    }
}