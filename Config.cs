using Rocket.API;

namespace SwitchPosition
{
    public class SwitchPosConfig : IRocketPluginConfiguration
    {
        public bool SendDoneMessage;
        public bool SwitchCallerPlayer;
        public bool SendMessageToVictim;
        public bool SendMessageToCaller;
        public bool SendMessageToEveryone;
        public bool AlertCaller;
        public string MessagesColorRGB;
        public string AditionalInfo;

        public void LoadDefaults()
        {
            AlertCaller = true;
            SwitchCallerPlayer = true;
            SendMessageToVictim = true;
            SendMessageToCaller = true;
            SendMessageToEveryone = false;
            MessagesColorRGB = "red";
            AditionalInfo = "Please don't abuse with it ok? :D ~Leite)";
        }
    }
}