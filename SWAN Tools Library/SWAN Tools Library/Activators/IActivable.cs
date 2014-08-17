
namespace swantiez.unity.tools.activators
{
    public interface IActivable
    {
        void link(AbstractActivator activator);
        void activate(AbstractActivator activator);
        void deactivate(AbstractActivator activator);
    }
}
