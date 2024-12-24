using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EACProject.Commands;
using EACProject.Modules;
using Rocket.Core.Logging;
using Rocket.Core.Plugins;
using Rocket.Core.Steam;
using Rocket.Unturned;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Events;
using Rocket.Unturned.Player;
using SDG.NetTransport;
using SDG.Unturned;
using Steamworks;
using UnityEngine;

using Logger = Rocket.Core.Logging.Logger;

namespace EACProject
{
    public class EACProject : RocketPlugin<EACConfiguration>
    {
        public static CSteamID groupAA;
        
        public static CSteamID groupBB;
        public static EACProject Instance { get; set; }
        
        public static UnturnedPlayer areaplayertest;

        public static uint groupAMEM = 0;
        public static uint groupBMEM = 0;
        public static float BluePlayerCount = 0;
        public static float RedPlayerCount = 0;
        public static float GreenPlayerCount = 0;
        public static ulong BluePoints = 0;
        public static ulong RedPoints = 0;
        public static ulong GreenPoints = 0;

        

        public static bool GameActive = true;
        public static bool NewGameCreatining = false;
        public static Vector3 USABase = new Vector3(-17, 32, 511);
        public static Vector3 RUSBase = new Vector3(-103, 32, 785);
        public static Vector3 Lobby = new Vector3(-264, 87, 628);
        CSteamID groupnewid = GroupManager.generateUniqueGroupID();
        
        public class TickSec : MonoBehaviour
        {

            public DateTime Check;
            public DateTime t_AreaCheck;
            public DateTime t_markersend;
            double asec = 3;
            double playercheck = 1;
            double markersend = 0.25;

            public void Awake()
            {
                Check = DateTime.Now;
                t_AreaCheck = DateTime.Now;
                UnturnedChat.Say("tick1");

        }
        AreaCapture m_areacapture = new AreaCapture();

            public void FixedUpdate()
            {
                if ((DateTime.Now - t_AreaCheck).TotalSeconds >= asec)//AREA
                {

                    t_AreaCheck = DateTime.Now;
                    if(GameActive == true)
                    {
                        m_areacapture.givingpoints();
                    }
                }
                if ((DateTime.Now - t_markersend).TotalSeconds >= markersend)//refleshui 1 SEC
                {

                    
                    t_markersend = DateTime.Now;
                    if (GameActive == true)
                    {


                        EACProject.Instance.sendandupdateplayerdecalmark();

                    }
                }
                if ((DateTime.Now - Check).TotalSeconds >= playercheck)//refleshui 1 SEC
                {


                    Check = DateTime.Now;
                    if (GameActive == true)
                    {
                        if (BluePoints == 100 || RedPoints == 100 || GreenPoints == 100)
                        {
                            EACProject.Instance.creatingnewgame();
                        }
                        EACProject.Instance.areaplayercheck();

                        EACProject.Instance.refleshui();

                    }
                }


            }
            
        }
        public class RealTimeTick : MonoBehaviour
        {

            public DateTime Check;
            public DateTime t_AreaCheck;
            double asec = 3;
            double GameRestart = 30;

            public void Awake()
            {
                Check = DateTime.Now;
                t_AreaCheck = DateTime.Now;
                UnturnedChat.Say("tick1");

            }


            public void FixedUpdate()
            {
                if ((DateTime.Now - t_AreaCheck).TotalSeconds >= asec)//AREA
                {

                    t_AreaCheck = DateTime.Now;
                }

            }

        }//Game restart more complicated
        GameObject obj;

        protected override void Load()
        {
            Logger.Log("TEST");
            Logger.Log("TEST");
            Logger.Log("TEST");
            Logger.Log("TEST");
            Logger.Log("TEST");
            Instance = this;

            U.Events.OnPlayerConnected += playerjoined;
            U.Events.OnPlayerDisconnected += playerjoined;
            U.Events.OnPlayerDisconnected += playerdisconnected;

            UnturnedPlayerEvents.OnPlayerRevive += onplayerrespawn;
            UnturnedPlayerEvents.OnPlayerDead += onplayerdead;
            
            EffectManager.onEffectButtonClicked = (EffectManager.EffectButtonClickedHandler)Delegate.Combine(EffectManager.onEffectButtonClicked, new EffectManager.EffectButtonClickedHandler(buttonclicked));


        }

