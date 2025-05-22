using System.Collections;
using System.Collections.Generic;
using System.Resources;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Managers : MonoBehaviour
{
    private static Managers s_instance;
    private DataManager _data = new DataManager();
    private InputManager _input = new InputManager();
    private ItemManager _item = new ItemManager();
    private PathManager _path = new PathManager();
    private PoolManager _pool = new PoolManager();
    private ProbManager _prob = new ProbManager();
    private ResourceManager _resource = new ResourceManager();
    private SequenceManager _sequence = new SequenceManager();
    private SceneManagerEx _scene = new SceneManagerEx();
    private SoundManager _sound = new SoundManager();
    private TileManager _tile = new TileManager();
    private UIManager _ui = new UIManager();
    private EmployeeManager _employee = new EmployeeManager();
    private WorkManager _work = new WorkManager();
    private WorkMediator _workMediator = new WorkMediator();
    
    private static Managers Instance { get { Init(); return s_instance; } }
    public static DataManager Data { get { return Instance._data; } }
    public static InputManager Input { get { return Instance._input; } }
    public static ItemManager Item { get { return Instance._item; } }
    public static PathManager Path { get { return Instance._path; } }
    public static PoolManager Pool { get { return Instance._pool; } }
    public static ProbManager Prob { get { return Instance._prob; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    public static SequenceManager Sequence { get { return Instance._sequence; } }
    public static SceneManagerEx Scene { get { return Instance._scene; } }
    public static SoundManager Sound { get { return Instance._sound; } } 
    public static TileManager Tile { get { return Instance._tile; } }
    public static UIManager UI { get { return Instance._ui; } }
    public static EmployeeManager Employee { get { return Instance._employee; } }
    public static WorkManager Work { get { return Instance._work; } }
    private WorkMediator WorkMediator { get { return Instance._workMediator; } }

    void Start()
    {
        Init();
    }

    void Update()
    {
        Input.OnUpdate();
        Path.OnUpdate();
        WorkMediator.OnUpdate();
    }

    static void Init()
    {
        if (null == s_instance)
        {
            GameObject gameobject = GameObject.Find("@Managers");
            if (null == gameobject)
            {
                gameobject = new GameObject { name = "@Managers" };
                gameobject.AddComponent<Managers>();
            }

            DontDestroyOnLoad(gameobject);
            s_instance = gameobject.GetComponent<Managers>();

            /* 자식 매너지 초기화  */
            s_instance.InitChilds();
            /* End */
        }
    }

    private void InitChilds()
    {
        Tile.Init(new Vector2Int(100, 100));
        Sound.Init();
        Pool.Init();
        Data.Init();
        Input.Init();
        Item.Init();
        WorkMediator.Init();
        Sequence.Init();
        Prob.Init();
    }

    static public void Clear()
    {
        Sound.Clear();
        Sequence.Clear();
        Input.Clear();
        Item.Clear();
        Scene.Clear();
        UI.Clear();
        Pool.Clear();
        Prob.Clear();
    }
}
