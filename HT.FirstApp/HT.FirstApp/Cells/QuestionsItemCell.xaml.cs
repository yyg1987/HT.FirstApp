using FFImageLoading.Transformations;
using Xamarin.Forms;
using HT.FirstApp.Model;

namespace HT.FirstApp.Cells
{
    public partial class QuestionsItemCell : ViewCell
    {
		public QuestionsItemCell ()
		{
			InitializeComponent ();
            ffimageloading.Transformations.Add(new CircleTransformation());
        }
        protected override void OnBindingContextChanged()
        {
            this.ffimageloading.Source = null;
            var item = BindingContext as Questions;

            if (item == null)
                return;

            this.ffimageloading.Source = item.QuestionUserInfo.IconDisplay;

            base.OnBindingContextChanged();
        }
    }
}