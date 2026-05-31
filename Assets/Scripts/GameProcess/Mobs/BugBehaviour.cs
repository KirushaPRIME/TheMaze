
using gameProcess.MazeGenerator;
using UnityEngine;

namespace gameProcess{
    class BugBehaviour : MonoBehaviour
    {
        [SerializeField] private float Speed;
        public static Maze maze;
        public Vector3 NewPosition;
        Directions LastDirect = Directions.right;

        void Start()
        {
            NewPosition = transform.position;
            float R = UnityEngine.Random.value;
            Speed += R;
            GetComponent<AudioSource>().pitch += UnityEngine.Random.value * 0.1f;
        }
        private void FixedUpdate()
        {
            if (maze == null)
                return;
            Vector3 moveVector = NewPosition - transform.position;
            if ((moveVector).magnitude < 0.1)
            {
                Directions[] PossibleWays = new Directions[3]; ;
                short CountWays = 0;
                position pos = PositionConvertor.GlobalInMaze(transform.position);
                Directions oppositeDirect = (Directions)(-(int)LastDirect);

                if (maze[pos.X + 1, pos.Y] && oppositeDirect != Directions.right)
                    PossibleWays[CountWays++] = Directions.right;
                if (maze[pos.X , pos.Y - 1] && oppositeDirect != Directions.up)
                    PossibleWays[CountWays++] = Directions.up;
                if (maze[pos.X - 1, pos.Y] && oppositeDirect != Directions.left)
                    PossibleWays[CountWays++] = Directions.left;
                if (maze[pos.X, pos.Y + 1] && oppositeDirect != Directions.down)
                    PossibleWays[CountWays++] = Directions.down;

                if (CountWays > 0)
                    LastDirect = PossibleWays[UnityEngine.Random.Range(0, CountWays)];
                else
                    LastDirect = oppositeDirect;

                Debug.Log(LastDirect);

                NewPosition = PositionConvertor.MazeInGlobal(
                    (((int)LastDirect % 2 == 0) ? (int)LastDirect / 2 : 0) + pos.X,
                    (((int)LastDirect % 2 != 0) ? (int)LastDirect : 0) + pos.Y
                    );
                NewPosition += new Vector3(
                    (UnityEngine.Random.value - 0.5f) * PositionConvertor.WidthBlock,
                    0,
                    (UnityEngine.Random.value - 0.5f) * PositionConvertor.WidthBlock);
            }
            else
            {
                GetComponent<Transform>().position += Vector3.Normalize(moveVector) * Speed * Time.fixedDeltaTime;
                GetComponent<Transform>().LookAt(NewPosition);
            }
        }
    }
}