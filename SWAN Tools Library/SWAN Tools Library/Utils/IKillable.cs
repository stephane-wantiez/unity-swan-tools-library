namespace swantiez.unity.tools.utils
{
    public interface IKillable
    {
        event DelegateUtils.OnFloatEvent OnDamageEvents;
        event DelegateUtils.OnSimpleEvent OnKillEvents;

        float getHealth();
        bool isAlive();
        bool isDead();
        void damage(float damageValue);
        void kill();
    }
}