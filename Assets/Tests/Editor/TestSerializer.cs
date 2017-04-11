using Game.Abstractions;
using Game.Helpers;
using Game.Models;
using NUnit.Framework;
using System.Collections.Generic;

public class TestSerializer
{
    private const string FilePath = "./test.json";

    [Test]
    public void AssignString()
    {
        //Arrange
        string value = "asdASD";
        Serializer<string> serializer = new Serializer<string>(FilePath, value);

        //Act
        serializer.Save(false);
        Assert.True(serializer.Load());

        //Assert
        Assert.AreEqual(value, serializer.Value);
    }

    [Test]
    public void AssignNumber()
    {
        //Arrange
        float value = 123.5f;
        Serializer<float> serializer = new Serializer<float>(FilePath, value);

        //Act
        serializer.Save(false);
        Assert.True(serializer.Load());

        //Assert
        Assert.AreEqual(value, serializer.Value);
    }

    [Test]
    public void AssignArray()
    {
        //Arrange
        int[] values = { 1, 2, 3 };
        Serializer<int[]> serializer = new Serializer<int[]>(FilePath, values);

        //Act
        serializer.Save(false);
        Assert.True(serializer.Load());

        //Assert
        Assert.AreEqual(values, serializer.Value);
    }

    [Test]
    public void AssignList()
    {
        //Arrange
        List<string> values = new List<string>();
        values.Add("A");
        values.Add("B");
        values.Add("C");
        Serializer<List<string>> serializer = new Serializer<List<string>>(FilePath, values);

        //Act
        serializer.Save(false);
        Assert.True(serializer.Load());

        //Assert
        Assert.AreEqual(values, serializer.Value);
    }

    [System.Serializable]
    class TestClass { public int n = 10; }

    [Test]
    public void AssignClass()
    {
        //Arrange
        TestClass myClass = new TestClass();
        Serializer<TestClass> serializer = new Serializer<TestClass>(FilePath, myClass);

        //Act
        serializer.Save(false);
        Assert.True(serializer.Load());

        //Assert
        Assert.AreEqual(myClass.n, serializer.Value.n);
    }

    [System.Serializable]
    class TestGeneric<T> { public T n; }

    //[Test][ExpectedException]
    //public void AssignGenericClass()
    //{
    //    //This test is expected to fail since you can't serialise a class with a generic value.
    //    //Arrange
    //    TestGeneric<int> myClass = new TestGeneric<int>();
    //    myClass.n = 99;
    //    Serializer<TestGeneric<int>> serializer = new Serializer<TestGeneric<int>>(FilePath, myClass);

    //    //Act
    //    serializer.Save(false);
    //    Assert.True(serializer.Load());

    //    //Assert
    //    Assert.AreEqual(myClass.n, serializer.Value.n);
    //}


    [System.Serializable]
    class TestSpecification : TestGeneric<int> { }


    [Test]
    public void AssignSpecificationcClass()
    {
        //It seems you can serialise a specification of a generic class.
        //Arrange
        TestSpecification myClass = new TestSpecification();
        myClass.n = 99;
        Serializer<TestSpecification> serializer = new Serializer<TestSpecification>(FilePath, myClass);

        //Act
        serializer.Save(false);
        Assert.True(serializer.Load());

        //Assert
        Assert.AreEqual(myClass.n, serializer.Value.n);
    }

    [System.Serializable]
    class GElement : GridElementModel<BuildingModel> { }

    [System.Serializable]
    class TestBuildingList : Serializer<List<GElement>>
    {
        public TestBuildingList(string path) : base(path, new List<GElement>()) { }
    }

    [Test]
    public void AssignBuildingList()
    {
        //It seems you can serialise a specification of a generic class.
        //Arrange
        TestBuildingList buildings = new TestBuildingList(FilePath);

        //Act
        var building = new GElement();
        buildings.Value.Add(building);
        buildings.Save(false);
        Assert.True(buildings.Load());

        //Assert
        Assert.AreEqual(buildings.Value[0], building);
    }
}