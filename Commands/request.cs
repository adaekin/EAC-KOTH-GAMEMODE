using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rocket.API;
using Rocket.Unturned.Player;
using Rocket.Core.Logging;
using Rocket.Unturned.Chat;
using UnityEngine;
using SDG.Unturned;
using SDG.Framework.Utilities;

namespace EACProject.Commands
{
    public class request : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "request";

        public string Help => "";

        public string Syntax => "";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string>();

        public void Execute(IRocketPlayer caller, string[] command)
        {

            Rocket.Core.Logging.Logger.Log("TEST");
            EACProject.Instance.LoadPlugin(); //Instanceten voidleri çekebiliyorum.
            

            UnturnedPlayer player = (UnturnedPlayer)caller;

            var look = player.Player.look;
            if (PhysicsUtility.raycast(new Ray(look.aim.position, look.aim.forward), out RaycastHit hit, Mathf.Infinity, RayMasks.BARRICADE))
            {
                InteractableSign sign1 = hit.transform.GetComponent<InteractableSign>();
                if(sign1 != null)
                {
                    InteractableSign sign = sign1;
                    string tex2 = sign1.text;
                    UnturnedChat.Say(tex2);
                    //BarricadeManager.ServerSetSignText(sign, "TestSign");
                    if (tex2 != null)
                    {
                        if(tex2.Length < 1)
                        {
                            UnturnedChat.Say("sign not invaild");
                            return;
                        }
                        
                        var kit = EACProject.Instance.Configuration.Instance.Kits.FirstOrDefault(x => x.Name == sign1.text);
                        if (kit == null)
                        {
                            UnturnedChat.Say("kit not found");
                            return;
                        }
                        var playerinv = player.Inventory;
                        for (byte page = 0; page < PlayerInventory.PAGES; page++)
                        {
                            if (page == PlayerInventory.AREA)
                                continue;
                            var count = playerinv.getItemCount(page);
                            for (byte index = 0; index < count; index++)
                            {
                                playerinv.removeItem(page, 0);
                            }
                        }



                        foreach (var item in kit.Items)
                        {
                            player.GiveItem(item, 1);
                        }
                    }
                    else
                    {
                        UnturnedChat.Say("Tabela boş");
                        return;
                    }
                }
                else
                {
                    UnturnedChat.Say("Geçersiz Tabela");
                }
            }
            else
            {
                UnturnedChat.Say("Obje bulunamadı");
            }
        }
    }
}
