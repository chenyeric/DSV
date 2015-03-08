﻿// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

using System.IO;
using System.Net.Http.Mocks;
using Microsoft.TestCommon;
using Moq;
using Moq.Protected;

namespace System.Net.Http.Internal
{
    public class NonClosingDelegatingStreamTest
    {
        [Fact]
        public void NonClosingDelegatingStream_Dispose()
        {
            // Arrange
            Mock<Stream> mockInnerStream = new Mock<Stream>() { CallBase = true };
            MockNonClosingDelegatingStream mockStream = new MockNonClosingDelegatingStream(mockInnerStream.Object);

            // Act
            mockStream.Dispose();

            // Assert 
            mockInnerStream.Protected().Verify("Dispose", Times.Never(), true);
            mockInnerStream.Verify(s => s.Close(), Times.Never());
        }

        [Fact]
        public void NonClosingDelegatingStream_Close()
        {
            // Arrange
            Mock<Stream> mockInnerStream = new Mock<Stream>() { CallBase = true };
            MockNonClosingDelegatingStream mockStream = new MockNonClosingDelegatingStream(mockInnerStream.Object);

            // Act
            mockStream.Close();

            // Assert 
            mockInnerStream.Protected().Verify("Dispose", Times.Never(), true);
            mockInnerStream.Verify(s => s.Close(), Times.Never());
        }
    }
}
