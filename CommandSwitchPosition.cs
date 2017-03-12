using System;
using UnityEngine;
using Rocket.API;
using Rocket.Unturned.Player;
using Rocket.Unturned;
using Rocket.Unturned.Commands;
using Rocket.Unturned.Chat;
using System.Collections.Generic;
using Rocket.API.Extensions;

namespace SwitchPosition
{
    public class CommandSwitchpos : IRocketCommand
    {
        public SwitchPosPlugin Instance;
        public AllowedCaller AllowedCaller
        {
            get { return AllowedCaller.Both; }
        }

        public string Name
        {
            get { return "switchposition"; }
        }

        public string Help
        {
            get { return "Switch position with another player"; }
        }

        public string Syntax
        {
            get { return "<player1> <player2> or just <player>"; }
        }

        public List<string> Aliases
        {
            get { return new List<string>(){"switchpos", "switchlocation", "switchloc", "swipos"}; }
        }

        public void Execute(IRocketPlayer caller, string[] command)
        {
            if (command.Length == 1)
            {
                string victim = command.GetStringParameter(0);
                UnturnedPlayer vPlayer = UnturnedPlayer.FromName(victim);
                UnturnedPlayer cPlayer = (UnturnedPlayer)caller;
                SwitchPosPlugin.SwitchPositionDirect(caller, vPlayer, cPlayer);
                //Vector3 positionCaller = new Vector3(cPlayer.Position.x, cPlayer.Position.y, cPlayer.Position.z);
                //Vector3 positionVictim = new Vector3(vPlayer.Position.x, vPlayer.Position.y, vPlayer.Position.z);
                //float rotationCaller = cPlayer.Rotation;
                //float rotationVictim = vPlayer.Rotation;
                //cPlayer.Teleport(positionVictim, rotationVictim);
                //vPlayer.Teleport(positionCaller, rotationCaller);
                //UnturnedChat.Say(cPlayer, "Done.");
            }
            else if (command.Length == 2)
            {
                //string p1a = command.GetStringParameter(0);
                //string p2a = command.GetStringParameter(1);
                //UnturnedPlayer p1 = UnturnedPlayer.FromName(p1a);
                //UnturnedPlayer p2 = UnturnedPlayer.FromName(p2a);
                //Vector3 positionP1 = new Vector3(p1.Position.x, p1.Position.y, p1.Position.z);
                //Vector3 positionP2 = new Vector3(p2.Position.x, p2.Position.y, p2.Position.z);
                //float rotationP1 = p1.Rotation;
                //float rotationP2 = p2.Rotation;
                //p1.Teleport(positionP2, rotationP2);
                //p2.Teleport(positionP1, rotationP1);
                string p1a = command.GetStringParameter(0);
                string p2a = command.GetStringParameter(1);
                UnturnedPlayer p1 = UnturnedPlayer.FromName(p1a);
                UnturnedPlayer p2 = UnturnedPlayer.FromName(p2a);
                SwitchPosPlugin.SwitchPositionP1P2(caller, p1, p2);
            }
            else UnturnedChat.Say(caller, "Check syntax.", Color.red);
        }

        public List<string> Permissions
        {
            get
           {
               return new List<string>
              {
                  "leite.switchposition"
              };
           }
        }
    }
}