        Modules.TeamSelection teamSelection = new Modules.TeamSelection();
        Modules.AreaCapture areaCapture = new Modules.AreaCapture();
        bool firstplayerjoined = false;
        public void playerjoined(UnturnedPlayer player)
        {

            player.Player.teleportToLocation(Lobby, 0);

            EffectManager.sendUIEffect(34652, 21, player.Player.channel.owner.transportConnection, true);
            if (firstplayerjoined == false)
            {
                firstplayerjoined = true;
                obj = new GameObject("Name");
                obj.AddComponent<TickSec>();
            }
            sendlobby(player);

            scoreboardui(player);

            SendTeamui(player);


        }

        public void playerdisconnected(UnturnedPlayer player)
        {
           
        }

        public void onplayerdead(UnturnedPlayer unturnedPlayer, Vector3 pos)
        {
            
        }

        public void onplayerrespawn(UnturnedPlayer player, Vector3 position, byte angle)
        {
            UnturnedChat.Say("Player respawned");
            if(player.Player.quests.groupID.ToString() == "76561198205339427")//BLUE
            {
                player.Player.teleportToLocation(USABase, 0);
                return;
            }
            if (player.Player.quests.groupID.ToString() == "76561198244016116")//RED
            {
                player.Player.teleportToLocation(RUSBase, 180);
                return;
            }
            else
            {
                player.Player.teleportToLocation(Lobby, 90);
            }
        }
        protected override void Unload()
        {
            GameObject.Destroy(obj);
            U.Events.OnPlayerConnected -= playerjoined;
            U.Events.OnPlayerDisconnected -= playerjoined;
            U.Events.OnPlayerDisconnected -= playerdisconnected;
        }
        public List<UnturnedPlayer> blue_areaplayer =  new List<UnturnedPlayer>();
        public List<UnturnedPlayer> red_areaplayer = new List<UnturnedPlayer>();
        public List<UnturnedPlayer> green_areaplayer = new List<UnturnedPlayer>();

        public void buttonclicked(Player caller, string buttonname)
        {


            UnturnedPlayer player = UnturnedPlayer.FromPlayer(caller);
            if (buttonname == "Usa")
            {
                Sendteam(player, "Usa");

                EffectManager.askEffectClearByID(34650, player.Player.channel.owner.transportConnection);
                player.Player.disablePluginWidgetFlag(EPluginWidgetFlags.Modal);

            }
            if (buttonname == "Russia")
            {
                Sendteam(player, "Russia");

                EffectManager.askEffectClearByID(34650, player.Player.channel.owner.transportConnection);
                player.Player.disablePluginWidgetFlag(EPluginWidgetFlags.Modal);

            }   
            else
            {
                UnturnedChat.Say(buttonname);

            }
        }
        public void areaplayercheck()
        {
            float Central_x = -60f;
            float Central_z = 620f;
            float threshold = 80f;
            blue_areaplayer.Clear();
            red_areaplayer.Clear();
            green_areaplayer.Clear();
            foreach (SteamPlayer client in Provider.clients)
            {
                
                UnturnedPlayer player = UnturnedPlayer.FromSteamPlayer(client);
                if(player.Player.transform.position.x >= Central_x - threshold && player.Player.transform.position.x <= Central_x + threshold && player.Player.transform.position.z >= Central_z - threshold && player.Player.transform.position.z <= Central_z + threshold && !player.Dead)
                {
                    if (player.Player.quests.groupID == (CSteamID)76561198205339427)//USA
                    {
                        blue_areaplayer.Add(player);
                        refleshui();
                    }
                    if (player.Player.quests.groupID == (CSteamID)76561198244016116)//RUS
                    {
                        red_areaplayer.Add(player);
                        refleshui();
                    }
                    if(player.Player.quests.groupID != (CSteamID)76561198244016116 && player.Player.quests.groupID != (CSteamID)76561198205339427)
                    {

                        UnturnedChat.Say(player, "Your not in group!", Color.red);
                    }

                }
                


            }
        }
        public void creatingnewgame()
        {

            BluePoints = 0;
            RedPoints = 0;
            GreenPoints = 0;
            GameObject.Destroy(obj);
            foreach (SteamPlayer client in Provider.clients)
            {
                UnturnedPlayer player = UnturnedPlayer.FromSteamPlayer(client);
                sendlobby(player);
            }
            obj = new GameObject("Name");
            obj.AddComponent<TickSec>();


        }
        public void sendlobby(UnturnedPlayer player)
        {
            GroupManager.getOrAddGroup(groupnewid, "LobbyGroup", out bool wascreated);
            player.Player.quests.ServerAssignToGroup(groupnewid, EPlayerGroupRank.MEMBER, true);
            player.Player.teleportToLocation(Lobby, 90);
            SendTeamui(player);
        }
        
