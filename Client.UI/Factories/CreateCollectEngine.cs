using GZKL.Client.UI.Common;
using GZKL.Client.UI.Factories.Collect;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GZKL.Client.UI.Factories
{
    public static class CreateCollectEngine
    {
        public static ICollectFactory Create(CollectDataEnum collectDataEnum)
        {
            ICollectFactory factory = null;

            switch (collectDataEnum)
            {
                case CollectDataEnum.Device1:
                    factory=new Device1CollectEngine();

                    break;
                case CollectDataEnum.Device2:
                    factory = new Device2CollectEngine();

                    break;
                case CollectDataEnum.Device3:


                    break;
                case CollectDataEnum.Device4:


                    break;
                case CollectDataEnum.Device5:


                    break;
                case CollectDataEnum.Device6:


                    break;
                default:
                    break;
            }

            if (factory == null)
            {
                throw new Exception($"未找到{nameof(collectDataEnum)}数据采集引擎");
            }

            return factory;
        }
    }
}
