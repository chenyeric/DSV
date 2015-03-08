﻿// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Web.Http.OData.Formatter.Serialization.Models;
using System.Xml.Linq;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Library;
using Microsoft.Data.OData;
using Microsoft.TestCommon;
using Moq;

namespace System.Web.Http.OData.Formatter.Serialization
{
    public class ODataCollectionSerializerTests
    {
        IEdmModel _model;
        IEdmEntitySet _customerSet;
        Customer _customer;
        ODataCollectionSerializer _serializer;
        IEdmPrimitiveType _edmIntType;

        public ODataCollectionSerializerTests()
        {
            _model = SerializationTestsHelpers.SimpleCustomerOrderModel();
            _customerSet = _model.FindDeclaredEntityContainer("Default.Container").FindEntitySet("Customers");
            _edmIntType = _model.FindType("Edm.Int32") as IEdmPrimitiveType;
            _customer = new Customer()
            {
                FirstName = "Foo",
                LastName = "Bar",
                ID = 10,
            };

            ODataSerializerProvider serializerProvider = new DefaultODataSerializerProvider();
            _serializer = new ODataCollectionSerializer(
                new EdmCollectionTypeReference(
                    new EdmCollectionType(
                        new EdmPrimitiveTypeReference(_edmIntType, isNullable: false)),
                        isNullable: false), serializerProvider);
        }

        [Fact]
        public void Ctor_ThrowsArgumentNull_EdmType()
        {
            Assert.ThrowsArgumentNull(
                () => new ODataCollectionSerializer(edmType: null, serializerProvider: null),
                "edmType");
        }

        [Fact]
        public void Ctor_ThrowsArgumentNull_SerializerProvider()
        {
            Assert.ThrowsArgumentNull(
                () => new ODataCollectionSerializer(edmType: new Mock<IEdmCollectionTypeReference>().Object, serializerProvider: null),
                "serializerProvider");
        }

        [Fact]
        public void Ctor_ThrowsArgument_EdmType_IfElementTypeIsNull()
        {
            Mock<IEdmCollectionType> collectionType = new Mock<IEdmCollectionType>();
            collectionType.Setup(c => c.ElementType).Returns<IEdmCollectionType>(null);
            IEdmCollectionTypeReference collectionTypeReference = new EdmCollectionTypeReference(collectionType.Object, isNullable: true);

            Assert.ThrowsArgument(
                () => new ODataCollectionSerializer(edmType: collectionTypeReference, serializerProvider: new DefaultODataSerializerProvider()),
                "edmType",
                "The element type of an EDM collection type cannot be null.");
        }

        [Fact]
        public void Ctor_SetsProperty_ElementType()
        {
            // Arrange
            Mock<IEdmCollectionType> collectionType = new Mock<IEdmCollectionType>();
            Mock<IEdmTypeReference> elementType = new Mock<IEdmTypeReference>();
            collectionType.Setup(c => c.ElementType).Returns(elementType.Object);
            IEdmCollectionTypeReference collectionTypeReference = new EdmCollectionTypeReference(collectionType.Object, isNullable: true);

            // Act
            var serializer = new ODataCollectionSerializer(collectionTypeReference, new Mock<ODataSerializerProvider>().Object);

            // Assert
            Assert.Equal(elementType.Object, serializer.ElementType);
        }

        [Fact]
        public void Ctor_SetsProperty_CollectionType()
        {
            // Arrange
            Mock<IEdmCollectionType> collectionType = new Mock<IEdmCollectionType>();
            Mock<IEdmTypeReference> elementType = new Mock<IEdmTypeReference>();
            collectionType.Setup(c => c.ElementType).Returns(elementType.Object);
            IEdmCollectionTypeReference collectionTypeReference = new EdmCollectionTypeReference(collectionType.Object, isNullable: true);

            // Act
            var serializer = new ODataCollectionSerializer(collectionTypeReference, new Mock<ODataSerializerProvider>().Object);

            // Assert
            Assert.Equal(collectionTypeReference, serializer.CollectionType);
        }

        [Fact]
        public void WriteObject_Throws_ArgumentNull_MessageWriter()
        {
            Assert.ThrowsArgumentNull(
                () => _serializer.WriteObject(graph: null, messageWriter: null, writeContext: null),
                "messageWriter");
        }

        [Fact]
        public void WriteObject_Throws_ArgumentNull_WriteContext()
        {
            Assert.ThrowsArgumentNull(
                () => _serializer.WriteObject(graph: null, messageWriter: ODataTestUtil.GetMockODataMessageWriter(), writeContext: null),
                "writeContext");
        }

