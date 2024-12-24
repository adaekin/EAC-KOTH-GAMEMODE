using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rocket.API;
using Rocket.Core.Logging;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using Rocket.Unturned.Events;
using SDG.Unturned;
using Steamworks;
using UnityEngine;

namespace EACProject.Commands
{
    public class team : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Both;

        public string Name => "leavegroup";

        public string Help => "";

        public string Syntax => "";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string>();

        public static team Instance { get; set; }

        public void Execute(IRocketPlayer caller, string[] command)
        {
            UnturnedPlayer player = (UnturnedPlayer)caller;


            //player.Player.quests.askLeaveGroup(player.Player.quests.groupID);
            CSteamID groupID = player.Player.quests.groupID;
            if (groupID == CSteamID.Nil)
            {
                return;
            }
            UnturnedChat.Say("1", Color.red);
            player.Player.quests.ReceiveKickFromGroup(groupID);

        }
    }
}
