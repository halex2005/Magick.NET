﻿// Copyright 2013-2020 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

#if !NETSTANDARD1_3

using System.Drawing;

namespace ImageMagick
{
    /// <summary>
    /// Interface for a class that can be used to create <see cref="IMagickImage"/>, <see cref="IMagickImageCollection"/> or <see cref="IMagickImageInfo"/> instances.
    /// </summary>
    public partial interface IMagickFactory
    {
        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImage"/>.
        /// </summary>
        /// <param name="bitmap">The bitmap to use.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        /// <returns>A new <see cref="IMagickImage"/> instance.</returns>
        IMagickImage CreateImage(Bitmap bitmap);
    }
}

#endif