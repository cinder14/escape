using System;
using System.Collections.Generic;
using System.Linq;

namespace Escape
{
    public class EscapeApp : BaseClass, IEscapeApp
	{
        #region Constructor

        public EscapeApp(ICacheHost cacheHost, IViewPlatform viewPlatform)
            : base("EscapeApp")
        {
            this.CacheHost = cacheHost;
            this.ViewPlatform = viewPlatform;
        }

        #endregion

        #region Properties

        public ICacheHost CacheHost { get; protected set; }
        public IViewPlatform ViewPlatform { get; protected set; }

        #endregion

        public void Initialize()
        {
            
        }

        public string GetLocalizedText(I18NToken token, string defaultText)
        {
            return defaultText; //TODO:Should: Implement shared i18n solution
        }

        public void RescueAdd(Rescue rescue)
        {
            List<Rescue> data = this.CacheHost.CachedDataGet<List<Rescue>>(false, "rescues.data");
            if (data == null)
            {
                data = new List<Rescue>();
            }
            data = this.Sanitize(data);

            data.Add(rescue);
            this.CacheHost.CachedDataSet(false, "rescues.data", data);
        }
        public Rescue RescueGetById(Guid rescue_id)
        {
            List<Rescue> data = this.CacheHost.CachedDataGet<List<Rescue>>(false, "rescues.data");
            if (data == null)
            {
                data = new List<Rescue>();
            }
            return data.FirstOrDefault(x => x.rescue_id == rescue_id);
        }
        public List<Rescue> RescueGetAll()
        {
            List<Rescue> data = this.CacheHost.CachedDataGet<List<Rescue>>(false, "rescues.data");
            if (data == null)
            {
                data = new List<Rescue>();
            }

            return this.Sanitize(data);
        }

        private List<Rescue> Sanitize(List<Rescue> data)
        {
            DateTime now = DateTime.Now;
            return data.Where(x => x.stamp_utc > now).ToList();
        }
	}
}

