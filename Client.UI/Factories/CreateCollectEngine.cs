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
                case CollectDataEnum.Interface1:
                    factory = new InterfaceId1();

                    break;
                case CollectDataEnum.Interface2:
                    factory = new InterfaceId2();

                    break;
                case CollectDataEnum.Interface3:
                    factory = new InterfaceId3();

                    break;
                case CollectDataEnum.Interface4:
                    factory = new InterfaceId4();

                    break;
                case CollectDataEnum.Interface5:
                    factory = new InterfaceId5();

                    break;
                case CollectDataEnum.Interface6:
                    factory = new InterfaceId6();

                    break;
                default:
                    break;
            }

            if (factory == null)
            {
                throw new Exception($"未找到{nameof(collectDataEnum)}接口数据采集引擎");
            }

            return factory;
        }
    }
}
