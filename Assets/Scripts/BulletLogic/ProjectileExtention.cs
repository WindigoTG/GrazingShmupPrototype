namespace GrazingShmup
{
    public static class ProjectileExtention
    {
        public static Projectile Of(this Projectile projectile, IProjectile subprojectile)
        {
            var lowest = GetLowest(projectile);

            lowest.SubFireable = subprojectile;
            return projectile;
        }

        private static Projectile GetLowest(Projectile projectile)
        {
            var lowest = projectile;
            while (projectile != null)
            {
                lowest = projectile;
                projectile = projectile.SubFireable as Projectile;
            }

            return lowest;
        }

        public static Projectile FiredInLine(this Projectile projectile)
        {
            return new Line().Of(projectile);
        }

        public static Projectile FiredInArc(this Projectile projectile)
        {
            return new Arc().Of(projectile);
        }

        public static Projectile FiredInRow(this Projectile projectile)
        {
            return new Row().Of(projectile);
        }

        public static Projectile FiredInDelayedCapsule(this Projectile projectile)
        {
            return new Capsule().Of(projectile);
        }

        public static Projectile FiredInMultishotCapsule(this Projectile projectile)
        {
            return new RepeaterCapsule().Of(projectile);
        }

        public static Projectile FiredInBurstCapsule(this Projectile projectile)
        {
            return new BurstCapsule().Of(projectile);
        }

        public static Projectile FiredInSpinningCapsule(this Projectile projectile)
        {
            return new SpinningCapsule().Of(projectile);
        }
    }
}