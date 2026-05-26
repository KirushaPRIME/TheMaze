#define DEBUGING
#define GENERATEWITHOPTIMISATION
//#define SPAWNLIGHT

using gameProcess.MazeGenerator;
using System;
using System.Collections.Generic;
using UnityEngine;

public class MazeSpawn : MonoBehaviour
{
    BugGenerator MazeGenerator;
    List<SpawnInstruction> SpawnInstruction;
    List<position> LightPosition;
    [SerializeField] public int MazeHigh, MazeWidth;
    [SerializeField] private int TunnelBugNamber;

    [SerializeField] private GameObject ColumnPref;
    [SerializeField] private GameObject GroundPref;
    [SerializeField] private GameObject LightPref;

    [SerializeField] private Transform ParenetColumn;
    [SerializeField] public float WidthBlock;
    [SerializeField] private float HighBlock;
    [SerializeField] public float LevelGround;

    public void SpawnMaze()
    {
        GameObject Ground = Instantiate(GroundPref, ParenetColumn);
        Ground.GetComponent<Transform>().localScale = new Vector3(WidthBlock * MazeWidth / 10, 1, WidthBlock * MazeHigh / 10);
        Ground.GetComponent<Transform>().position =
            new Vector3(
                WidthBlock * MazeWidth / 2 - WidthBlock / 2,
                0,
                WidthBlock * MazeHigh / 2 - WidthBlock / 2
            );
        /*
#if DEBUGING
        Debug.Log("Start Generate Maze");
#endif

        MazeGenerator = new BugGenerator(MazeHigh, MazeWidth, MazeHigh / 2, MazeWidth / 2, TunnelBugNamber);

#if DEBUGING
        Debug.Log("End Generate Maze");
        int CountBlock = 0;
        for (int i = 0; i < MazeGenerator.GetWidth(); i++)
        {
            for (int j = 0; j < MazeGenerator.GetHeight(); j++)
            {
                if (!MazeGenerator[i,j])
                    CountBlock++;
            }
        }
        Debug.Log(CountBlock);
        Debug.Log("Start Optimizate Maze");
#endif


#if GENERATEWITHOPTIMISATION
        SpawnInstruction = OptimizateMaze.MakeInstructions(MazeGenerator);
#endif


#if DEBUGING
        Debug.Log("End Optimizate Maze");
        Debug.Log("Start Place Light");
#endif

        LightPosition = LightPlacemarker.PlaceLight(MazeGenerator);

#if DEBUGING
        Debug.Log("End Place Light");
        Debug.Log("Start Spawn Maze");
#endif

#if GENERATEWITHOPTIMISATION
        GameObject BlockObject;
        for (int i = 0; i < SpawnInstruction.Count; i++)
        {
            BlockObject = Instantiate(ColumnPref, ParenetColumn);
            BlockObject.transform.localScale = new Vector3(
                BlockObject.transform.localScale.x * (1 + SpawnInstruction[i][1].X - SpawnInstruction[i][0].X),
                BlockObject.transform.localScale.y,
                BlockObject.transform.localScale.z
                );
            BlockObject.transform.position =
                new Vector3(
                    (SpawnInstruction[i][1].X + SpawnInstruction[i][0].X) * WidthBlock / 2,
                    LevelGround + HighBlock / 2,
                    (SpawnInstruction[i][1].Y + SpawnInstruction[i][0].Y) * WidthBlock / 2
                    );
        }
#else
        GameObject BlockObject;
        for ( int i = 0; i < MazeGenerator.GetWidth(); i++)
        {
            for(int j = 0;j < MazeGenerator.GetHeight(); j++)
            {
                if (!MazeGenerator[i, j])
                {
                    BlockObject = Instantiate(ColumnPref, ParenetColumn);
                    BlockObject.transform.position = new Vector3(i * WidthBlock, LevelGround + HighBlock / 2, j * WidthBlock);
                }
            }
        }
#endif
#if SPAWNLIGHT
        GameObject LightObject;
        for (int i = 0; i < LightPosition.Count; i++)
        {
            LightObject = Instantiate(LightPref, ParenetColumn);
            LightObject.GetComponent<Transform>().position = new Vector3(LightPosition[i].X * WidthBlock, LevelGround + HighBlock, LightPosition[i].Y * WidthBlock);
        }
#endif
#if DEBUGING
        Debug.Log("End Spawn Maze");
#endif*/
    }
}
