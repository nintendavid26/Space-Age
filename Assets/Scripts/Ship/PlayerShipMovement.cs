using UnityEngine;
using System.Collections;
using Battle;

namespace Overworld
{
    public class PlayerShipMovement : ShipMovement
    {
        public GameObject Background;
        public Material BackgroundTexture;
        public Vector2 TextureOffset;
        public static PlayerShipMovement Player;
        public OverWorldUI UI;
        public int Money;
        public PlayerShip Ally1;
        public PlayerShip Ally2;

        // Use this for initialization
        void Start()
        {
            ship = GetComponent<Ship>();
            TextureOffset = new Vector2(0, 0);
            ship = GetComponent<PlayerShip>();
            rb = GetComponent<Rigidbody>();
            BackgroundTexture.SetTextureOffset("_MainTex", TextureOffset);
        }

        void Awake()
        {
            Player = this;
        }
        // Update is called once per frame
        void GetMove()
        {
            if (!ShipCanMove) { return; }
            if (Input.GetKeyDown(KeyCode.Space)) { Shoot(); }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                if (Input.GetKey(KeyCode.RightArrow)) { Move(Direction.NE); }
                else if (Input.GetKey(KeyCode.LeftArrow)) { Move(Direction.NW); }
                else { Move(Direction.N); }
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                if (Input.GetKey(KeyCode.RightArrow)) { Move(Direction.SE); }
                else if (Input.GetKey(KeyCode.LeftArrow)) { Move(Direction.SW); }
                else { Move(Direction.S); }
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                Move(Direction.E);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                Move(Direction.W);
            }
        }

        void Update()
        {
            GetMove();

        }

        public override void Move(Direction dir)
        {
            base.Move(dir);
            if (dir == Direction.N)
            {
                TextureOffset.y = TextureOffset.y + Time.deltaTime;
            }

            else if (dir == Direction.S)
            {
                TextureOffset.y = TextureOffset.y - Time.deltaTime;
            }
            else if (dir == Direction.W)
            {
                TextureOffset.x = TextureOffset.x - Time.deltaTime;
            }
            else if (dir == Direction.E)
            {
                TextureOffset.x = TextureOffset.x + Time.deltaTime;
            }
            else if (dir == Direction.NE)
            {
                TextureOffset.y = TextureOffset.y + Time.deltaTime;
                TextureOffset.x = TextureOffset.x + Time.deltaTime;
            }
            else if (dir == Direction.NW)
            {
                TextureOffset.y = TextureOffset.y + Time.deltaTime;
                TextureOffset.x = TextureOffset.x - Time.deltaTime;
            }
            else if (dir == Direction.SE)
            {
                TextureOffset.y = TextureOffset.y - Time.deltaTime;
                TextureOffset.x = TextureOffset.x + Time.deltaTime;
            }
            else if (dir == Direction.SW)
            {
                TextureOffset.y = TextureOffset.y - Time.deltaTime;
                TextureOffset.x = TextureOffset.x - Time.deltaTime;
            }
            Camera.main.transform.position = new Vector3(transform.position.x, Camera.main.transform.position.y, transform.position.z);
            Background.transform.position = new Vector3(transform.position.x, Background.transform.position.y, transform.position.z);
            BackgroundTexture.SetTextureOffset("_MainTex", TextureOffset);

        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("EnemyBullet"))
            {
                Bullet B = other.GetComponent<Bullet>();
                EnemyShipMovement E = (EnemyShipMovement)B.ShotFrom;
                EnemyShip[] EnemyTeam = E.CastTeam();
                BattleController.Controller.StartBattle(EnemyTeam, BattleController.Initiative.Enemy);
            }
        }

        public override void Shoot()
        {
            base.Shoot();
            ship.stats["fuel",false]--;
            UpdateUI();
            
        }

        public void UpdateUI()
        {
            UI.FuelText.text = "Fuel " + ship.stats["Fuel",false] + "/" + ship.stats["MaxFuel",false];
            UI.Fuel.fillAmount = (float)ship.stats["Fuel",false] / ship.stats["Fuel",false];
            UI.Money.text = "$" + Money;
        }

    }
}