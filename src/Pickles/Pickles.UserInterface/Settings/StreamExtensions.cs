﻿// #region License
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
using System.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace Pickles.UserInterface.Settings
{
  internal static class StreamExtensions
  {
    internal static void Serialize<T>(this Stream stream, T item)
    {
      using (XmlWriter writer = XmlWriter.Create(stream, new XmlWriterSettings { Indent = true }))
      {
        new DataContractSerializer(typeof(T)).WriteObject(writer, item);
      }
    }

    internal static T Deserialize<T>(this Stream stream)
    {
      T result;

      using (XmlReader reader = XmlReader.Create(stream))
      {
        result = (T)new DataContractSerializer(typeof(T)).ReadObject(reader);
      }

      return result;
    }
  }
}