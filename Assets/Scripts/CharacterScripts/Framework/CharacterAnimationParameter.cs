using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG.Character
{
    [System.Serializable]
    /// <summary>
    /// 动画参数管理器
    /// </summary>
    public class CharacterAnimationParameter
    {
        public string Run="Run";

        public string Idle="Idle";

        public string Jump="Jump";

        public string Walk="Walk";

        public string Death="Death";

        public string Hurt = "Hurt";

        public string Attack01="Attack01";

        public string Attack02="Attack02";

        public string Attack03="Attack03";

        public string BeAttacked="BeAttacked";

        public string FindTarget = "FindTarget";
    }
}
