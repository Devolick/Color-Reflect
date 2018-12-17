using UnityEngine;
using System.Collections;

public enum GameMainState
{
    None, Game, Pause
}
public enum GamePlayState {
None,Clear,Play
}

public class GameState:Base {

    static GameState instance;
    GameMainState state;
    public static GameMainState StateMain {
        get { return instance.state; }
        set { instance.state = value;
            if (value == GameMainState.Pause)
            {
                TableArea.Instance.FreezePhysicsBalls(true);
            }
            else
            {
                TableArea.Instance.FreezePhysicsBalls(false);
            }
        }
    }
    public static GamePlayState StatePlay {
        get;
        set;
    }

    protected override void _Awake()
    {
        base._Awake();
        instance = this;
        StateMain = GameMainState.Game;
        StatePlay = GamePlayState.Clear;
    }



















}
