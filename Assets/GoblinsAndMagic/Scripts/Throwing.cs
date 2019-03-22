using Assets.GoblinsAndMagic.Scripts.Data;
using UnityEngine;

namespace Assets.GoblinsAndMagic.Scripts
{
    /// <summary>
    /// Defines basic behaviour for throwable objects - arrows, fireballs and other
    /// </summary>
    public abstract class Throwing : MonoBehaviour
    {
        /// <summary>
        /// Speed in global space
        /// </summary>
        public float Speed;

        protected float Direction;
        protected CreatureParams ShooterParams;
        protected CreatureState ShooterState;

        /// <summary>
        /// Default initializer
        /// </summary>
        /// <param name="direction">Object direction</param>
        /// <param name="shooter">Object owner</param>
        public virtual void Initialize(float direction, Creature shooter)
        {
            Direction = Mathf.Sign(direction);
            ShooterParams = shooter.Params;
            ShooterState = shooter.State;
            Destroy(gameObject, 10);
        }

        public abstract void Hit(Creature creature);

        public void Update()
        {
            transform.position += new Vector3(Direction * Speed * Time.deltaTime, 0);
        }

        /// <summary>
        /// Defines what happens when object hits something
        /// </summary>
        /// <param name="target">Hited collider</param>
        public void OnTriggerEnter(Collider target)
        {
            if (target.isTrigger) return;

            var creature = target.GetComponent<Creature>();

            if (creature == null)
            {
                if (transform.position.z > target.bounds.min.z && transform.position.z < target.bounds.max.z)
                {
                    Hit(null);
                }
            }
            else if (creature.State.Team != ShooterState.Team && creature.State.Hp > 0)
            {
                Hit(creature);
            }
        }
    }
}