using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//参考ページ https://qiita.com/Teach/items/d0911434d7642bd6b163

/// <summary>データを管理する</summary>
public class ReGameManager : MonoBehaviour
{
    /// <summary>シーンをロードした際にロード先の自分を入れるための変数</summary>
    GameManager m_gameManager;
    /// <summary>現在のState</summary>
    GameState m_currentGameState;
    /// <summary>スポーン地点</summary>
    GameObject m_spawnPoint;

    /// <summary>playerのオブジェクト</summary>
    [SerializeField] GameObject m_player = default;

    private void Awake()
    {
        m_spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint");
    }

    /// <summary>ゲームの状態</summary>
    enum GameState
    {
        Title,
        Playing,
        Respawn,
        Ending,
        ReSet
    }

    /// <summary>外から現在のStateを変える</summary>
    /// <param name="state">ゲームの状態</param>
    void SetCurrentState(GameState state)
    {
        m_currentGameState = state;
        OnGameStateChanged(m_currentGameState);
    }

    /// <summary>状態が変化したらどうするか</summary>
    /// <param name="state">ゲームの状態</param>
    void OnGameStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.Title:
                break;
            case GameState.Playing:
                break;
            case GameState.Respawn:
                Respawn();
                break;
            case GameState.Ending:
                break;
            case GameState.ReSet:
                break;
            default:
                break;
        }
    }

    void Respawn()
    {
        m_player.transform.position = m_spawnPoint.transform.position;
    }
}
