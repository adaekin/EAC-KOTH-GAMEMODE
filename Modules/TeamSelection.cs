using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EACProject.Commands;
using Rocket.Core.Logging;
using Rocket.Core.Plugins;
using Rocket.Core.Steam;
using Rocket.Unturned;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Events;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;
using UnityEngine;

namespace EACProject.Modules
{

    public class TeamSelection
    {
        CSteamID groupAID = (CSteamID)76561198244016116;
        CSteamID groupBID = (CSteamID)76561198205339427;
        public void rus(UnturnedPlayer player)
        {
            
            UnturnedChat.Say(groupAID.ToString());
            var group = GroupManager.getOrAddGroup(groupAID, "RUS", out bool wascreated);

            if (!wascreated)
            {
                group.name = "RUS";
                GroupManager.sendGroupInfo(group);
            }
            player.Player.quests.ServerAssignToGroup(groupAID, EPlayerGroupRank.MEMBER, true);
            EACProject.groupBMEM = group.members;
        }
        public void usa(UnturnedPlayer player)
        {
            

                UnturnedChat.Say(groupBID.ToString());
            var group = GroupManager.getOrAddGroup(groupBID, "USA", out bool wascreated);

            if (!wascreated)
            {
                group.name = "USA";
                GroupManager.sendGroupInfo(group);
            }
            player.Player.quests.ServerAssignToGroup(groupBID, EPlayerGroupRank.MEMBER, true);
            EACProject.groupAMEM = group.members;

        }

    }
}