        [Fact]
        public void WriteObject_WritesValueReturnedFrom_CreateODataCollectionValue()
        {
            // Arrange
            MemoryStream stream = new MemoryStream();
            IODataResponseMessage message = new ODataMessageWrapper(stream);
            ODataMessageWriter messageWriter = new ODataMessageWriter(message);
            Mock<ODataCollectionSerializer> serializer = new Mock<ODataCollectionSerializer>(_serializer.CollectionType, new DefaultODataSerializerProvider());
            ODataSerializerContext writeContext = new ODataSerializerContext { RootElementName = "CollectionName" };
            IEnumerable enumerable = new object[0];
            ODataCollectionValue collectionValue = new ODataCollectionValue { TypeName = "NS.Name", Items = new[] { 0, 1, 2 } };

            serializer.CallBase = true;
            serializer.Setup(s => s.CreateODataCollectionValue(enumerable, writeContext)).Returns(collectionValue).Verifiable();

            // Act
            serializer.Object.WriteObject(enumerable, messageWriter, writeContext);

            // Assert
            serializer.Verify();
            stream.Seek(0, SeekOrigin.Begin);
            XElement element = XElement.Load(stream);
            Assert.Equal("CollectionName", element.Name.LocalName);
            Assert.Equal(3, element.Descendants().Count());
            Assert.Equal(new[] { "0", "1", "2" }, element.Descendants().Select(e => e.Value));
        }

        [Fact]
        public void CreateODataCollectionValue_ThrowsArgumentNull_WriteContext()
        {
            Assert.ThrowsArgumentNull(
                () => _serializer.CreateODataCollectionValue(enumerable: null, writeContext: null),
                "writeContext");
        }

        [Fact]
        public void CreateODataValue_ThrowsArgument_IfGraphIsNotEnumerable()
        {
            object nonEnumerable = new object();
            Mock<ODataSerializerProvider> serializerProvider = new Mock<ODataSerializerProvider>();
            var serializer = new ODataCollectionSerializer(_serializer.CollectionType, serializerProvider.Object);
            serializerProvider.Setup(s => s.GetEdmTypeSerializer(It.IsAny<IEdmTypeReference>())).Returns<IEdmTypeReference>(null);

            Assert.ThrowsArgument(
                () => serializer.CreateODataValue(graph: nonEnumerable, writeContext: new ODataSerializerContext()),
                "graph",
                "The argument must be of type 'IEnumerable'.");
        }

        [Fact]
        public void CreateODataCollectionValue_ThrowsSerializationException_TypeCannotBeSerialized()
        {
            IEnumerable enumerable = new[] { 0 };
            Mock<ODataSerializerProvider> serializerProvider = new Mock<ODataSerializerProvider>();
            var serializer = new ODataCollectionSerializer(_serializer.CollectionType, serializerProvider.Object);
            serializerProvider.Setup(s => s.GetEdmTypeSerializer(It.IsAny<IEdmTypeReference>())).Returns<IEdmTypeReference>(null);

            Assert.Throws<SerializationException>(
                () => serializer.CreateODataCollectionValue(enumerable: enumerable, writeContext: new ODataSerializerContext()),
                "'Edm.Int32' cannot be serialized using the ODataMediaTypeFormatter.");
        }

        [Fact]
        public void CreateODataValue_Calls_CreateODataCollectionValue()
        {
            // Arrange
            ODataCollectionValue oDataCollectionValue = new ODataCollectionValue();
            var collection = new object[0];
            Mock<ODataCollectionSerializer> serializer =
                new Mock<ODataCollectionSerializer>(_serializer.CollectionType, new DefaultODataSerializerProvider());
            serializer.CallBase = true;
            serializer
                .Setup(s => s.CreateODataCollectionValue(collection, It.IsAny<ODataSerializerContext>()))
                .Returns(oDataCollectionValue)
                .Verifiable();

            // Act
            ODataValue value = serializer.Object.CreateODataValue(collection, new ODataSerializerContext());

            // Assert
            serializer.Verify();
            Assert.Same(oDataCollectionValue, value);
        }

        [Fact]
        public void CreateODataCollectionValue_Serializes_AllElementsInTheCollection()
        {
            var oDataValue = _serializer.CreateODataCollectionValue(new int[] { 1, 2, 3 }, new ODataSerializerContext());

            var values = Assert.IsType<ODataCollectionValue>(oDataValue);

            List<int> elements = new List<int>();
            foreach (var item in values.Items)
            {
                elements.Add(Assert.IsType<int>(item));
            }

            Assert.Equal(elements, new int[] { 1, 2, 3 });
        }