        public void SendTeamui(UnturnedPlayer player)
        {

            EffectManager.sendUIEffect(34650, 22, player.Player.channel.owner.transportConnection, true, "Hello " + player.DisplayName);
            player.Player.enablePluginWidgetFlag(EPluginWidgetFlags.Modal);
        }
        public void Sendteam(UnturnedPlayer player, string team)
        {
            if(team == "Usa")
            {
                teamSelection.usa(player);
                player.Player.teleportToLocation(USABase, 0);


            }
            if (team == "Russia")
            {
                teamSelection.rus(player);
                player.Player.teleportToLocation(RUSBase, 180);



            }
        }


        public void scoreboardui(UnturnedPlayer player)
        {
            EffectManager.sendUIEffect(34653, 24, player.Player.channel.owner.transportConnection, true);
        }


        public void refleshui()
        {
            BluePlayerCount = 0f;
            RedPlayerCount = 0f;
            foreach (SteamPlayer client in Provider.clients)
            {

                UnturnedPlayer player = UnturnedPlayer.FromSteamPlayer(client);
                
                if (player.Player.quests.groupID == (CSteamID)76561198205339427)//USA
                {
                    BluePlayerCount++;
                }
                if (player.Player.quests.groupID == (CSteamID)76561198244016116)//RUS
                {
                    RedPlayerCount++;
                }

                EffectManager.sendUIEffectText(24, player.SteamPlayer().transportConnection, true, "BlueforTeam", BluePlayerCount.ToString());// bottom players
                EffectManager.sendUIEffectText(24, player.SteamPlayer().transportConnection, true, "OpforTeam", RedPlayerCount.ToString());
                EffectManager.sendUIEffectText(24, player.SteamPlayer().transportConnection, true, "IndTeam", GreenPlayerCount.ToString());
                EffectManager.sendUIEffectText(24, player.SteamPlayer().transportConnection, true, "BlueArea", BluePoints.ToString());//points
                EffectManager.sendUIEffectText(24, player.SteamPlayer().transportConnection, true, "RedArea", RedPoints.ToString());
                EffectManager.sendUIEffectText(24, player.SteamPlayer().transportConnection, true, "GreenArea", "3");
                EffectManager.sendUIEffectText(24, player.SteamPlayer().transportConnection, true, "blue_areaplayer", blue_areaplayer.Count.ToString());//area players
                EffectManager.sendUIEffectText(24, player.SteamPlayer().transportConnection, true, "red_areaplayer", red_areaplayer.Count.ToString());
                EffectManager.sendUIEffectText(24, player.SteamPlayer().transportConnection, true, "green_areaplayer", green_areaplayer.Count.ToString());
                //EffectManager.sendUIEffectVisibility("BLUE EFFECT KEY", player.SteamPlayer().transportConnection, true, "BLUE MARK", true);
                


            }
            
        }
        public static void Effecttest(Vector3 position, ITransportConnection transportConnection, EffectAsset effectAsset, float scale = 1f)
        {
            TriggerEffectParameters effectParameters = new TriggerEffectParameters(effectAsset);
            effectParameters.reliable = true;
            effectParameters.SetRelevantPlayer(transportConnection);
            effectParameters.position = position;
            effectParameters.SetDirection(Vector3.forward);
            effectParameters.SetUniformScale(scale);
            
            
            EffectManager.triggerEffect(effectParameters);
        }
        public void sendandupdateplayerdecalmark()
        {
            foreach (SteamPlayer client in Provider.clients)
            {
                UnturnedPlayer player = UnturnedPlayer.FromSteamPlayer(client);
                EffectManager.askEffectClearByID(34654, player.CSteamID);



                var assest = Assets.find(EAssetType.EFFECT, 34654);

                Effecttest(player.Position, player.Player.channel.owner.transportConnection, (EffectAsset)assest, 1);
            }
        }
        
    }
}
