using Extensions.Collections;
using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Overworld;
using Extensions.Coroutines;
using System.IO;
using Helper_Scripts;

namespace Battle
{
    public class BattleController : MonoBehaviour
    {

        public PlayerShip[] PlayerShips;
        public EnemyShip[] EnemyShips;
        public Ship[] AllShips;
        public EnemyShip[] NextWave = null;
        public enum Initiative { Player, Enemy, Neutral }
        public Initiative I;
        public static BattleController Controller;
        public Vector3 BattlePosition;
        public Vector3 CameraBattlePos;
        public Image Black;
        public bool InBattle;
        public Ship CurrentShip;
        public bool Selecting;
        public bool AnimationPlaying = false;
        public BattleCommand SelectedCommand;
        public Ship[] SelectedTarget;
        public bool Auto;
        public bool TurnStarted;
        public GameObject CurrentLight;
        public Vector3 prevPos;
        public Vector3 prevCam;

        public List<EnemyShip.Rewards> Rewards = new List<EnemyShip.Rewards>();

        public void Awake()
        {
            Controller = this;
            BattleSkill s = new BattleSkill();
            LoadAllSkillsFromJson();

        }


        public void StartBattle(EnemyShip[] Enemies, Initiative i)
        {
            prevPos = PlayerShipMovement.Player.transform.position;
            prevCam = Camera.main.transform.position;
            switch (i)
            {
                case Initiative.Player:
                    Music.ChangeSong("Player");
                    break;
                case Initiative.Enemy:
                    Music.ChangeSong("Enemy");
                    break;
                case Initiative.Neutral:
                    Music.ChangeSong("Neutral");
                    break;
                default:
                    break;
            }

            OverWorldUI.UI.gameObject.SetActive(false);
            BattleUI.UI.gameObject.SetActive(true);
            I = i;
            InBattle = true;
            EnemyShips = Enemies;
            Enemies[0].Allies = EnemyShips;
            StartCoroutine(this.Sequence(
                Fade(), MainBattleLoop(), EndBattle()
            ));
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                Auto = !Auto;
            }
        }

        IEnumerator MainBattleLoop()
        {
            if (I == Initiative.Player)
            {
                foreach (Ship ship in PlayerShips)
                {
                    SetCurrentLight(ship);
                    yield return StartCoroutine(TakeTurn(ship));
                    if (!Alive(EnemyShips))
                    {
                        yield break;
                    }
                }
            }
            else if (I == Initiative.Enemy)
            {
                foreach (Ship ship in EnemyShips)
                {
                    SetCurrentLight(ship);
                    yield return StartCoroutine(TakeTurn(ship));
                    if (!Alive(PlayerShips))
                    {
                        yield break;
                    }
                }

            }
            int i = 0;
            while (Alive(PlayerShips) && Alive(EnemyShips))
            {
                CurrentShip = AllShips[i];
                if (CurrentShip.Alive())
                {
                    SetCurrentLight(CurrentShip);
                    yield return StartCoroutine(TakeTurn(CurrentShip));
                }
                i++;
                i = AllShips.Count() == i ? 0 : i;
            }

        }

        IEnumerator EndBattle()
        {
            if (Alive(PlayerShips))
            {
                yield return StartCoroutine(Win());
            }
            else
            {
                yield return StartCoroutine(Lose());
            }
        }
        private IEnumerator Lose()
        {
            Debug.Log("Player Loses");
            throw new NotImplementedException();
        }

        private IEnumerator Win()
        {
            Debug.Log("Player Wins");
            Music.ChangeSong("Victory");
            BringUpRewardsScreen();
            //for(int i=0;i<Rewards.)
            yield return null;
        }

        private void BringUpRewardsScreen()
        {
            BattleUI.UI.rewardScreen.Initialize(Rewards);
        }

        bool Alive(Ship[] Ships)
        {
            foreach (Ship ship in Ships)
            {
                if (ship.Alive()) { return true; }
            }
            return false;
        }

