using System;
using System.Collections.Generic;

namespace Escape
{
    public interface IEscapeApp
    {
        void Initialize();
        string GetLocalizedText(I18NToken token, string defaultText);

        void RescueAdd(Rescue rescue);
        List<Rescue> RescueGetAll();
        Rescue RescueGetById(Guid rescue_id);
    }
}

