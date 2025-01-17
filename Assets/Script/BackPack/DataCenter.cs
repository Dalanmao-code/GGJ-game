using System;
using System.Collections;
using System.Collections.Generic;


namespace Core
{
    public static class DataCenter
    {
        private static ItemDataArray _ITEM_DATA_ARRAY;

        static DataCenter()
        {
            Init();
        }

        public static void Init()
        {
            _ITEM_DATA_ARRAY = AssetsLibrary.LoadResource<ItemDataArray>("Data/ItemDataArray");
            
        }

        #region 道具
        /// <summary>
        /// 使用关键字进行索引，传入key和value，寻找合适的对象
        /// </summary>
        /// <param name="value">用于查询的值</param>
        /// <param name="key">用于在道具类中查询的字段名</param>
        /// <returns>对应类型的道具模板</returns>
        public static ItemData GetItemDataByKeyValue(string key, string value)
        {
            List<ItemData> items = _ITEM_DATA_ARRAY.itemDataList;
            return (ItemData)GetItemByKeyValue(key, value, items);
        }

        /// <summary>
        /// 根据首个字段进行索引，适用于首个字段为主ID的对象 
        /// </summary>
        /// <param name="id">用于查询的id</param>
        /// <returns>道具模板实例</returns>
        public static ItemData GetItemDataByID(int id)
        {
            ItemData result = null;
            foreach (ItemData item in _ITEM_DATA_ARRAY.itemDataList)
            {
                if (item.id == id)
                {
                    result = item;
                    return result;
                }
            }
            return null;
        }

        /// <summary>
        /// 封装查询方法
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="key">字段名</param>
        /// <param name="Data">数据对象集合</param>
        private static object GetItemByKeyValue(string key, string value, IEnumerable<object> Data)
        {
            foreach (var item in Data)
            {
                //由于反射的性能开销较大，注意使用的时候尽量避免反复调用
                var field = item?.GetType().GetField(key);
                if (field != null && field.GetValue(item)?.ToString() == value)
                {
                    return item;
                }
            }
            Console.WriteLine("未找到匹配对象");
            return null;
        }
        #endregion

    }
}

