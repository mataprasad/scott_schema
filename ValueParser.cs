using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KX.Colegio.Util.Utility
{
   public static class ValueParser
    {
       public static T GetParsedValue<T>(object obj)
       {
           Object returnVal = new object();
           Type type = typeof(T);
           switch (type.Name)
           {
               case "String":
                   {
                       string output = string.Empty;
                       if (obj != DBNull.Value)
                       {
                           if (string.IsNullOrEmpty(obj.ToString()))
                           {
                               returnVal = (object)output;
                           }
                           else
                           {
                               returnVal = obj.ToString().Trim();
                           }
                       }
                       else
                       {
                           returnVal = (object)output;
                       }
                       break;
                   }
               case "Int32":
                   {
                       int output = 0;
                       int.TryParse(obj.ToString(), out output);
                       returnVal = (object)output;
                       break;
                   }
               case "DateTime":
                   {
                       DateTime output = DateTime.MinValue;
                       DateTime.TryParse(obj.ToString(), out output);
                       returnVal = (object)output;
                       break;
                   }
               case "Single":
                   {
                       Single output = 0.0f;
                       Single.TryParse(obj.ToString(), out output);
                       returnVal = (object)output;
                       break;
                   }
               case "Double":
                   {
                       Double output = 0.0;
                       Double.TryParse(obj.ToString(), out output);
                       returnVal = (object)output;
                       break;
                   }
               case "Decimal":
                   {
                       Decimal output = decimal.MinValue;
                       Decimal.TryParse(obj.ToString(), out output);
                       returnVal = (object)output;
                       break;
                   }
               case "Int16":
                   {
                       Int16 output = 0;
                       Int16.TryParse(obj.ToString(), out output);
                       returnVal = (object)output;
                       break;
                   }
               case "Int64":
                   {
                       Int64 output = 0;
                       Int64.TryParse(obj.ToString(), out output);
                       returnVal = (object)output;
                       break;
                   }
               case "Boolean":
                   {
                       bool output = false;
                       bool.TryParse(obj.ToString(), out output);
                       returnVal = (object)output;
                       break;
                   }
               default:
                   {
                       object output = null;
                       if (obj != DBNull.Value)
                       {
                           if (obj == null)
                           {
                               returnVal = (object)output;
                           }
                           else
                           {
                               if (type.GetGenericArguments() != null)
                               {
                                   returnVal = Convert.ChangeType(obj, Type.GetType(type.GetGenericArguments()[0].FullName));
                               }
                               else
                               {
                                   returnVal = obj;
                               }
                           }
                       }
                       else
                       {
                           returnVal = (object)output;
                       }
                       break;
                   }
           }
           return (T)(object)returnVal;
       }
    }
}
