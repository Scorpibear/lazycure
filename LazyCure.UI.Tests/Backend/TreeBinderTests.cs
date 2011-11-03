using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using NUnit.Framework;
using NMock2;
using LifeIdea.LazyCure.Shared.Interfaces;

namespace LifeIdea.LazyCure.UI.Backend
{
    [TestFixture]
    public class TreeBinderTests: Mockery
    {
        private TreeBinder treeBinder;
        private TreeView to;
        ITaskViewDataSource source;
        [SetUp]
        public void SetUp()
        {
            source = NewMock<ITaskViewDataSource>();
            treeBinder = new TreeBinder(source);
            to = new TreeView();
        }
        [Test]
        public void StoreOriginal()
        {
            TreeNode original = new TreeNode("original");
            Stub.On(source).Method("GetEnumerator").Will(Return.Value(new TreeNode[] { original }.GetEnumerator()));
            treeBinder.BindNodes(to);
            Assert.AreSame(original, to.Nodes[0]);
        }
        [Test]
        public void AddSibling()
        {
            Stub.On(source).Method("GetEnumerator").Will(Return.Value(new TreeNode[] { }.GetEnumerator()));
            TreeNode test = new TreeNode("test");
            Stub.On(source).Method("CreateTask").Will(Return.Value(test));
            treeBinder.BindNodes(to);
            TreeNode newNode = treeBinder.AddSibling(null);
            Assert.AreEqual(test,newNode);
        }
        [Test]
        public void AddSiblingToSubtaskCallsAddTaskAfter()
        {
            TreeNode parent = new TreeNode("parent");
            TreeNode sub = new TreeNode("sub");
            parent.Nodes.Add(sub);
            Stub.On(source).Method("GetEnumerator").Will(Return.Value(new TreeNode[] { parent }.GetEnumerator()));
            TreeNode test = new TreeNode("test");
            Expect.Once.On(source).Method("AddTaskAfter").With(sub).Will(Return.Value(test));
            treeBinder.BindNodes(to);
            TreeNode newNode = treeBinder.AddSibling(sub);
            Assert.AreEqual(1, to.Nodes.Count, "count on root");
            Assert.AreEqual(1, to.Nodes[0].Nodes.Count, "count on sub");
            VerifyAllExpectationsHaveBeenMet();
        }
        [Test]
        public void AddSiblingInTheMiddleOfRoot()
        {
            TreeNode first = new TreeNode("first");
            TreeNode second = new TreeNode("second");
            Stub.On(source).Method("GetEnumerator").Will(Return.Value(new TreeNode[] { first,second }.GetEnumerator()));
            TreeNode test = new TreeNode("test");
            Stub.On(source).Method("AddTaskAfter").With(first).Will(Return.Value(test));
            treeBinder.BindNodes(to);
            treeBinder.AddSibling(first);
            Assert.AreEqual(test, to.Nodes[1]);
        }
        [Test]
        public void BindSubnodes()
        {
            TreeNode parent = new TreeNode("parent");
            TreeNode subnode = new TreeNode("subnode");
            parent.Nodes.Add(subnode);
            Stub.On(source).Method("GetEnumerator").Will(Return.Value(new TreeNode[] { parent }.GetEnumerator()));
            treeBinder.BindNodes(to);
            Assert.AreSame(subnode, to.Nodes[0].Nodes[0]);
        }
        [Test]
        public void RemoveSource()
        {
            TreeNode newNode = new TreeNode("remove");
            Stub.On(source).Method("GetEnumerator").Will(Return.Value(new TreeNode[] { newNode }.GetEnumerator()));
            treeBinder.BindNodes(to);
            Expect.Once.On(source).Method("RemoveNode").With(newNode);

            treeBinder.Remove(newNode);
            VerifyAllExpectationsHaveBeenMet();
        }
        [Test]
        public void RemoveView()
        {
            TreeNode newNode = new TreeNode("remove");
            Stub.On(source).Method("GetEnumerator").Will(Return.Value(new TreeNode[] { newNode }.GetEnumerator()));
            Stub.On(source).Method("RemoveNode").With(newNode);
            treeBinder.BindNodes(to);
            
            treeBinder.Remove(newNode);
            Assert.AreEqual(0, to.Nodes.Count);
        }
        [Test]
        public void Rename()
        {
            TreeNode original = new TreeNode("original");
            Stub.On(source).Method("GetEnumerator").Will(Return.Value(new TreeNode[] { original }.GetEnumerator()));
            Expect.Once.On(source).Method("Rename").With(original, "new name");
            treeBinder.BindNodes(to);
            treeBinder.Rename(original, "new name");
            VerifyAllExpectationsHaveBeenMet();
        }
    }
}
