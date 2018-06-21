using System.Windows.Input;
using Xamarin.Forms;
using HT.FirstApp.Helpers;
using HT.FirstApp.Model;
using HT.FirstApp.Pages.Account;

namespace HT.FirstApp.Cells
{
    public partial class BookmarksItemCellView : ViewCell
    {
        public BookmarksItemCellView()
        {
            InitializeComponent();
        }
        public static readonly BindableProperty DeleteCommandProperty =
            BindableProperty.Create(nameof(DeleteCommand), typeof(ICommand), typeof(BookmarksItemCellView), default(ICommand));

        public ICommand DeleteCommand
        {
            get { return GetValue(DeleteCommandProperty) as Command; }
            set { SetValue(DeleteCommandProperty, value); }
        }
        public static readonly BindableProperty EditCommandProperty =
            BindableProperty.Create(nameof(EditCommand), typeof(ICommand), typeof(BookmarksItemCellView), default(ICommand));

        public ICommand EditCommand
        {
            get { return GetValue(EditCommandProperty) as Command; }
            set { SetValue(EditCommandProperty, value); }
        }
    }
}