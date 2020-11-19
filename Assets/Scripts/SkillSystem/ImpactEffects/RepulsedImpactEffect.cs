using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ARPG.Skill
{
    public class RepulsedImpactEffect : IImpactEffect
    {
        private float mess;
        public void Execute(SkillDeployer deployer)
        {
            //
            foreach (var t in deployer.SkillData.attackTargets)
            {
                Vector3 force = (t.position - deployer.gameObject.transform.position)*10f;
                t.transform.gameObject.GetComponent<Rigidbody2D>().AddForce(force);
            }
        }
        private IEnumerator SpeedDownDelay(Rigidbody2D rigidbody)
        {
            while (rigidbody.velocity.x != 0 || rigidbody.velocity.y != 0)
            {
                yield return new WaitForFixedUpdate();
                if (rigidbody.velocity.x > 0)
                    rigidbody.velocity.Set(rigidbody.velocity.x - mess, rigidbody.velocity.y);
                if (rigidbody.velocity.y > 0)
                    rigidbody.velocity.Set(rigidbody.velocity.x , rigidbody.velocity.y- mess);
            }

        }
    }
}