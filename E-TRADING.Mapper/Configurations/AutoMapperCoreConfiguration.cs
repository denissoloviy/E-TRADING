using E_TRADING.Mapper.Profiles;

namespace E_TRADING.Mapper.Configurations
{
    public class AutoMapperCoreConfiguration
    {
        public static void Configure()
        {
            global::AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new CoreProfile());
            });
        }
    }
}
