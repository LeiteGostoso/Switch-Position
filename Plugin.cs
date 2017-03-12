using System;
using Rocket.API;
using Rocket.Core.Plugins;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using UnityEngine;
using Rocket.API.Extensions;
using Rocket.API.Collections;
using Rocket.Core.Extensions;

namespace SwitchPosition
{
    public static class ColorExtensions
    {
        /// <summary>
        /// Convert string to Color (if defined as a static property of Color)
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static Color ToColor(this string color)
        {
            return (Color)typeof(Color).GetProperty(color.ToLowerInvariant()).GetValue(null, null);
        }
    }
    public class SwitchPosPlugin : RocketPlugin<SwitchPosConfig>
    {
        public static SwitchPosPlugin Instance;

        private DateTime lastCalled = DateTime.Now;

        protected override void Load()
        {
            Instance = this;
            Rocket.Core.Logging.Logger.Log("Switchpos loaded.");
        }

        public void FixedUpdate()
        {
        }

        public static void SwitchPositionP1P2(IRocketPlayer caller, UnturnedPlayer p1, UnturnedPlayer p2)
        {
            Vector3 positionP1 = new Vector3(p1.Position.x, p1.Position.y, p1.Position.z);
            Vector3 positionP2= new Vector3(p2.Position.x, p2.Position.y, p2.Position.z);
            float rotationP1 = p1.Rotation;
            float rotationP2 = p2.Rotation;
            p1.Teleport(positionP2, rotationP2);
            p2.Teleport(positionP1, rotationP1);
            if (Instance.Configuration.Instance.SendMessageToEveryone)
            {
                UnturnedChat.Say(Instance.Translate("MessageToEveryone",p1.CharacterName,p2.CharacterName), Instance.Configuration.Instance.MessagesColorRGB.ToColor());
            }

            if (Instance.Configuration.Instance.SendMessageToVictim)
            {
                UnturnedChat.Say(p1, Instance.Translate("MessageToVictim", p2.CharacterName), Instance.Configuration.Instance.MessagesColorRGB.ToColor());
                UnturnedChat.Say(p2, Instance.Translate("MessageToVictim", p1.CharacterName), Instance.Configuration.Instance.MessagesColorRGB.ToColor());
            }

            if (Instance.Configuration.Instance.AlertCaller)
            {
                UnturnedChat.Say(caller, Instance.Translate("AlertToCaller", p1.CharacterName, p2.CharacterName), Instance.Configuration.Instance.MessagesColorRGB.ToColor());
            }
        }

        public static void SwitchPositionDirect(IRocketPlayer caller, UnturnedPlayer vPlayer, UnturnedPlayer cPlayer)
        {
            Vector3 positionCaller = new Vector3(cPlayer.Position.x, cPlayer.Position.y, cPlayer.Position.z);
            Vector3 positionVictim = new Vector3(vPlayer.Position.x, vPlayer.Position.y, vPlayer.Position.z);
            float rotationCaller = cPlayer.Rotation;
            float rotationVictim = vPlayer.Rotation;
            cPlayer.Teleport(positionVictim, rotationVictim);
            vPlayer.Teleport(positionCaller, rotationCaller);
            if (Instance.Configuration.Instance.AlertCaller)
            {
                UnturnedChat.Say(caller, Instance.Translate("AlertToCaller", vPlayer.CharacterName, cPlayer.CharacterName), Instance.Configuration.Instance.MessagesColorRGB.ToColor());
            }
            if (Instance.Configuration.Instance.SendMessageToVictim)
            {
                UnturnedChat.Say(vPlayer, Instance.Translate("MessageToVictim", cPlayer.CharacterName), Instance.Configuration.Instance.MessagesColorRGB.ToColor());
            }
            if (Instance.Configuration.Instance.SendMessageToEveryone)
            {
                UnturnedChat.Say(Instance.Translate("MessageToEveryone", cPlayer.CharacterName, vPlayer.CharacterName), Instance.Configuration.Instance.MessagesColorRGB.ToColor());
            }
        }


        public override TranslationList DefaultTranslations
        {
            get
            {
                return new TranslationList()
                {
                    {"MessageToCaller","You switched your position with {0}."},
                    {"MessageToVictim","{0} switched positions with you."},
                    {"MessageToEveryone","{0} switched positions with {1}."},
                    {"AlertToCaller","Done. [{0} to {1}]."}
                };
            }
        }
        //private override TranslationList DefaultTranslations
        //{
        //    get
        //    {
        //        return new TranslationList()
        //        {
        //            {"MessageToCaller","You switched your position with {0}."},
        //            {"MessageToVictim","{0} switched positions with you."},
        //            {"MessageToEveryone","{0} switched positions with {1}."},
        //            {"AlertToCaller","Done. [{0} to {1}]."}
        //        };
        //    }
        //}
    }
}