        [Fact]
        public void CreateODataCollectionValue_CanSerialize_IEdmObjects()
        {
            // Arrange
            IEdmComplexObject[] collection = new IEdmComplexObject[] { new Mock<IEdmComplexObject>().Object };
            ODataSerializerContext serializerContext = new ODataSerializerContext();
            IEdmComplexTypeReference elementType = new EdmComplexTypeReference(new EdmComplexType("NS", "ComplexType"), isNullable: true);
            IEdmCollectionTypeReference collectionType = new EdmCollectionTypeReference(new EdmCollectionType(elementType), isNullable: false);

            Mock<ODataSerializerProvider> serializerProvider = new Mock<ODataSerializerProvider>();
            Mock<ODataComplexTypeSerializer> elementSerializer = new Mock<ODataComplexTypeSerializer>(MockBehavior.Strict, elementType, serializerProvider.Object);
            serializerProvider.Setup(s => s.GetEdmTypeSerializer(elementType)).Returns(elementSerializer.Object);
            elementSerializer.Setup(s => s.CreateODataComplexValue(collection[0], serializerContext)).Returns(new ODataComplexValue()).Verifiable();

            ODataCollectionSerializer serializer = new ODataCollectionSerializer(collectionType, serializerProvider.Object);

            // Act
            var result = serializer.CreateODataCollectionValue(collection, serializerContext);

            // Assert
            elementSerializer.Verify();
        }

        [Fact]
        public void CreateODataCollectionValue_Returns_EmptyODataCollectionValue_ForNull()
        {
            var oDataValue = _serializer.CreateODataCollectionValue(null, new ODataSerializerContext());

            Assert.NotNull(oDataValue);
            ODataCollectionValue collection = Assert.IsType<ODataCollectionValue>(oDataValue);
            Assert.Empty(collection.Items);
        }

        [Fact]
        public void CreateODataCollectionValue_SetsTypeName()
        {
            // Arrange
            IEnumerable enumerable = new int[] { 1, 2, 3 };
            ODataSerializerContext context = new ODataSerializerContext();

            // Act
            ODataValue oDataValue = _serializer.CreateODataCollectionValue(enumerable, context);

            // Assert
            ODataCollectionValue collection = Assert.IsType<ODataCollectionValue>(oDataValue);
            Assert.Equal("Collection(Edm.Int32)", collection.TypeName);
        }

        [Fact]
        public void AddTypeNameAnnotationAsNeeded_DoesNotAddAnnotation_InDefaultMetadataMode()
        {
            // Arrange
            ODataCollectionValue value = new ODataCollectionValue();

            // Act
            ODataCollectionSerializer.AddTypeNameAnnotationAsNeeded(value, ODataMetadataLevel.Default);

            // Assert
            Assert.Null(value.GetAnnotation<SerializationTypeNameAnnotation>());
        }

        [Fact]
        public void AddTypeNameAnnotationAsNeeded_AddsAnnotation_InJsonLightMetadataMode()
        {
            // Arrange
            string expectedTypeName = "TypeName";
            ODataCollectionValue value = new ODataCollectionValue
            {
                TypeName = expectedTypeName
            };

            // Act
            ODataCollectionSerializer.AddTypeNameAnnotationAsNeeded(value, ODataMetadataLevel.FullMetadata);

            // Assert
            SerializationTypeNameAnnotation annotation = value.GetAnnotation<SerializationTypeNameAnnotation>();
            Assert.NotNull(annotation); // Guard
            Assert.Equal(expectedTypeName, annotation.TypeName);
        }

        [Fact]
        public void AddTypeNameAnnotationAsNeeded_AddsAnnotationWithNull_InJsonLightNoMetadataMode()
        {
            // Arrange
            string expectedTypeName = "TypeName";
            ODataCollectionValue value = new ODataCollectionValue
            {
                TypeName = expectedTypeName
            };

            // Act
            ODataCollectionSerializer.AddTypeNameAnnotationAsNeeded(value, ODataMetadataLevel.NoMetadata);

            // Assert
            SerializationTypeNameAnnotation annotation = value.GetAnnotation<SerializationTypeNameAnnotation>();
            Assert.NotNull(annotation); // Guard
            Assert.Null(annotation.TypeName);
        }

        [Theory]
        [InlineData(TestODataMetadataLevel.Default, false)]
        [InlineData(TestODataMetadataLevel.FullMetadata, true)]
        [InlineData(TestODataMetadataLevel.MinimalMetadata, false)]
        [InlineData(TestODataMetadataLevel.NoMetadata, true)]
        public void ShouldAddTypeNameAnnotation(TestODataMetadataLevel metadataLevel, bool expectedResult)
        {
            // Act
            bool actualResult = ODataCollectionSerializer.ShouldAddTypeNameAnnotation(
                (ODataMetadataLevel)metadataLevel);

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData(TestODataMetadataLevel.FullMetadata, false)]
        [InlineData(TestODataMetadataLevel.NoMetadata, true)]
        public void ShouldSuppressTypeNameSerialization(TestODataMetadataLevel metadataLevel, bool expectedResult)
        {
            // Act
            bool actualResult = ODataCollectionSerializer.ShouldSuppressTypeNameSerialization(
                (ODataMetadataLevel)metadataLevel);

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }
    }
}
