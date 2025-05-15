using System.Collections;
using System.Collections.Generic;
using System.Resources;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Managers : MonoBehaviour
{
    private static Managers s_instance;
    private DataManager _data = new DataManager();
    private InputManager _input = new InputManager();
    private PathManager _path = new PathManager();
    private PoolManager _pool = new PoolManager();
    private ResourceManager _resource = new ResourceManager();
    private SceneManagerEx _scene = new SceneManagerEx();
    private SoundManager _sound = new SoundManager();
    private TileManager _tile = new TileManager();
    private UIManager _ui = new UIManager();

    public static Managers Instance { get { Init(); return s_instance; } }
    public static DataManager Data { get { return Instance._data; } }
    public static InputManager Input { get { return Instance._input; } }
    public static PathManager Path { get { return Instance._path; } }
    public static PoolManager Pool { get { return Instance._pool; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    public static SceneManagerEx Scene { get { return Instance._scene; } }
    public static SoundManager Sound { get { return Instance._sound; } } 
    public static TileManager Tile { get { return Instance._tile; } }
    public static UIManager UI { get { return Instance._ui; } }

    void Start()
    {
        Init();
    }

    void Update()
    {
        _input.OnUpdate();
        _path.OnUpdate();
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
        _tile.Init(new Vector2Int(100, 100));
        _sound.Init();
        _pool.Init();
        _data.Init();
        _input.Init();
    }

    static public void Clear()
    {
        Sound.Clear();
        Input.Clear();
        Scene.Clear();
        UI.Clear();
        Pool.Clear();
    }
}
