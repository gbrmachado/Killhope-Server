using Killhope.Plugins.Rocks.Domain;
using Killhope.Plugins.Rocks.Domain.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Killhope.Plugins.Rocks.Presentation
{
    public class RockListController
    {
        public const string ADD_SHOULD_SAVE = "Do you want to save?";
        public const string REMOVE_SELECTED_ROCK = "Are you sure you want to remove the selected Rock?";
        public const string SAVE_BEFORE_EXIT = "Are you sure you want to remove the selected Rock?";

        public delegate void EditStateChangedEvent(object sender, EventArgs e);

        public event EditStateChangedEvent EditStateChanged;

        private Rock __selectedRock;

        //Should be readOnly - immutable ref after initialise()
        //
        private RockList rockList;

        private bool isAdding;

        private readonly IMessageService messageService;
        private readonly IRockListView view;
        private readonly IJSONModificationService fileService;

        /// <summary>
        /// State of the current index of the controller before an add, to allow for cancellation and returning to the correct index in the list.
        /// </summary>
        /// <remarks>-1 if there was no previous index.</remarks>
        private int indexBeforeAdd = -1;


        /// <summary>
        /// The ID of the currently selected rock (used for saving/change detection).
        /// </summary>
        private string selectedID;
        private readonly ContentEditorViewModel contentEditorViewModel;


        /// <summary>
        /// The currently selected Rock
        /// </summary>
        /// <remarks>May be new (not in the list) only if isAdding is set.</remarks>
        private Rock selected
        {
            get { return __selectedRock; }
            set
            {
                this.__selectedRock = value;
                if (__selectedRock != null)
                    this.selectedID = __selectedRock.UniqueId;
                else
                    this.selectedID = null;
                this.SetupView(selected);
                OnEditStateChanged(EventArgs.Empty);
            }
        }

        private int currentIndex { get { return rockList.IndexOf(selectedID); } }

        public RockListController(IJSONModificationService fileService, IMessageService messageService, IRockListView view, ContentEditorViewModel viewModel)
        {
            if (fileService == null)
                throw new ArgumentNullException("fileService");

            this.fileService = fileService;
            this.messageService = messageService;
            this.view = view;
            this.contentEditorViewModel = viewModel;


        }

        public void EditContent(Content content)
        {
            contentEditorViewModel.Content = content;
            contentEditorViewModel.ShowDialog();
        }


        public void Initialise()
        {
            this.rockList = RockList.FromJson(fileService.Load());

            //We want to set either way to make way 
            this.selected = hasElements() ? this.rockList.Clone(0) : null;
        }

        private bool validate(bool showMessage = true)
        {
            Rock toAdd = getRockFromView();

            ValidationResult v = toAdd.Validate();

            //TODO: Add unique constraint on UniqueID

            if (showMessage && !v.isValid)
            {
                messageService.DisplayValidationResult(v);
            }

            return v.isValid;
        }

        /// <summary>
        /// Returns the modifiable data in the view as a new rock object.
        /// </summary>
        /// <returns></returns>
        private Rock getRockFromView()
        {
            //TODO: A few of these fields are mutable.
            Rock toAdd = new Rock();
            toAdd.Content = view.Content;
            toAdd.Formula = view.Formula;
            toAdd.Images = view.Images;
            toAdd.GalleryImages = view.GalleryImages;
            toAdd.Title = view.Title;
            toAdd.UniqueId = view.UniqueId;
            toAdd.VideoPath = view.VideoPath;
            return toAdd;
        }

        public void Save(bool shouldValidate = true)
        {
            if (shouldValidate && !validate())
                return;

            Rock toAdd = getRockFromView();
            if (isAdding)
            {
                this.rockList.Add(toAdd);
                isAdding = false;
            }
            else
            {
                this.rockList.Replace(this.selectedID, toAdd);
            }

            this.selected = toAdd;
        }


        public bool CanAdd() { return !isAdding; }

        public bool CanDelete() { return selected != null; }

        public bool CanGoNext() { return hasElements() && !isAdding && !isLast(); }

        public bool CanGoPrevious() { return hasElements() && !isAdding && !isFirst(); }

        /// <summary>
        /// Whether the save button can be pressed
        /// </summary>
        /// <remarks>Not necessarily whether the button will work due to validation concerns.
        /// Note that it is the view's responsibility to disable this if no data changes are detected.</remarks>
        /// <returns>Whether the save button can be pressed</returns>
        public bool CanSave() { return selected != null && view.CanSave; }



        /// <summary>
        /// Whether we have elements to select from in the list.
        /// </summary>
        /// <remarks>Will return false if we are in the state of adding the first item.</remarks>
        /// <returns></returns>
        private bool hasElements()
        {
            return rockList.Count > 0;
        }

        /// <summary>
        /// Whether we are at the final element in the collection.
        /// </summary>
        /// <returns></returns>
        private bool isLast()
        {
            if (!hasElements())
                throw new InvalidOperationException("No elements in collection.");

            if (isAdding)
                throw new InvalidOperationException("Cannot determine the collection position due to being in the \"Add\" State.");

            int index = rockList.IndexOf(selectedID);

            if (index < 0)
                throw new InvalidStateException("Selected object not found in collection (and not in 'Add' State).");

            return index == rockList.Rocks.Count - 1;

        }


        private bool isFirst()
        {
            if (!hasElements())
                throw new InvalidOperationException("No elements in collection.");

            if (isAdding)
                throw new InvalidOperationException("Cannot determine the collection position due to being in the \"Add\" State.");

            int index = rockList.IndexOf(selectedID);

            if (index < 0)
                throw new InvalidStateException("Selected object not found in collection (and not in 'Add' State).");

            return index == 0;
        }


        public void Add()
        {
            if (CanSave() && messageService.DisplayYesNoCancel(ADD_SHOULD_SAVE) == MessageResult.Yes)
            {
                if (validate(showMessage: true))
                    Save(shouldValidate: false);
                else
                    return; //save failed, we want to wait until
            }

            this.indexBeforeAdd = rockList.IndexOf(this.selected);

            //Note: selectedID will be set to null, as expected.
            this.selected = new Rock();
            this.isAdding = true;
        }
        
        public void Delete()
        {
            var res = messageService.DisplayYesNo(REMOVE_SELECTED_ROCK);
            if (res != MessageResult.Yes)
                return;

            if (isAdding)
            {
                //We the position in the list when adding so a delete takes us back.
                this.selected = indexBeforeAdd != -1 ? rockList.Clone(indexBeforeAdd) : null;
                this.indexBeforeAdd = -1;
            }
            else
            {
                var newRock = this.rockList.Delete(selectedID);
                this.selected = newRock;
            }

        }

        public void GoNext()
        {
            if (!CanGoNext())
                throw new InvalidOperationException();

            if (!TrySave())
                return;

            this.selected = rockList.Clone(this.currentIndex + 1);
        }

        public void GoPrevious()
        {
            if (!CanGoPrevious())
                throw new InvalidOperationException();

            if (!TrySave())
                return;

            this.selected = rockList.Clone(this.currentIndex - 1);
        }

        private bool TrySave()
        {
            if (!CanSave())
                return true;

            if (!validate())
                return false;

            Save(shouldValidate: false);

            return true;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>True if the form may close.</returns>
        public bool TryClose()
        {
            var res = messageService.DisplayYesNoCancel(SAVE_BEFORE_EXIT);
            if(res == MessageResult.Cancel)
                return false;

            if (res == MessageResult.Yes) 
            {
                //If the save fails, stop.
                if (!TrySave())
                    return false;

                //Save the RockList 
                try
                {
                    this.fileService.Save(RockList.ToJson(this.rockList));
                }
                catch (Exception e)
                {
                    messageService.DisplayError(e, "Failed to save Rock List");
                    return false;
                }
            }


            return true;
        }

        private void SetupView(Rock selected)
        {
            if (selected != null)
            {
                view.Content = selected.Content;
                view.Formula = selected.Formula;
                view.Images = selected.Images;
                view.GalleryImages = selected.GalleryImages;
                view.Title = selected.Title;
                view.UniqueId = selected.UniqueId;
                view.VideoPath = selected.VideoPath;
            }

            this.view.LoadRock(selected);
        }


        //TODO: Make more use of these functions for readonly.
        protected virtual void OnEditStateChanged(EventArgs e)
        {
            if (EditStateChanged != null)
                EditStateChanged(this, e);
        }

        public bool CanEdit { get { return selected != null; } }
    }
}