        void SetUpBattle()
        {

            //This might need to be split up but I don't think it's that neccesary
            PlayerShips = Array.ConvertAll(PlayerShip.Player.Allies, item => (PlayerShip)item); ;
            PlayerShipMovement.Player.transform.position = BattlePosition;
            PlayerShips[1].gameObject.SetActive(true);
            PlayerShips[2].gameObject.SetActive(true);
            PlayerShips[0].transform.position = new Vector3(BattlePosition.x, BattlePosition.y, BattlePosition.z);
            PlayerShips[1].transform.position = new Vector3(BattlePosition.x - 10, BattlePosition.y, BattlePosition.z);
            PlayerShips[2].transform.position = new Vector3(BattlePosition.x + 10, BattlePosition.y, BattlePosition.z);

            EnemyShips = (EnemyShip[])EnemyShips[0].Allies;
            EnemyShips[1].gameObject.SetActive(true);
            EnemyShips[2].gameObject.SetActive(true);
            EnemyShips[0].transform.position = new Vector3(BattlePosition.x, BattlePosition.y, BattlePosition.z + 10);
            EnemyShips[1].transform.position = new Vector3(BattlePosition.x - 10, BattlePosition.y, BattlePosition.z + 10);
            EnemyShips[2].transform.position = new Vector3(BattlePosition.x + 10, BattlePosition.y, BattlePosition.z + 10);
            PlayerShipMovement.Player.transform.rotation = Quaternion.identity;
            EnemyShips.ToList().ForEach(x => x.transform.eulerAngles = x.DefaultRot);
            EnemyShips.ToList().ForEach(x => x.AdjustStatsToLevel());
            EnemyShips[0].GetComponent<EnemyShipMovement>().enabled = false;
            Camera.main.transform.position = CameraBattlePos;
            PlayerShips.ToList().ForEach(x => x.Enemies = EnemyShips);
            EnemyShips.ToList().ForEach(x => x.Enemies = PlayerShips);
            PlayerShipMovement.Player.enabled = false;
            //Instantiate Party
            //Instantiate Enemies
            AllShips = EnemyShips.Add<Ship>(PlayerShips).OrderBy(x => x.stats["Speed",true]).ToArray();

        }

        public IEnumerator Fade()
        {

            for (float f = 0; f <= 1; f += 0.2f)
            {
                Color c = Black.color;
                c.a = f;
                Black.color = c;
                yield return new WaitForSeconds(0.01f);
            }
            SetUpBattle();
            for (float f = 1; f >= 0; f -= 0.02f)
            {
                Color c = Black.color;
                c.a = f;
                Black.color = c;
                yield return new WaitForSeconds(0.01f);
            }
        }

        public void SetCommand(BattleCommand Command, Ship User)
        {
            SelectedCommand = Command;
            Command.User = User;
        }
        public IEnumerator TakeTurn(Ship s)
        {
            /*
            TODO Add a back button
            With the current implementation it gets data
            Then uses that data to wait for more data
            A better way would be for the functions to chain into each other
            This will probably be alot of refactoring
            */
            CurrentShip = s;
            yield return StartCoroutine(s.StartTurn());
            StartCoroutine(CurrentShip.GetCommand());
            yield return new WaitUntil(() => SelectedCommand != null);
            StartCoroutine(CurrentShip.GetTarget(SelectedCommand));
            yield return new WaitUntil(() => SelectedTarget != null); //I know this is kinda dumb
            yield return new WaitUntil(() => SelectedTarget.Length > 0); //will fix later
            StartCoroutine(SelectedCommand.Do(CurrentShip, SelectedTarget));
            yield return new WaitUntil(() => !AnimationPlaying);
            SelectedCommand = null;
            SelectedTarget = null;
            if (s.GetComponent<EnemyShip>() != null) {
                yield return new WaitForSeconds(0.5f);//TODO Make this be handled within enemyShip
            }
            yield return StartCoroutine(s.EndTurn());
        }

        void LoadAllSkillsFromJson()
        {
            DirectoryInfo d = new DirectoryInfo(Application.streamingAssetsPath + "/Skills/JSON/");
            FileInfo[] f = d.GetFiles();
            foreach (FileInfo file in f)
            {
                if (file.Name.Split('.').Last() == "json")
                {
                    BattleSkill skill = new BattleSkill();
                    skill.FromJSON(file.Name.Split('.')[0]);
                    //c.ToJSON();
                    //ScriptableObjectUtility.CopyAsset(c, Application.dataPath+"/Cards/", c.Name);
                    if (!BattleSkill.Skills.ContainsKey(skill.Name))
                    {
                        BattleSkill.Skills.Add(skill.Name, skill);
                    }

                }
            }
    
        }

        public void Leave()
        {
            OverWorldUI.UI.gameObject.SetActive(true);
            BattleUI.UI.gameObject.SetActive(false);
            PlayerShipMovement.Player.transform.position = prevPos;
            PlayerShipMovement.Player.enabled = true;
            Camera.main.transform.position = prevCam;
            Music.ChangeSong("OverWorld");
        }


        public void SetCurrentLight(Ship S)
        {
            CurrentLight.transform.position = S.transform.position;
            CurrentLight.transform.position=new Vector3(CurrentLight.transform.position.x, CurrentLight.transform.position.y+2,CurrentLight.transform.position.z);
        }

    }
}
