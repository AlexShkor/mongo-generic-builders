using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using NUnit.Framework;

namespace MongoUtils.Tests
{
    [TestFixture]
    public class MongoUpdateBuilderMultipleMethodsTest
    {
        [Test]
        public void test()
        {
            var updater = new MongoUpdateBuilder<TestDocument>();
            var doc = new ItemDocument() { Value = 5 };
            updater.Set(x => x.Name, "Name").Set(x => x.InnerDoc, doc);
            Assert.AreEqual(updater.ResultQuery.ToString(),
                Update.Set("Name", "Name").Set("InnerDoc", doc.ToBsonDocument()).ToString());
        }
    }

    [TestFixture]
    public class MongoUpdateBuilderSetMetodTest
    {
        [Test]
        public void it_build_set_query()
        {
            var updater = new MongoUpdateBuilder<TestDocument>();
            updater.Set(x => x.Name, "Name");
            Assert.AreEqual(updater.ResultQuery.ToString(), Update.Set("Name", "Name").ToString());
        }


        [Test]
        public void it_build_set_array_inner_query()
        {
            var updater = new MongoUpdateBuilder<TestDocument>();
            updater.Set(x => x.Items[It.IsAny<int>()].Value, 5);
            Assert.AreEqual(updater.ResultQuery.ToString(), Update.Set("Items.$.Value", 5).ToString());
        }

        [Test]
        public void it_build_set_array_positionaly_query()
        {
            var updater = new MongoUpdateBuilder<TestDocument>();
            updater.Set(x => x.Items[101].Value, 5);
            Assert.AreEqual(updater.ResultQuery.ToString(), Update.Set("Items.101.Value", 5).ToString());
        }

        [Test]
        public void it_build_set_array_doc_query()
        {
            var updater = new MongoUpdateBuilder<TestDocument>();
            var doc = new ItemDocument() { Value = 5 };
            updater.Set(x => x.Items[It.IsAny<int>()], doc);
            Assert.AreEqual(updater.ResultQuery.ToString(), Update.Set("Items.$", doc.ToBsonDocument()).ToString());
        }

        [Test]
        public void it_build_set_array_value_query()
        {
            var updater = new MongoUpdateBuilder<TestDocument>();
            updater.Set(x => x.Srtings[It.IsAny<int>()], "Value");
            Assert.AreEqual(updater.ResultQuery.ToString(), Update.Set("Srtings.$", "Value").ToString());
        }

        [Test]
        public void it_build_set_inner_doc()
        {
            var updater = new MongoUpdateBuilder<TestDocument>();
            var doc = new ItemDocument() { Value = 5 };
            updater.Set(x => x.InnerDoc, doc);
            Assert.AreEqual(updater.ResultQuery.ToString(), Update.Set("InnerDoc", doc.ToBsonDocument()).ToString());
        }
    }

    [TestFixture]
    public class MongoUpdateBuilderPushMetodTest
    {
        [Test]
        public void it_build_set_query()
        {
            var updater = new MongoUpdateBuilder<TestDocument>();
            updater.Push(x => x.Srtings, "Name");
            Assert.AreEqual(updater.ResultQuery.ToString(), Update.Push("Srtings", "Name").ToString());
        }


    }

    public class ItemDocument
    {
        public int Value { get; set; }
    }


    public class TestDocument
    {
        public string Name { get; set; }

        public List<ItemDocument> Items { get; set; }

        public List<String> Srtings { get; set; }

        public ItemDocument InnerDoc { get; set; }

        public int Value { get; set; }
    }
}
