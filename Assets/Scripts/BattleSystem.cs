using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, PLAYERTURN2, PLAYERTURN3, PLAYERTURN4, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public Text[] healthTexts;
    public Text[] nameTexts;

    public GameObject playerPrefab;
    public GameObject player2Prefab;
    public GameObject player3Prefab;
    public GameObject player4Prefab;
    public GameObject enemyPrefab;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    Unit playerUnit;
    Unit player2Unit;
    Unit player3Unit;
    Unit player4Unit;
    Unit enemyUnit;

    public BattleState state;

    public BattleHUD playerHUD;
    public EnemyHUD enemyHUD;

    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    void Update()
    {
        playerHUD.SetHUD(playerUnit,nameTexts[0],healthTexts[0]);
        playerHUD.SetHUD(player2Unit,nameTexts[1],healthTexts[1]);
        playerHUD.SetHUD(player3Unit,nameTexts[2],healthTexts[2]);
        playerHUD.SetHUD(player4Unit,nameTexts[3],healthTexts[3]);
    }

    IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGO.GetComponent<Unit>();

        GameObject playerGO2 = Instantiate(player2Prefab, playerBattleStation);
        player2Unit = playerGO2.GetComponent<Unit>();

        GameObject playerGO3 = Instantiate(player3Prefab, playerBattleStation);
        player3Unit = playerGO3.GetComponent<Unit>();

        GameObject playerGO4 = Instantiate(player4Prefab, playerBattleStation);
        player4Unit = playerGO4.GetComponent<Unit>();

        GameObject enemyGO =  Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<Unit>();

        GameObject.Find("defend").SetActive(false);
        GameObject.Find("heal").SetActive(false);
        GameObject.Find("jump").SetActive(false);
        GameObject.Find("hurt").SetActive(false);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        playerTurn();

    }

    IEnumerator PlayerAttack()
    {

        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

        enemyHUD.SetHP(enemyUnit.currentHP);

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN2;
            playerTurn2();
        }
    }

    IEnumerator PlayerAttack2()
    {

        bool isDead = enemyUnit.TakeDamage(player2Unit.damage);

        enemyHUD.SetHP(enemyUnit.currentHP);

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN3;
            playerTurn3();
        }
    }

    IEnumerator PlayerAttack3()
    {

        bool isDead = enemyUnit.TakeDamage(player3Unit.damage);

        enemyHUD.SetHP(enemyUnit.currentHP);

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN4;
            playerTurn4();
        }
    }

    IEnumerator PlayerAttack4()
    {

        bool isDead = enemyUnit.TakeDamage(player4Unit.damage);

        enemyHUD.SetHP(enemyUnit.currentHP);

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator EnemyTurn()
    {

        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            playerTurn();
        }
    }

    void EndBattle()
    {
        
    }
    
    void playerTurn()
    {
        GameObject.Find("defend").SetActive(true);
    }

    void playerTurn2()
    {
        GameObject.Find("hurt").SetActive(true);
    }

    void playerTurn3()
    {
        GameObject.Find("heal").SetActive(true);
    }

    void playerTurn4()
    {
        GameObject.Find("jump").SetActive(true);
    }

    public void onAttackButton()
    {
        if (state == BattleState.PLAYERTURN)
            StartCoroutine(PlayerAttack());
        else if (state == BattleState.PLAYERTURN2)
            StartCoroutine(PlayerAttack2());
        else if (state == BattleState.PLAYERTURN3)
            StartCoroutine(PlayerAttack3());
        else if (state == BattleState.PLAYERTURN4)
            StartCoroutine(PlayerAttack4());
        else
            return;

    }
}
