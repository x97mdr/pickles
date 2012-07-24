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
using System.IO;

namespace Pickles.UserInterface.Settings
{
  /// <summary>
  /// A serializer class for <see cref="MainModel"/>.
  /// </summary>
  public class MainModelSerializer : IMainModelSerializer
  {
    private const string entitiesNameV1 = "MainSettingsV1";

    private readonly string dataDirectory;

    /// <summary>
    /// Initializes a new instance of the <see cref="MainModelSerializer"/> class.
    /// </summary>
    /// <param name="dataDirectory">The data directory.</param>
    public MainModelSerializer(string dataDirectory)
    {
      this.dataDirectory = dataDirectory;
    }

    /// <summary>
    /// Writes the specified item with the specified id.
    /// </summary>
    /// <param name="item">The item.</param>
    public void Write(MainModel item)
    {
      string path = Path.Combine(this.dataDirectory, entitiesNameV1 + ".xml");

      using (FileStream stream = new FileStream(path, FileMode.Create))
      {
        stream.Serialize(item);
      }
    }

    /// <summary>
    /// Reads the collection.
    /// </summary>
    /// <returns>The collection with data that was written.</returns>
    public MainModel Read()
    {
      MainModel result;

      string path = Path.Combine(this.dataDirectory, entitiesNameV1 + ".xml");

      if (!File.Exists(path))
      {
          return null;
      }

      using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
      {
        result = stream.Deserialize<MainModel>();
      }

      return result;
    }
  }
}