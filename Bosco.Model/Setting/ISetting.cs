using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bosco.Utility;
using System.Data;

namespace Bosco.Model.Setting
{
    public interface ISetting
    {
        void ApplySetting();
        ResultArgs SaveSetting(DataTable dtSetting);
        ResultArgs FetchSettingDetails(int UserID);
    }
}
