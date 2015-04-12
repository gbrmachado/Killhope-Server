using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Killhope.Plugins.Rocks.Presentation;
using System.Collections.Generic;
using Killhope.Plugins.Rocks.Domain.Test.Mocks;

namespace Killhope.Plugins.Rocks.Domain.Test
{
    [TestClass]
    public class TestRockListController
    {
        private MockMessageService messageService;
        private MockRockListView view;

        [TestInitialize]
        public void Init()
        {
            this.messageService = new MockMessageService();
            this.view = new MockRockListView();
        }

        private MockRockList GetBlankRockList()
        {
            return new MockRockList(new RockList());
        }

        private RockListController GetBlankRockListController()
        {
            var rl = GetBlankRockList();
            var ret = new RockListController(rl, messageService, this.view, null);
            ret.Initialise();
            return ret;
        }

        private RockListController SaveFirstElement()
        {
            var ctl = GetBlankRockListController();
            AddElement(ctl, "Rock-1");
            return ctl;
        }

        private void AddElement(RockListController ctl, string name)
        {
            ctl.Add();
            this.view.MakeValidRock(name);

            this.messageService.AddOnlyValid();

            ctl.Save();
        }

        private RockListController SaveTwoElements()
        {
            var ctl = SaveFirstElement();
            this.view.HasChanged = false;
            AddElement(ctl, "Rock-Second");
            return ctl;
        }

        private RockListController AddTwoDeleteOne()
        {
            var ctl = SaveTwoElements();
            ctl.Delete();
            return ctl;
        }

        private RockListController AddAndDeleteTwo()
        {
            var ctl = SaveTwoElements();
            ctl.Delete();
            ctl.Delete();
            return ctl;
        }

        [TestMethod]
        public void MetaBlankRockList()
        {
            var rl = GetBlankRockList();
            Assert.AreEqual(0, rl.UseAsOutput.Count);
        }

        private void AssertStateIsBlank(RockListController ctl)
        {
            Assert.IsFalse(ctl.CanDelete());
            Assert.IsFalse(ctl.CanGoNext());
            Assert.IsFalse(ctl.CanGoPrevious());
            Assert.IsFalse(ctl.CanSave());

            Assert.IsTrue(ctl.CanAdd());
        }


        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorFailsOnNull()
        {
            new RockListController(null, null, null, null);
        }

        [TestMethod]
        public void BlankRockListDoesNotFail()
        {
            var rl = GetBlankRockList();
            var res = new RockListController(rl, this.messageService, this.view, null);
            res.Initialise();

        }

        [TestMethod]
        public void BlankRockListCannotSave()
        {
            var ctl = GetBlankRockListController();
            AssertStateIsBlank(ctl);
        }

        [TestMethod]
        public void AddChangesSaveState()
        {
            var ctl = GetBlankRockListController();
            ctl.Add();
            view.MakeValidRock("a");
            Assert.IsTrue(ctl.CanSave());
        }




        [TestMethod]
        public void SavingFirstElementAllowsDelete()
        {
            var ctl = SaveFirstElement();
            Assert.IsTrue(ctl.CanDelete());
        }

        [TestMethod]
        public void SavingFirstElementAllowsSaveAndAdd()
        {
            var ctl = SaveFirstElement();
            Assert.IsTrue(ctl.CanSave());
            Assert.IsTrue(ctl.CanAdd());
        }

        [TestMethod]
        public void SavingFirstElementStillAllowsNoNavigation()
        {
            var ctl = SaveFirstElement();
            Assert.IsFalse(ctl.CanGoPrevious());
            Assert.IsFalse(ctl.CanGoNext());
        }

        [TestMethod]
        public void SavingSecondElementAllowsNavigationBackToFirst()
        {
            var ctl = SaveTwoElements();
            Assert.IsTrue(ctl.CanGoPrevious());
            Assert.IsFalse(ctl.CanGoNext());
        }



        [TestMethod]
        public void DeletingTwoElementsRevertsToStartState()
        {
            this.messageService.Enqueue(RockListController.REMOVE_SELECTED_ROCK, Application.MessageResult.Yes);
            this.messageService.Enqueue(RockListController.REMOVE_SELECTED_ROCK, Application.MessageResult.Yes);
            var ctl = AddAndDeleteTwo();
            AssertStateIsBlank(ctl);
        }

        [TestMethod]
        public void IfNoIsSelectedRockShouldNotBeDeleted()
        {
            this.messageService.Enqueue(RockListController.REMOVE_SELECTED_ROCK, Application.MessageResult.No);
            this.messageService.Enqueue(RockListController.REMOVE_SELECTED_ROCK, Application.MessageResult.No);
            var ctl = AddAndDeleteTwo();

            Assert.IsTrue(ctl.CanGoPrevious());

        }

        [TestMethod]
        public void CantGoBackIfOnlyOneLeft()
        {
            this.messageService.Enqueue(RockListController.REMOVE_SELECTED_ROCK, Application.MessageResult.Yes);
            var ctl = AddTwoDeleteOne();

            Assert.IsFalse(ctl.CanGoPrevious());
            Assert.IsTrue(ctl.CanDelete());
            Assert.IsFalse(ctl.CanGoNext());
            view.HasChanged = true;
            Assert.IsTrue(ctl.CanSave());
        }

        [TestMethod]
        public void CurrentFail()
        {
            //After saving, the saved rock is not the same as the selected rock.
            //Actually, this most likely passes due to calling new Rock() before saving.

            Assert.Inconclusive();
        }

        [TestMethod]
        public void IndexBeforeAdd()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void IndexBeforeAddStart()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void IndexBeforeAddEnd()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void IndexBeforeAddWithNoPrevious()
        {
            Assert.Inconclusive();
        }
 
    }
}
