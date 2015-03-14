using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Buffalo.Kernel.Defaults;
using Buffalo.DB.CommBase.DataAccessBases;
using Buffalo.Kernel;
using Buffalo.DB.DataBaseAdapter;
using Buffalo.DB.DbCommon;

namespace Buffalo.DB.BQLCommon.BQLConditionCommon
{
    /// <summary>
    /// ֵ������
    /// </summary>
    public class BQLValueTypeItem:BQLValueItem
    {
        private object itemValue;

        public object ItemValue
        {
            get { return itemValue; }
            set { itemValue = value; }
        }

        internal override bool IsNullValue()
        {
            return itemValue==null;
        }
        /// <summary>
        /// ֵ������
        /// </summary>
        /// <param name="itemValue">ֵ</param>
        public BQLValueTypeItem(object itemValue)
        {

            this.itemValue = itemValue;

        }
        ///// <summary>
        ///// �����ֵ
        ///// </summary>
        //public object ItemValue 
        //{
        //    get 
        //    {
        //        return itemValue;
        //    }
        //}

        internal override void FillInfo(KeyWordInfomation info)
        {
            if (_valueDbType == DbType.Object)
            {
                _valueDbType = DefaultType.ToDbType(itemValue.GetType());//��ƥ������
            }
        }

        internal override string DisplayValue(KeyWordInfomation info)
        {
            //string ret = null;
            
            if (info.ParamList != null && _valueDbType != DbType.Object && !info.OutPutModle) 
            {
                DBParameter dbPrm=info.ParamList.NewParameter(_valueDbType, itemValue,info.DBInfo);
                return dbPrm.ValueName;
            }

            return FormatValueType(info);
        }

        


        /// <summary>
        /// ��ʽ��ֵ��
        /// </summary>
        /// <param name="valueItem"></param>
        /// <returns></returns>
        private string FormatValueType(KeyWordInfomation info)
        {
            Type valueDataType = null;
            if (itemValue != null) 
            {
                valueDataType = itemValue.GetType();
            }
            if (DefaultType.EqualType(valueDataType , DefaultType.StringType) || DefaultType.EqualType(valueDataType , DefaultType.GUIDType))
            {
                return DataAccessCommon.FormatValue(itemValue, DbType.String, info.DBInfo);
            }
            else if (DefaultType.EqualType(valueDataType , DefaultType.DateTimeType))
            {
                return DataAccessCommon.FormatValue(itemValue, DbType.DateTime, info.DBInfo);
            }
            else if (DefaultType.EqualType(valueDataType , DefaultType.BytesType))
            {
                return DataAccessCommon.FormatValue(itemValue, DbType.Binary, info.DBInfo);
            }

            else if (DefaultType.EqualType(valueDataType , DefaultType.BooleanType) )
            {
                return DataAccessCommon.FormatValue(itemValue, DbType.Boolean, info.DBInfo);
            }
            else if (DefaultType.EqualType(valueDataType, DefaultType.GUIDType))
            {
                return DataAccessCommon.FormatValue(itemValue, DbType.Guid, info.DBInfo);
            }
            else if (valueDataType == null) 
            {
                return "null";
            }
            return itemValue.ToString();
        }
    }
}