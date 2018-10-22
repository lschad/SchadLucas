using System;

namespace SchadLucas.Wpf.EzMvvm.Context
{
    public static class ViewModelBinder
    {
        public static bool BindView(object viewModel, IView view)
            => Bind(viewModel, view);

        public static bool BindView(IViewModel viewModel, IView view)
            => Bind(viewModel, view);

        private static bool Bind(object viewModel, IView view)
        {
            if (view is null)
            {
                throw new ArgumentNullException(nameof(view));
            }

            var dataContextIsEqual = viewModel == view.DataContext;

            if (!dataContextIsEqual)
            {
                view.DataContext = viewModel;
            }

            return !dataContextIsEqual;
        }
    }
}