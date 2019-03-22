namespace Assets.GoblinsAndMagic.Scripts
{
    /// <summary>
    /// Defines arrow behaviour
    /// </summary>
    public class Arrow : Throwing
    {
        /// <summary>
        /// Initialize direction and shooter
        /// </summary>
        public override void Initialize(float direction, Creature shooter)
        {
            base.Initialize(direction, shooter);

            if (direction < 0)
            {
                transform.Rotate(0, 0, 180);
            }
        }

        /// <summary>
        /// Defines hit behaviour
        /// </summary>
        /// <param name="creature">Hited creature</param>
        public override void Hit(Creature creature)
        {
            if (creature != null)
            {
                creature.GetDamage(ShooterParams.Damage);
                AudioPlayer.PlayEffect("Hit");
            }

            Destroy(gameObject);
        }
    }
}