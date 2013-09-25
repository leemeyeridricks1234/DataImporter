using StructureMap;

namespace Common
{
    public static class IocContainer
    {
        public static T Resolve<T>()
        {
            return ObjectFactory.Container.GetInstance<T>();
        }

        public static T ResolveWithAlias<T>(object alias)
        {
            return ObjectFactory.Container.GetInstance<T>(alias.ToString());
        }
        
        public static void RegisterSingletonInstance<T>(T instance)
        {
            ObjectFactory.Container.Configure(x => x.For<T>().Singleton().Use(instance));
        }

        public static void RegisterInstance<I, T>() where T : I
        {
            ObjectFactory.Container.Configure(x => x.For<I>().Use<T>());
        }

        public static void ResolveWithValue<I, T, TV>(TV value) where T : I
        {
            ObjectFactory.Container.With(value).GetInstance<T>();
        }

        public static T ResolveInstanceWithValueWithAlias<T, TV>(object alias, TV value)
        {
            return ObjectFactory.Container.With(value).GetInstance<T>(alias.ToString());
        }

        public static void RegisterTypeWithAlias<I, T>(object alias) where T : I
        {
            ObjectFactory.Container.Configure(x => x.For<I>().Use<T>().Named(alias.ToString()));
        }
    }
}