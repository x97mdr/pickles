//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="MainModelSerializer.cs" company="PicklesDoc">
//  Copyright 2011 Jeffrey Cameron
//  Copyright 2012-present PicklesDoc team and community contributors
//
//
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;
using System.IO.Abstractions;

namespace PicklesDoc.Pickles.UserInterface.Settings
{
  /// <summary>
  /// A serializer class for <see cref="MainModel"/>.
  /// </summary>
  public class MainModelSerializer : IMainModelSerializer
  {
    private const string EntitiesNameV1 = "MainSettingsV1";

    private readonly string dataDirectory;

    private readonly IFileSystem fileSystem;

    /// <summary>
    /// Initializes a new instance of the <see cref="MainModelSerializer"/> class.
    /// </summary>
    /// <param name="dataDirectory">The data directory.</param>
    /// <param name="fileSystem">The wrapper for the file system.</param>
    public MainModelSerializer(string dataDirectory, IFileSystem fileSystem)
    {
        this.dataDirectory = dataDirectory;
        this.fileSystem = fileSystem;
    }

      /// <summary>
    /// Writes the specified item with the specified id.
    /// </summary>
    /// <param name="item">The item.</param>
    public void Write(MainModel item)
    {
      string path = this.fileSystem.Path.Combine(this.dataDirectory, EntitiesNameV1 + ".xml");

      using (var stream = this.fileSystem.File.Create(path))
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

      string path = this.fileSystem.Path.Combine(this.dataDirectory, EntitiesNameV1 + ".xml");

      if (!this.fileSystem.File.Exists(path))
      {
          return null;
      }

      using (var stream = this.fileSystem.File.OpenRead(path))
      {
        result = stream.Deserialize<MainModel>();
      }

      return result;
    }
  }
}