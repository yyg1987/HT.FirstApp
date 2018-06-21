using FFImageLoading.Transformations;
using System.Windows.Input;
using Xamarin.Forms;
using HT.FirstApp.Model;

namespace HT.FirstApp.Cells
{
    public partial class StatusesItemCell : ViewCell
    {
		public StatusesItemCell ()
		{
			InitializeComponent ();
            ffimageloading.Transformations.Add(new CircleTransformation());
        }
        protected override void OnBindingContextChanged()
        {
            this.ffimageloading.Source = null;
            var item = BindingContext as Statuses;

            if (item == null)
                return;

            this.ffimageloading.Source = item.UserIconUrl;

            base.OnBindingContextChanged();
        }
        public static readonly BindableProperty DeleteCommandProperty =
            BindableProperty.Create(nameof(DeleteCommand), typeof(ICommand), typeof(StatusesItemCell), default(ICommand));

        public ICommand DeleteCommand
        {
            get { return GetValue(DeleteCommandProperty) as Command; }
            set { SetValue(DeleteCommandProperty, value); }
        }
    }
}