﻿using AppGet.Commands;
using AppGet.Gui.Controls;
using AppGet.Gui.Views.Shared;
using Caliburn.Micro;

namespace AppGet.Gui.Views
{
    public abstract class CommandViewModel<T> : Conductor<IScreen>, ICommandViewModel where T : AppGetOption
    {
        protected T Options { get; private set; }

        public bool CanHandle(AppGetOption options)
        {
            Options = options as T;
            return options != null;
        }

        protected void ShowError(string title, string message)
        {
            var headerVm = new DialogHeaderViewModel(title, message, "sad-cry", Accents.Success);
            var dialog = new DialogViewModel(headerVm);

            ActivateItem(dialog);
        }
    }
}