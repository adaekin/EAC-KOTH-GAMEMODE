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
    public class join : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Both;

        public string Name => "join";

        public string Help => "";

        public string Syntax => "";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string>();

        public static team Instance { get; set; }

        public void Execute(IRocketPlayer caller, string[] command)
        {
            var groupnewid = GroupManager.generateUniqueGroupID();
            UnturnedPlayer player = (UnturnedPlayer)caller;
            GroupManager.getOrAddGroup(groupnewid, "Testy", out bool wascreated);
            player.Player.quests.ServerAssignToGroup(groupnewid, EPlayerGroupRank.MEMBER, true);


            //Provider.clients.Count(x => player.Player.quests.groupID.m_SteamID == 76561198205339427);
            //var group = GroupManager.getGroupInfo((CSteamID)76561198205339427);
            //UnturnedChat.Say(group.members.ToString());
        }
    }
}
