// #region License
// 
// 
// /*
//     Copyright [2011] [Jeffrey Cameron]
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//        http://www.apache.org/licenses/LICENSE-2.0
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
// */
// #endregion

using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;

namespace PicklesDoc.Pickles.UserInterface.Mvvm
{
  public static class EventHandlerExtensions
  {
    public static void Raise(this EventHandler handler, object sender, EventArgs e)
    {
      if (handler != null)
      {
        handler(sender, e);
      }
    }

    public static void Raise<TValue>(
      this PropertyChangedEventHandler handler, object sender, Expression<Func<TValue>> selector)
    {
      if (handler != null)
      {
        handler(sender, new PropertyChangedEventArgs(GetProperty(selector).Name));
      }
    }

    internal static PropertyInfo GetProperty(Expression expression)
    {
      if (expression is LambdaExpression)
      {
        expression = ((LambdaExpression)expression).Body;
      }
      switch (expression.NodeType)
      {
        case ExpressionType.MemberAccess:
          return (PropertyInfo)((MemberExpression)expression).Member;
        default:
          throw new InvalidOperationException("Expression does not contain a property.");
      }
    }
  }
}
 