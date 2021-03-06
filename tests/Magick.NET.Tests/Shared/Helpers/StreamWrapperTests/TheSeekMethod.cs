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

using System;
using System.IO;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    public partial class StreamWrapperTests
    {
        [TestClass]
        public class TheSeekMethod
        {
            [TestMethod]
            public unsafe void ShouldNotThrowExceptionWhenWhenStreamThrowsExceptionDuringSeeking()
            {
                using (var memStream = new MemoryStream())
                {
                    using (var stream = new SeekExceptionStream(memStream))
                    {
                        using (var streamWrapper = StreamWrapper.CreateForReading(stream))
                        {
                            byte[] buffer = new byte[255];
                            fixed (byte* p = buffer)
                            {
                                long count = streamWrapper.Seek(0, IntPtr.Zero, IntPtr.Zero);
                                Assert.AreEqual(-1, count);
                            }
                        }
                    }
                }
            }

            [TestMethod]
            public unsafe void ShouldUseStartPositionOfStreamAsBegin()
            {
                using (var memStream = new MemoryStream())
                {
                    memStream.Position = 42;

                    using (var streamWrapper = StreamWrapper.CreateForReading(memStream))
                    {
                        memStream.Position = 0;

                        var result = streamWrapper.Seek(0, (IntPtr)SeekOrigin.Begin, IntPtr.Zero);

                        Assert.AreEqual(0, result);
                        Assert.AreEqual(42, memStream.Position);
                    }
                }
            }

            [TestMethod]
            public unsafe void ShouldUseStartPositionAsOffset()
            {
                using (var memStream = new MemoryStream())
                {
                    memStream.Position = 42;

                    using (var streamWrapper = StreamWrapper.CreateForReading(memStream))
                    {
                        var result = streamWrapper.Seek(10, (IntPtr)SeekOrigin.Current, IntPtr.Zero);

                        Assert.AreEqual(10, result);
                        Assert.AreEqual(52, memStream.Position);
                    }
                }
            }

            [TestMethod]
            public unsafe void ShouldRemoveStartPositionFromEndOffset()
            {
                using (var memStream = new MemoryStream(new byte[64]))
                {
                    memStream.Position = 42;

                    using (var streamWrapper = StreamWrapper.CreateForReading(memStream))
                    {
                        var result = streamWrapper.Seek(0, (IntPtr)SeekOrigin.End, IntPtr.Zero);

                        Assert.AreEqual(22, result);
                        Assert.AreEqual(64, memStream.Position);
                    }
                }
            }

            [TestMethod]
            public unsafe void ShouldReturnMinusOneForInvalidWhence()
            {
                using (var memStream = new MemoryStream())
                {
                    using (var streamWrapper = StreamWrapper.CreateForReading(memStream))
                    {
                        var result = streamWrapper.Seek(0, (IntPtr)3, IntPtr.Zero);

                        Assert.AreEqual(-1, result);
                    }
                }
            }
        }
    }
}
